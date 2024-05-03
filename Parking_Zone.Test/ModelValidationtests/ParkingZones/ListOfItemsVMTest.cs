using Parking_Zone.MVC.Models.ParkingZoneVMs;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Parking_Zone.Test.ModelValidationtests.ParkingZones;

public class ListOfItemsVMTest
{
    public static IEnumerable<object[]> TestData =>
      new List<object[]>
      {
                new object[] { 1, null, "Test1", DateTime.Now, false },
                new object[] { 2, "Test3", null, DateTime.Now, false },
                new object[] { 3, "Test4", "Test4", null, false },
                new object[] { 4, "Test5", "Test5", DateTime.Now, true }
      };

    [Theory]
    [MemberData(nameof(TestData))]
    public void GivenItemToBeValidated_WhenCreatingDetailsVM_ThenValidationIsPerformed
        (long id, string name, string address, DateTime? createdDate, bool expectedValidation)
    {
        //Arrange
        var detailsVM = new ListOfItemsVM()
        {
            Id = id,
            Name = name,
            Address = address,
            DateOfEstablishment = createdDate
        };

        var validationContext = new ValidationContext(detailsVM, null, null);
        var validationResult = new List<ValidationResult>();

        //Act
        var isValidResult = Validator.TryValidateObject(detailsVM, validationContext, validationResult);

        //Assert
        Assert.Equal(expectedValidation, isValidResult);
    }
}
