using Moq;
using Parking_Zone.Data.IRepositories;
using Parking_Zone.Domain.Entities;
using Parking_Zone.Service.Interfaces;
using Parking_Zone.Service.Services;
using System.Security.AccessControl;
using System.Text.Json;
using Xunit;

namespace Parking_Zone.Test.Services;

public class ParkingZoneServiceTest
{
    private readonly Mock<IParkingZoneRepository> _parkingZoneRepository;
    private readonly IParkingZoneService _parkingZoneService;
    private readonly ParkingZone _parkingZone;
    private readonly long id = 1;

    public ParkingZoneServiceTest()
    {
       _parkingZoneRepository = new Mock<IParkingZoneRepository>();
       _parkingZoneService = new ParkingZoneService(_parkingZoneRepository.Object);
        _parkingZone = new()
        {
            Id = id,
            Name = "Test",
            Address = "Tashkent"
        };
    }

    #region Insert
    [Fact]
    public void GivenParkingZoneModel_WhenInsertIsCalled_ThenRepositoryCreateIsCalled()
    {
        //Arrange 
        _parkingZoneRepository.Setup(x => x.Create(_parkingZone));

        //Act
        _parkingZoneService.Create(_parkingZone);

        //Assert
        _parkingZoneRepository.Verify(x => x.Create(_parkingZone), Times.Once());
    }
    #endregion

    #region Update
    [Fact]
    public void GivenParkingZoneModel_WhenUpdateIsCalled_ThenRepositoryUpdateIsCalled()
    {
        //Arrange
        _parkingZoneRepository.Setup(x => x.Update(_parkingZone));

        //Act
        _parkingZoneService.Update(_parkingZone);

        //Assert
        _parkingZoneRepository.Verify(x => x.Update(_parkingZone), Times.Once());
    }
    #endregion

    #region Delete
    [Fact]
    public void GivenId_WhenRemoveIsCalled_ThenRepositoryDeleteIsCalled()
    {
        //Arrange
        _parkingZoneRepository.Setup(x => x.Delete(_parkingZone));

        //Act 
        _parkingZoneService.Delete(_parkingZone);

        //Assert
        _parkingZoneRepository.Verify(x => x.Delete(_parkingZone), Times.Once());
    }
    #endregion


    #region GetAll
    [Fact]
    public void GivenNothing_WhenRetrieveAllIsCalled_ThenReturnsListOfParkingZones()
    {
        //Arrange
        IEnumerable<ParkingZone> expectedZones = new List<ParkingZone>() { _parkingZone };

        _parkingZoneRepository
            .Setup(x => x.GetAll())
            .Returns(expectedZones);

        //Act
        var result = _parkingZoneService.GetAll();

        //Assert
        Assert.IsAssignableFrom<IEnumerable<ParkingZone>>(result);
        Assert.Equal(JsonSerializer.Serialize(expectedZones), JsonSerializer.Serialize(result));
        _parkingZoneRepository.Verify(x => x.GetAll(), Times.Once());
    }
    #endregion

    #region GetById
    [Fact]
    public void GivenId_WhenRetrieveByIdIsCalled_ThenReturnsParkingZoneModel()
    {
        //Arrange
        _parkingZoneRepository
            .Setup(x => x.GetById(id))
            .Returns(_parkingZone);

        //Act
        var result = _parkingZoneService.GetById(id);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<ParkingZone>(result);
        Assert.Equal(JsonSerializer.Serialize(_parkingZone), JsonSerializer.Serialize(result));
        _parkingZoneRepository.Verify(x => x.GetById(id), Times.Once());
    }
    #endregion
}
