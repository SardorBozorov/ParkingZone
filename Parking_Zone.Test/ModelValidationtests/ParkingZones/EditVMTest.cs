using Parking_Zone.MVC.Models.ParkingZoneVMs;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Parking_Zone.Test.ModelValidationtests.ParkingZones
{
    public class EditVMTest
    {
        public static IEnumerable<object[]> TestData =>
          new List<object[]>
          {
                new object[] {  null, "Test1", false },
                new object[] { "Test3", null, false },
                new object[] { "Test5", "Test5", true }
          };

        [Theory]
        [MemberData(nameof(TestData))]
        public void GivenItemToBeValidated_WhenCreatingDetailsVM_ThenValidationIsPerformed
            ( string name, string address, bool expectedValidation)
        {
            //Arrange
            EditVM editVM = new()
            {
                Name = name,
                Address = address,
            };

            var validationContext = new ValidationContext(editVM, null, null);
            var validationResult = new List<ValidationResult>();

            //Act
            var isValidResult = Validator.TryValidateObject(editVM, validationContext, validationResult);

            //Assert
            Assert.Equal(expectedValidation, isValidResult);
        }
    }
}
