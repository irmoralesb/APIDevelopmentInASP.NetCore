using CourseLibrary.API.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Models
{
    //these validations returns  400 error code instead of 422 , so a custom code was added to Startup.cs file
    [CourseTitleMustBeDifferentFromDescriptionAttributte(ErrorMessage = "Title shoud be different from description.")] //This validation is executed all along with the other property validations
    public abstract class CourseForManipulationDto : IValidatableObject //The validation implemented by this interface ONLY is executed when the property validations are executed and passed
    {
        [Required(ErrorMessage = "You should fill out a title.")]
        [MaxLength(100, ErrorMessage = "The title should't have more than 100 characters.")]
        public string Title { get; set; }
        [MaxLength(1500, ErrorMessage = "The description should't have more than 1500 characters.")]
        public virtual string Description { get; set; }

        // This works but it is executed only when the attributes for properties are executed and passed.
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Title == Description)
            {
                yield return new ValidationResult("The provided description should be different from the title.", new[] { "CourseForCreationDto" });
            }
        }

    }
}
