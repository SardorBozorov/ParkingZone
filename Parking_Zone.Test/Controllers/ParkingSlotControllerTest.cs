using Microsoft.AspNetCore.Mvc;
using Moq;
using Parking_Zone.Domain.Entities;
using Parking_Zone.Domain.Enums;
using Parking_Zone.MVC.Areas.Admin.Controllers;
using Parking_Zone.MVC.Models.ParkingSlotVMs;
using Parking_Zone.Service.Interfaces;
using System.Text.Json;
using Xunit;

namespace Parking_Zone.Test.Controllers;

public class ParkingSlotControllerTest
{
    private readonly Mock<IParkingSlotService> _parkingSlotServiceMoq;
    private readonly Mock<IParkingZoneService> _parkingZoneServiceMoq;
    private readonly ParkingSlotController _parkingSlotController;
    private readonly ParkingZone _parkingZone;
    private readonly List<ParkingSlot> _parkingSlot;
    private readonly int _id = 1;

    public ParkingSlotControllerTest()
    {
        _parkingSlotServiceMoq = new Mock<IParkingSlotService>();
        _parkingZoneServiceMoq = new Mock<IParkingZoneService>();
        _parkingSlotController = new ParkingSlotController(_parkingSlotServiceMoq.Object, _parkingZoneServiceMoq.Object);
        _parkingZone = new() 
        {
            Id = _id,
            Name = "Zone 1",
            Address = "Tashkent"
        };
        _parkingSlot = new List<ParkingSlot>
        {
            new ()
            {
                Id = 1,
                Number = 2,
                IsAvailable = false,
                ParkingZoneId = 1,
                Category = 0,
            },
            new ()
            {
                Id = _id,
                Number = 3,
                IsAvailable = true,
                ParkingZoneId = 1,
                Category = ParkingSlotCategory.Business
            }
        };
    }

    #region Index
    [Fact]
    public void GivenParkingZoneId_WhenIndexIsCalled_ThenReturnsParkingSlotsVM()
    {
        // Arrange
        var expectedVMs = new List<ListOfSlotsVM>();
        expectedVMs.AddRange(ListOfSlotsVM.MapToVM(_parkingSlot));

        _parkingSlotServiceMoq
            .Setup(x => x.GetSlotsByZoneId(_id))
            .Returns(_parkingSlot);
        _parkingZoneServiceMoq
            .Setup(x => x.GetById(_id))
            .Returns(_parkingZone);

        //Act
        var result = _parkingSlotController.Index(_id);
        var model = ((ViewResult)result).Model;

        //Assert
        Assert.IsType<ViewResult>(result);
        Assert.IsAssignableFrom<IEnumerable<ListOfSlotsVM>>(model);
        Assert.Equal(JsonSerializer.Serialize(model), JsonSerializer.Serialize(expectedVMs));
        Assert.NotNull(result);
        Assert.NotNull(model);
        _parkingSlotServiceMoq.Verify(_parkingSlot => _parkingSlot.GetSlotsByZoneId(_id), Times.Once);
        _parkingZoneServiceMoq.Verify(_parkingZone => _parkingZone.GetById(_id), Times.Once);
    }
    #endregion

    #region Create
    [Fact]
    public void GivenParkingZoneId_WhenCreateGetIsCalled_ThenReturnViewResult()
    {
        //Arrange
        CreateVM expectedCreateVM = new()
        {
            ParkingZoneId = _parkingZone.Id,
        };

        //Act
        var result = _parkingSlotController.Create(expectedCreateVM.ParkingZoneId);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(JsonSerializer.Serialize(expectedCreateVM.ParkingZoneId), JsonSerializer.Serialize(_parkingZone.Id));
    }

    [Fact]
    public void GivenCreateVM_WhenCreatePostIsCalled_ThenModelStateIsTrueAndReturnsRedirectToIndex()
    {
        //Arrange
        CreateVM expectedCreateVM = new()
        {
            Number = _parkingSlot[0].Number,
            Category = _parkingSlot[0].Category,
            IsAvailable = _parkingSlot[0].IsAvailable,
            ParkingZoneId = _parkingSlot[0].ParkingZoneId,
        };

        _parkingSlotServiceMoq
                .Setup(x => x.Create(_parkingSlot[0]));

        //Act
        var result = _parkingSlotController.Create(expectedCreateVM);

        //Assert
        Assert.NotNull(result);
        Assert.True(_parkingSlotController.ModelState.IsValid);
        _parkingSlotServiceMoq.Verify(x => x.Create(It.IsAny<ParkingSlot>()), Times.Once);
    }
    #endregion

