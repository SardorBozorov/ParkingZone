using Parking_Zone.Domain.Enums;
using Parking_Zone.MVC.Models.ParkingSlotVMs;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Parking_Zone.Test.ModelValidationtests.ParkingSlots;

public class EditVMTest
{
    public static IEnumerable<object[]> TestData =>
    new List<object[]>
    {
                new object[] { 1 ,5, ParkingSlotCategory.VIP, false, 20, true },
    };

    [Theory]
    [MemberData(nameof(TestData))]
    public void GivenItemToBeValidated_WhenEditSlot_ThenValidationIsPerformed
        (long id, uint number, ParkingSlotCategory category, bool isAvailable, long parkingZoneId, bool expectedValidation)
    {
        //Arrange
        EditVM listItemVM = new EditVM()
        {
            Number = number,
            Category = category,
            IsAvailable = isAvailable,
            ParkingZoneId = parkingZoneId,
            Id = id
        };

        var validationContext = new ValidationContext(listItemVM);
        var validationResult = new List<ValidationResult>();

        //Act
        bool result = Validator.TryValidateObject(listItemVM, validationContext, validationResult);

        //Assert
        Assert.Equal(expectedValidation, result);
    }
}
