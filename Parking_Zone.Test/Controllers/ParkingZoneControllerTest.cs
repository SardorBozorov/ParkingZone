using Microsoft.AspNetCore.Mvc;
using Moq;
using Parking_Zone.Domain.Entities;
using Parking_Zone.MVC.Areas.Admin.Controllers;
using Parking_Zone.MVC.Models.ParkingZoneVMs;
using Parking_Zone.Service.Interfaces;
using System.Text.Json;
using Xunit;

namespace Parking_Zone.Test.Controllers;

public class ParkingZoneControllerTest
{
    private readonly Mock<IParkingZoneService> _parkingZoneServiceMoq;
    private readonly ParkingZonesController _parkingZoneController;
    private readonly ParkingZone _parkingZone;
    private readonly long id = 1;
    public ParkingZoneControllerTest()
    {
        _parkingZoneServiceMoq = new Mock<IParkingZoneService>();
        _parkingZoneController = new ParkingZonesController(_parkingZoneServiceMoq.Object);
        _parkingZone = new()
        {
            Id = id,
            Name = "Test",
            Address = "Tashkent"
        };
    }

    #region Index
    [Fact]
    public void GivenNothing_WhenIndexIsCalled_ThenReturnsViewResultWithListOfVMs()
    {
        //Arrange
        var expectedParkingZones = new List<ParkingZone>() { _parkingZone };
        var expectedLIstOfVMs = new List<ListOfItemsVM>() { new(_parkingZone) };

        _parkingZoneServiceMoq
            .Setup(x => x.GetAll())
            .Returns(expectedParkingZones);

        //Act
        var viewResult = _parkingZoneController.Index();

        //Assert
        var model = Assert.IsType<ViewResult>(viewResult).Model;
        Assert.NotNull(model);
        Assert.IsAssignableFrom<IEnumerable<ListOfItemsVM>>(model);
        Assert.Equal(JsonSerializer.Serialize(expectedLIstOfVMs), JsonSerializer.Serialize(model));
        _parkingZoneServiceMoq.Verify(x => x.GetAll(), Times.Once);
    }
    #endregion

    #region Details
    [Fact]
    public void GivenId_WhenDetailIsCalled_ThenReturnViewResultWithVM()
    {
        //Arrange
        _parkingZoneServiceMoq
            .Setup(x => x.GetById(id))
            .Returns(_parkingZone);
        DetailsVM expectedDetailVM = new DetailsVM(_parkingZone);

        //Act
        var viewResult = _parkingZoneController.Details(id);

        //Assert
        var model = Assert.IsType<ViewResult>(viewResult).Model;
        Assert.NotNull(model);
        Assert.IsAssignableFrom<DetailsVM>(model);
        Assert.Equal(JsonSerializer.Serialize(expectedDetailVM), JsonSerializer.Serialize(model));
        _parkingZoneServiceMoq.Verify(x => x.GetById(id), Times.Once);
    }

    [Fact]
    public void GivenId_WhenDetailsIsCalled_ThenReturnsNotFound()
    {
        //Arrange
        _parkingZoneServiceMoq.Setup(x => x.GetById(id));

        //Act
        var result = _parkingZoneController.Details(id);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<NotFoundResult>(result);
        _parkingZoneServiceMoq.Verify(x => x.GetById(id), Times.Once());
    }
    #endregion

    #region Create

    [Fact]
    public void GivenNothing_WhenCreateIsCalled_ThenReturnsViewResult()
    {
        //Act
        var viewResult = _parkingZoneController.Create();

        //Assert
        Assert.IsType<ViewResult>(viewResult);
    }

    [Fact]
    public void GivenCreateVM_WhenPostCreateIsCalled_ThenModelStateIsFalseAndReturnsViewResultVM()
    {
        //Arrange
        CreateVM createVM = new();
        _parkingZoneController.ModelState.AddModelError("Name", "Required");

        //Act
        var viewResult = _parkingZoneController.Create(createVM);

        //Assert
        Assert.NotNull(viewResult);
        Assert.IsType<ViewResult>(viewResult);
        Assert.False(_parkingZoneController.ModelState.IsValid);
    }

    [Fact]
    public void GivenCreateVM_WhenPostCreateIsCalled_ThenModelStateIsTrueAndReturnsRedirectToActionResult()
    {
        //Arrange
        CreateVM createVM = new();
        _parkingZoneServiceMoq.Setup(x => x.Create(It.IsAny<ParkingZone>()));

        //Act
        var viewResult = _parkingZoneController.Create(createVM);

        //Assert
        var redirectResult = Assert.IsType<RedirectToActionResult>(viewResult);
        Assert.Equivalent(redirectResult.ActionName, "Index");
        Assert.True(_parkingZoneController.ModelState.IsValid);
        _parkingZoneServiceMoq.Verify(x => x.Create(It.IsAny<ParkingZone>()), Times.Once());
    }

    #endregion

    #region Edit

    [Fact]
    public void GivenId_WhenEditIsCalled_ThenReturnsNotFoundResult()
    {
        //Arrange
        _parkingZoneServiceMoq.Setup(x => x.GetById(id));

        //Act
        var result = _parkingZoneController.Edit(id);

        //Assert
        Assert.IsType<NotFoundResult>(result);
        _parkingZoneServiceMoq.Verify(x => x.GetById(id), Times.Once());
    }

