using Parking_Zone.Domain.Enums;
using Parking_Zone.MVC.Models.ParkingSlotVMs;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Parking_Zone.Test.ModelValidationtests.ParkingSlots;

public class ListOfSlotsVMTest
{
    public static IEnumerable<object[]> TestData =>
      new List<object[]>
      {
                new object[] { 1, null, new ParkingSlotCategory(), false, 10, false },
                new object[] { 3, "30", new ParkingSlotCategory(), false, 20, true },
      };

    [Theory]
    [MemberData(nameof(TestData))]
    public void GivenItemToBeValidated_WhenCreatingListItemVM_ThenValidationIsPerformed
        (long id, string number, ParkingSlotCategory category, bool isAvailable, long parkingZoneId, bool expectedValidation)
    {
        //Arrange
        ListOfSlotsVM listItemVM = new ListOfSlotsVM()
        {
            Id = id,
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
