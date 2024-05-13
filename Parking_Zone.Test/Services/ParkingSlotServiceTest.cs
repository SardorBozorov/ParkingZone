using FluentAssertions.Common;
using Moq;
using Parking_Zone.Data.IRepositories;
using Parking_Zone.Data.Repositories;
using Parking_Zone.Domain.Entities;
using Parking_Zone.Domain.Enums;
using Parking_Zone.Service.Interfaces;
using Parking_Zone.Service.Services;
using System.Security.Cryptography;
using System.Text.Json;
using Xunit;

namespace Parking_Zone.Test.Services;

public class ParkingSlotServiceTest
{
    private readonly Mock<IParkingSlotRepository> _parkingSlotRepository;
    private readonly IParkingSlotService _parkingSlotService;
    private readonly ParkingSlot _parkingSlot;
    private readonly long id = 1;
    public ParkingSlotServiceTest()
    {
        _parkingSlotRepository = new Mock<IParkingSlotRepository>();
        _parkingSlotService = new ParkingSlotService(_parkingSlotRepository.Object);
        _parkingSlot = new()
        {
            Id = id,
            Number = 10,
            Category = ParkingSlotCategory.Start,
            IsAvailable = false,
            ParkingZoneId = id
        };
    }
    #region Insert
    [Fact]
    public void GivenParkingSlotModel_WhenInsertIsCalled_ThenRepositoryCreateIsCalled()
    {
        //Arrange 
        _parkingSlotRepository.Setup(x => x.Create(_parkingSlot));

        //Act
        _parkingSlotService.Create(_parkingSlot);

        //Assert
        _parkingSlotRepository.Verify(x => x.Create(_parkingSlot), Times.Once());
    }
    #endregion

    #region Update
    [Fact]
    public void GivenParkingSlotModel_WhenUpdateIsCalled_ThenRepositoryUpdateIsCalled()
    {
        //Arrange
        _parkingSlotRepository.Setup(x => x.Update(_parkingSlot));

        //Act
        _parkingSlotService.Update(_parkingSlot);

        //Assert
        _parkingSlotRepository.Verify(x => x.Update(_parkingSlot), Times.Once());
    }
    #endregion

    #region Delete
    [Fact]
    public void GivenId_WhenRemoveIsCalled_ThenRepositoryDeleteIsCalled()
    {
        //Arrange
        _parkingSlotRepository.Setup(x => x.Delete(_parkingSlot));

        //Act 
        _parkingSlotService.Delete(_parkingSlot);

        //Assert
        _parkingSlotRepository.Verify(x => x.Delete(_parkingSlot), Times.Once());
    }
    #endregion

    #region GetAll
    [Fact]
    public void GivenNothing_WhenGetAllIsCalled_ThenReturnsListOfParkingSlots()
    {
        //Arrange
        IEnumerable<ParkingSlot> expectedZones = new List<ParkingSlot>() { _parkingSlot };

        _parkingSlotRepository
            .Setup(x => x.GetAll())
            .Returns(expectedZones);

        //Act
        var result = _parkingSlotService.GetAll();

        //Assert
        Assert.IsAssignableFrom<IEnumerable<ParkingSlot>>(result);
        Assert.Equal(JsonSerializer.Serialize(expectedZones), JsonSerializer.Serialize(result));
        _parkingSlotRepository.Verify(x => x.GetAll(), Times.Once());
    }
    #endregion

    #region GetById
    [Fact]
    public void GivenId_WhenGetByIdIsCalled_ThenReturnsParkingSlotModel()
    {
        //Arrange
        _parkingSlotRepository
            .Setup(x => x.GetById(id))
            .Returns(_parkingSlot);

        //Act
        var result = _parkingSlotService.GetById(id);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<ParkingSlot>(result);
        Assert.Equal(JsonSerializer.Serialize(_parkingSlot), JsonSerializer.Serialize(result));
        _parkingSlotRepository.Verify(x => x.GetById(id), Times.Once());
    }
    #endregion

    #region GetAll by ParkingZoneId
    [Fact]
    public void GivenId_WhenGetByParkingZoneIdIsCalled_ThenReturnsParkingSlots()
    {
        //Arrange
        var expected = new List<ParkingSlot>() { _parkingSlot };
        _parkingSlotRepository.Setup(x => x.GetAll()).Returns(new List<ParkingSlot>() { _parkingSlot });

        //Act
        var result = _parkingSlotService.GetSlotsByZoneId(id);

        //Assert
        Assert.IsAssignableFrom<IEnumerable<ParkingSlot>>(result);
        Assert.Equal(JsonSerializer.Serialize(result), JsonSerializer.Serialize(expected));
        Assert.NotNull(result);
        _parkingSlotRepository.Verify(x => x.GetAll(), Times.Once);
    }
    #endregion
}
