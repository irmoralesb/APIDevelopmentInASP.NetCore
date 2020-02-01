using CourseLibrary.API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.ValidationAttributes
{
    // Implementing this validation instead of implementing the validation from the interface in the class 
    // will allow us to validate at the sametime that the other property validations
    public class CourseTitleMustBeDifferentFromDescriptionAttributte : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            var course = (CourseForManipulationDto)validationContext.ObjectInstance;
            if (course.Title == course.Description)
            {
                return new ValidationResult("The provided description should be different from the title", new[] { nameof(CourseForManipulationDto) });
            }
            return ValidationResult.Success;
        }
    }
}