    [Fact]
    public void GivenId_WhenEditIsCalled_ThenReturnsViewResultWithVM()
    {
        //Arrange
        EditVM expectedEditVM = new(_parkingZone);

        _parkingZoneServiceMoq
            .Setup(x => x.GetById(id))
            .Returns(_parkingZone);

        //Act
        var viewResult = _parkingZoneController.Edit(id);

        //Assert
        var model = Assert.IsType<ViewResult>(viewResult).Model;
        Assert.IsAssignableFrom<EditVM>(model);
        Assert.Equal(JsonSerializer.Serialize(expectedEditVM), JsonSerializer.Serialize(model));
        _parkingZoneServiceMoq.Verify(x => x.GetById(id), Times.Once());
    }

    [Fact]
    public void GivenIdAndEditVM_WhenPostEditIsCalled_ThenReturnsNotFoundResult()
    {
        //Arrange
        EditVM editVM = new();
        ParkingZone existingParkingZone = new() { Id = 2 };

        _parkingZoneServiceMoq
            .Setup(x => x.GetById(id))
            .Returns(existingParkingZone);

        //Act
        var result = _parkingZoneController.Edit(id, editVM);

        //Assert
        Assert.IsType<NotFoundResult>(result);
        _parkingZoneServiceMoq.Verify(x => x.GetById(id), Times.Once());
    }

    [Theory]
    [InlineData("Name", "Required")]
    [InlineData("Address", "Required")]
    public void GivenIdAndEditVM_WhenPostEditIsCalled_ThenModelStateIsFalseAndReturnsViewResult(string key, string errorMessage)
    {
        //Arrange
        EditVM editVM = new(_parkingZone);
        EditVM expectedEditVM = new(_parkingZone);

        _parkingZoneController.ModelState.AddModelError(key, errorMessage);
        _parkingZoneServiceMoq
            .Setup(x => x.GetById(id))
            .Returns(_parkingZone);

        //Act
        var result = _parkingZoneController.Edit(id, editVM);

        //Assert
        var model = Assert.IsType<ViewResult>(result).Model;
        Assert.False(_parkingZoneController.ModelState.IsValid);
        Assert.Equal(JsonSerializer.Serialize(expectedEditVM), JsonSerializer.Serialize(model));
        _parkingZoneServiceMoq.Verify(x => x.GetById(id), Times.Once());
    }

    [Fact]
    public void GivenIdAndEditVM_WhenEditIsCalled_ThenModelStateIsTrueAndReturnsRedirectToActionResult()
    {
        //Arrange
        EditVM editVM = new();
        _parkingZoneServiceMoq.Setup(x => x.Update(_parkingZone));
        _parkingZoneServiceMoq
            .Setup(x => x.GetById(id))
            .Returns(_parkingZone);

        //Act
        var result = _parkingZoneController.Edit(id, editVM);

        //Assert
        var resModel = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equivalent(resModel.ActionName, "Index");
        Assert.True(_parkingZoneController.ModelState.IsValid);
        _parkingZoneServiceMoq.Verify(x => x.GetById(id), Times.Once());
        _parkingZoneServiceMoq.Verify(x => x.Update(_parkingZone), Times.Once());
    }

    #endregion

    #region Delete
    [Fact]
    public void GivenId_WhenDeleteIsCalled_ThenReturnsNotFoundResult()
    {
        //Arrange
        _parkingZoneServiceMoq.Setup(x => x.GetById(id));

        //Act
        var result = _parkingZoneController.Delete(id);

        //Assert
        Assert.IsType<NotFoundResult>(result);
        _parkingZoneServiceMoq.Verify(x => x.GetById(id), Times.Once());
    }

    [Fact]
    public void GivenId_WhenDeleteIsCalled_ThenReturnsViewResult()
    {
        //Arrange
        _parkingZoneServiceMoq
            .Setup(x => x.GetById(id))
            .Returns(_parkingZone);

        //Act
        var result = _parkingZoneController.Delete(id);

        //Assert
        var model = Assert.IsType<ViewResult>(result).Model;
        Assert.IsAssignableFrom<ParkingZone>(model);
        Assert.Equal(JsonSerializer.Serialize(_parkingZone), JsonSerializer.Serialize(model));
        _parkingZoneServiceMoq.Verify(x => x.GetById(id), Times.Once());
    }

    [Fact]
    public void GivenId_WhenDeleteConfirmedIsCalled_ThenReturnsNotFoundResult()
    {
        //Arrange
        _parkingZoneServiceMoq.Setup(x => x.GetById(id));

        //Act
        var result = _parkingZoneController.DeleteConfirmed(id);

        //Assert
        Assert.IsType<NotFoundResult>(result);
        _parkingZoneServiceMoq.Verify(x => x.GetById(id), Times.Once());
    }

    [Fact]
    public void GivenId_WhenDeleteConfirmedIsCalled_ThenReturnsRedirectToActionResult()
    {
        //Arrange
        _parkingZoneServiceMoq.Setup(x => x.Delete(_parkingZone));
        _parkingZoneServiceMoq
            .Setup(x => x.GetById(id))
            .Returns(_parkingZone);

        //Act
        var result = _parkingZoneController.DeleteConfirmed(id);

        //Assert
        var viewResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equivalent(viewResult.ActionName, "Index");
        _parkingZoneServiceMoq.Verify(x => x.GetById(id), Times.Once());
        _parkingZoneServiceMoq.Verify(x => x.Delete(_parkingZone), Times.Once());
    }

    #endregion
}

