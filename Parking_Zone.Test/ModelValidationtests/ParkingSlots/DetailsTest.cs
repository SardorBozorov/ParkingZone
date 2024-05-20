using Parking_Zone.Domain.Enums;
using Parking_Zone.MVC.Models.ParkingSlotVMs;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Parking_Zone.Test.ModelValidationtests.ParkingSlots;

public class DetailsTest
{
    public static IEnumerable<object[]> TestData =>
        new List<object[]>
        {
            new object[] { 1, 5, ParkingSlotCategory.VIP, false, 20, true }
        };

    [Theory]
    [MemberData(nameof(TestData))]
    public void GIvenItemToBeValidated_WhenCreatingdetailsVM_ThenValidationIsPerformed
        (long id, uint number, ParkingSlotCategory category, bool isAvailable, long parkingZoneId, bool expectedValidation)
    {
        //Arrange
        DetailsVM detailsVM = new DetailsVM()
        {
            Id = id,
            Number = number,
            Category = category,
            IsAvailable = isAvailable,
            ParkingZoneId = parkingZoneId,
        };
        var validationContext = new ValidationContext(detailsVM);
        var validationResult = new List<ValidationResult>();

        //Act
        bool result = Validator.TryValidateObject(detailsVM, validationContext, validationResult);

        //Assert
        Assert.Equal(expectedValidation, result);

    }
}
