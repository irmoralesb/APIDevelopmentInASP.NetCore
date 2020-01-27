using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Controllers
{
    [ApiController]
    [Route("api/authors")] //For Web APIs it is better only to have the same 
    //[Route("api/[controller]")] //Not recommended since we want to keep the API the same, but evaluate it if that is what you want
    public class AuthorsController : ControllerBase
    {
        private readonly ICourseLibraryRepository _courseLibraryRepository;


        public AuthorsController(ICourseLibraryRepository courseLibraryRepository)
        {
            _courseLibraryRepository = courseLibraryRepository ?? throw new ArgumentNullException(nameof(courseLibraryRepository));
        }

        [HttpGet()]
        public IActionResult GetAuthors()
        {
            var authorsFromRepo = _courseLibraryRepository.GetAuthors();
            return Ok(authorsFromRepo);
        }

        [HttpGet("{authorId:guid}")]
        public IActionResult GetAuthor(Guid authorId)
        {
            // This way is not recomended for high concurrency scenarios, the resource may be deleted between the 2 db calls
            //if (!_courseLibraryRepository.AuthorExists(authorId))
            //{ return NotFound(); }

            // This way, only one DB call
            var authorFromRepo = _courseLibraryRepository.GetAuthor(authorId: authorId);

            if (authorFromRepo == null)
            { return NotFound(); }

            return Ok(authorFromRepo);
        }

    }
}
