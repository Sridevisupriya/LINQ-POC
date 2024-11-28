using LearnStudentAPI.Models;
using LearnStudentAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LearnStudentAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : Controller
    {
        private readonly IStudentRepo _studentRepo;

        public StudentController(IStudentRepo studentRepo)
        {
            _studentRepo = studentRepo;
        }
       
        [HttpGet]
        public List<Student> GetStudents()
        {
            var result = _studentRepo.GetStudents();
            return result;
        }

        [HttpGet("StudentById/{id}")]
        public Student GetStudentById(int id)
        {
            var result = _studentRepo.GetStudent(id);
            return result;
        }

        [HttpGet("GetCouseDetailsByStudentId/{id}")]
        public Course GetCouseDetailsByStudentId(int id)
        {
            var result = _studentRepo.GetCourseByStudentId(id);
            return result;
        }

        [HttpGet("GetStudentsByCourseId")]
        public List<Student> GetStudentsByCourseId(int courseId)
        {
            return _studentRepo.GetStudentsByCourseId(courseId);
        }

        [HttpGet("GetCoursesWithMinStudents")]
        public List<Course> GetCoursesWithMinStudents(int minStudents)
        {
            return _studentRepo.GetCoursesWithMinStudents(minStudents);
        }

        [HttpGet("GetStudentsByGender")]
        public List<Student> GetStudentsByGender(string gender)
        {
            return _studentRepo.GetStudentsByGender(gender);
        }

        [HttpGet("GetCoursesWithStudents")]
        public List<object> GetCoursesWithStudents()
        {
            return _studentRepo.GetCoursesWithStudents();
        }

        [HttpGet("GetCourseWithNoStudents")]
        public List<Course> GetCourseWithNoStudents()
        {
            return _studentRepo.GetCourseWithNoStudents();
        }

        [HttpGet("GetStudentsWithCourseNamesWithCourseBlockName")]
        public List<object> GetStudentsWithCourseNamesWithCourseBlockName()
        {
            return _studentRepo.GetStudentsWithCourseNamesWithCourseBlockName();
        }

        [HttpPost("AddStudent")]
        public async Task<string> AddStudentAsync(Student student)
        {
            return await _studentRepo.AddStudent(student);
        }

        [HttpGet("GetStudentsAndCourseDetailsUsingRawQuery")]
        public List<object> GetStudentsAndCourseDetailsUsingRawQuery()
        {
            return  _studentRepo.GetStudentsAndCourseDetailsUsingRawQuery();
        }
    }
}
