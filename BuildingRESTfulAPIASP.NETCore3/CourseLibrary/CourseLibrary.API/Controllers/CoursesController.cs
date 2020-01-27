using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CourseLibrary.API.Models;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseLibrary.API.Controllers
{

    [ApiController]
    [Route("api/authors/{authorId}/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseLibraryRepository _courseLibraryRepository;
        private readonly IMapper _mapper;

        public CoursesController(ICourseLibraryRepository courseLibraryRepository, IMapper mapper)
        {
            _courseLibraryRepository = courseLibraryRepository ?? throw new ArgumentNullException(nameof(courseLibraryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public ActionResult<IEnumerable<CourseDTO>> GetCoursesForAuthor(Guid authorId)
        {
            if(!_courseLibraryRepository.AuthorExists(authorId))
            { return NotFound(); }

            var coursesForAuthorFromRepo = _courseLibraryRepository.GetCourses(authorId);
            var courses = _mapper.Map<IEnumerable<CourseDTO>>(coursesForAuthorFromRepo);

            return Ok(courses);
        }

        [HttpGet("{courseId}")]
        public ActionResult<CourseDTO> GetCourseForAuthor(Guid authorId, Guid courseId)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId))
            { return NotFound(); }

            var courseForAuthorFromRepo = _courseLibraryRepository.GetCourse(authorId, courseId);

            if (courseForAuthorFromRepo == null)
            { return NotFound(); }

            var course = _mapper.Map<CourseDTO>(courseForAuthorFromRepo);

            return Ok(course);
        }

    }
}