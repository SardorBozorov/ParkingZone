using Parking_Zone.MVC.Models.ParkingZoneVMs;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Parking_Zone.Test.ModelValidationtests.ParkingZones;

public class CreateVMTest
{
    public static IEnumerable<object[]> TestData =>
            new List<object[]>
            {
                new object[] { null, "test 1", false },
                new object[] { "test 2", null, false },
                new object[] { "test 3", "test 3", true }
            };

    [Theory]
    [MemberData(nameof(TestData))]
    public void GivenItemToValidate_WhenCreatingCreateVM_ThenValidationIsPerformed(
        string name, string address, bool expectedValidation)
    {
        // Arrange
        var createVM = new CreateVM
        {
            Name = name,
            Address = address
        };

        var validationContext = new ValidationContext(createVM, null, null);
        var validationResult = new List<ValidationResult>();

        // Act
        var result = Validator.TryValidateObject(createVM, validationContext, validationResult);

        // Assert
        Assert.Equal(expectedValidation, result);
    }
}
