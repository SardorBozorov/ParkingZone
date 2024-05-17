using Parking_Zone.Domain.Enums;
using Parking_Zone.MVC.Models.ParkingSlotVMs;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Parking_Zone.Test.ModelValidationtests.ParkingSlots;

public class CreateVMTest
{
    public static IEnumerable<object[]> TestData =>
      new List<object[]>
      {
                new object[] { 5, ParkingSlotCategory.VIP, false, 20, true },
      };

    [Theory]
    [MemberData(nameof(TestData))]
    public void GivenItemToBeValidated_WhenCreateNewSlot_ThenValidationIsPerformed
        ( uint number, ParkingSlotCategory category, bool isAvailable, long parkingZoneId, bool expectedValidation)
    {
        //Arrange
        CreateVM listItemVM = new CreateVM()
        { 
            Number = number,
            Category = category,
            IsAvailable = isAvailable,
            ParkingZoneId = parkingZoneId
        };

        var validationContext = new ValidationContext(listItemVM);
        var validationResult = new List<ValidationResult>();

        //Act
        bool result = Validator.TryValidateObject(listItemVM, validationContext, validationResult);

        //Assert
        Assert.Equal(expectedValidation, result);
    }
}
