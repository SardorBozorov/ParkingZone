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
        _parkingSlotController = new ParkingSlotController(_parkingSlotServiceMoq.Object);
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
    }
    #endregion
}