     #region Edit
        [Fact]
        public void GivenParkingSlotId_WhenEditIsCalled_ThenReturnsViewResult()
    {
            //Arrange
            EditVM expectedEditVM = new(_parkingSlot[0]);
            _parkingSlotServiceMoq.Setup(x => x.GetById(_parkingSlot[0].Id)).Returns(_parkingSlot[0]);

            //Act
            var result = _parkingSlotController.Edit(_parkingSlot[0].Id);

            //Assert
            var model = Assert.IsType<ViewResult>(result).Model;
            Assert.NotNull(result);
            Assert.Equal(JsonSerializer.Serialize(model), JsonSerializer.Serialize(expectedEditVM));
            _parkingSlotServiceMoq.Verify(x => x.GetById(_parkingSlot[0].Id), Times.Once);
        }

        [Fact]
        public void GivenParkingSlotId_WhenEditGetIsCalled_ThenReturnsNotFound()
        {
            //Arrange
            _parkingSlotServiceMoq.Setup(x => x.GetById(_parkingSlot[0].Id));

            //Act
            var result = _parkingSlotController.Edit(_parkingSlot[0].Id);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
            _parkingSlotServiceMoq.Verify(x => x.GetById(_parkingSlot[0].Id), Times.Once);
        }

        [Fact]
        public void GivenEditVMAndParkingSlotId_WhenEditPostIsCalled_ThenIdAndSlotIdDoNotMatchAndReturnsNotFound()
        { 
            //Arrange
            EditVM editVM = new(_parkingSlot[0]);

            //Act
            var result = _parkingSlotController.Edit(editVM, _parkingSlot[0].Id);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GivenEditVMAndParkingSlotId_WhenEditPostIsCalled_ThenReturnsNotFoundIfSlotIsNull()
        {
             //Arrange
             EditVM editVM = new(_parkingSlot[0]);
            _parkingSlotServiceMoq.Setup(x => x.GetById(editVM.Id));

            //Act
            var result = _parkingSlotController.Edit(editVM, editVM.Id);

            //Assert
            Assert.IsType<NotFoundResult>(result);
            _parkingSlotServiceMoq.Verify(x => x.GetById(editVM.Id), Times.Once);
        }

        [Fact]
        public void GivenEditVMAndParkingSlotId_WhenEditPostIsCalled_ThenReturnModelError()
        {
            //Arrange
            EditVM editVM = new(_parkingSlot[0]);
            _parkingSlotController.ModelState.AddModelError("Number", "Number is not valid");
            _parkingSlotServiceMoq
                    .Setup(x => x.GetById(_parkingSlot[0].Id))
                    .Returns(_parkingSlot[0]);
            _parkingSlotServiceMoq
                    .Setup(x => x.IsUniqueNumber(editVM.ParkingZoneId, editVM.Number))
                    .Returns(false);

            //Act
            var result = _parkingSlotController.Edit(editVM, _parkingSlot[0].Id);

            //Assert
            Assert.NotNull(result);
            Assert.False(_parkingSlotController.ModelState.IsValid);
            _parkingSlotServiceMoq.Verify(x => x.GetById(_parkingSlot[0].Id), Times.Once);
            _parkingSlotServiceMoq
                    .Verify(x => x.IsUniqueNumber(editVM.ParkingZoneId, editVM.Number), Times.Once);
        }

        [Fact]
        public void GivenEditVMAndParkingSlotId_WhenEditPostIsCalled_ThenModelStateIsValidReturnsToIndex()
        {
            //Arrange
            EditVM editVM = new(_parkingSlot[0])
            {
                Number = 123
            };
            var slot = editVM.MapToModel(_parkingSlot[0]);
            _parkingSlotServiceMoq
                    .Setup(x => x.GetById(_parkingSlot[0].Id))
                    .Returns(_parkingSlot[0]);

            _parkingSlotServiceMoq.Setup(x => x.Update(slot));

            _parkingSlotServiceMoq
                    .Setup(x => x.IsUniqueNumber(editVM.ParkingZoneId, editVM.Number))
                    .Returns(false);

            //Act
            var result = _parkingSlotController.Edit(editVM, _parkingSlot[0].Id);

            //Assert
            Assert.NotNull(result);
            Assert.NotEqual(JsonSerializer.Serialize(result), JsonSerializer.Serialize(editVM));
            Assert.IsType<RedirectToActionResult>(result);
            Assert.True(_parkingSlotController.ModelState.IsValid);
            _parkingSlotServiceMoq.Verify(x => x.Update(_parkingSlot[0]), Times.Once);
            _parkingSlotServiceMoq.Verify(x => x.GetById(_parkingSlot[0].Id), Times.Once);
            _parkingSlotServiceMoq
                    .Verify(x => x.IsUniqueNumber(editVM.ParkingZoneId, editVM.Number), Times.Once);
        }
    #endregion

    #region Details
    [Fact]
    public void GivenParkingSlotId_WhenDetailsIsCalled_ThenReturnsViewResult()
    {
        //Arrange
        DetailsVM expectedVM = new(_parkingSlot[0]);
        _parkingSlotServiceMoq
            .Setup(x => x.GetById(_parkingSlot[0].Id))
            .Returns(_parkingSlot[0]);

        //Act
        var result = _parkingSlotController.Details(_parkingSlot[0].Id);

        //Assert
        var model = Assert.IsType<ViewResult>(result).Model;
        Assert.NotNull(result);
        Assert.Equal(JsonSerializer.Serialize(model), JsonSerializer.Serialize(expectedVM));
        _parkingSlotServiceMoq.Verify(x => x.GetById(_parkingSlot[0].Id), Times.Once);
    }
    #endregion

    #region Delete
    [Fact]
    public void GivenSlotId_WhenGetDeleteIsCalled_ThenReturnsNotFoundResult()
    {
        //Arrange
        _parkingSlotServiceMoq.Setup(x => x.GetById(_parkingSlot[0].Id));

        //Act
        var result = _parkingSlotController.Delete(_parkingSlot[0].Id);

        //Assert
        Assert.IsType<NotFoundResult>(result);
        _parkingSlotServiceMoq.Verify(x => x.GetById(_parkingSlot[0].Id), Times.Once);
    }

    [Fact]
    public void GivenParkingSlotId_WhenGetDeleteIsCalled_ThenReturnsViewResult()
    {
        //Arrange
        _parkingSlotServiceMoq
                .Setup(x => x.GetById(_parkingSlot[0].Id))
                .Returns(_parkingSlot[0]);

        //Act
        var result = _parkingSlotController.Delete(_parkingSlot[0].Id);

        //Assert
        var model = Assert.IsType<ViewResult>(result).Model;
        Assert.Equal(JsonSerializer.Serialize(model), JsonSerializer.Serialize(_parkingSlot[0]));
        _parkingSlotServiceMoq.Verify(x => x.GetById(_parkingSlot[0].Id), Times.Once);
    }

    [Fact]
    public void GivenParkingSlotId_WhenPostDeleteIsCalled_ThenReturnsNotFoundResult()
    {
        //Arrange
        _parkingSlotServiceMoq
            .Setup(x => x.GetById(_parkingSlot[0].Id));

        //Act
        var result = _parkingSlotController.Delete(_parkingSlot[0].Id);

        //Assert
        Assert.IsType<NotFoundResult>(result);
        _parkingSlotServiceMoq.Verify(x => x.GetById(_parkingSlot[0].Id), Times.Once);
    }

    [Fact]
    public void GivenParkingSlotId_WhenPostDeleteIsCalled_ThenReturnsRedirectToActionResult()
    {
        //Arrange
        _parkingSlotServiceMoq
            .Setup(x => x.GetById(_parkingSlot[0].Id))
            .Returns(_parkingSlot[0]);
        _parkingSlotServiceMoq.Setup(x => x.Delete(_parkingSlot[0]));

        //Act
        var result = _parkingSlotController.DeleteConfirmed(_parkingSlot[0].Id);

        //Assert
        Assert.NotNull(result);
        Assert.Equal("Index", ((RedirectToActionResult)result).ActionName);
        _parkingSlotServiceMoq.Verify(x => x.GetById(_parkingSlot[0].Id), Times.Once);
        _parkingSlotServiceMoq.Verify(x => x.Delete(_parkingSlot[0]), Times.Once);
    }
    #endregion
}
