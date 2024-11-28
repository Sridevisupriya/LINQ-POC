using LearnStudentAPI.Models;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq;

namespace LearnStudentAPI.Repository
{
    public class StudentRepo : IStudentRepo
    {
        private readonly LearnDbContext _context;
        public StudentRepo(LearnDbContext context)
        {
            _context = context;
        }

        public List<Student> GetStudents()
        {
            var students = _context.Students.ToList();

            return students;
        }

        public Student GetStudent(int id)
        {
            //course lectures average
            var cl = _context.Courses.Average(x=> x.LecturesCount).ToString();

            //Order by
            var ol = _context.Courses.OrderBy(x => x.Instructor).ToList();

            //filter by lecture count > 20
            var fl = _context.Courses.Where(x => x.LecturesCount>20).ToList();

            // var student = _context.Students.FirstOrDefault(x => x.StudentId == id);

            var studentDetails = (from student in _context.Students
                          where student.StudentId == id
                          select student).FirstOrDefault();
            if (studentDetails == null)
                return null;
            return studentDetails;
        }

        public Course GetCourseByStudentId(int studentId)
        {
            //var studentDetailsWithCourseName = from student in _context.Students
            //                                   join course in _context.Courses on student.CourseId equals course.CourseId
            //                                   where student.StudentId == studentId
            //                                   select new
            //                                   {
            //                                       student.StudentId,
            //                                       student.FirstName,
            //                                       student.LastName,
            //                                       student.Gender,
            //                                       student.DateOfBirth,
            //                                       course.CourseName
            //                                   };

            //var studentDetailsWithCourseNameResult = studentDetailsWithCourseName.FirstOrDefault();

            //var noOfStudentsEnrolledCourseWise = from student in _context.Students
            //                                     group student by student.CourseId into studentGroup
            //                                     select new
            //                                     {
            //                                         CourseId = studentGroup.Key,
            //                                         Count = studentGroup.Count()
            //                                     };

            //var result = noOfStudentsEnrolledCourseWise.ToList();


            //var courseDetails = (from student in _context.Students
            //                     join course in _context.Courses on student.CourseId equals course.CourseId
            //                     where student.StudentId == studentId
            //                     select course).FirstOrDefault();

            var courseDetails = _context.Students
                                .Join(_context.Courses,
                                 student => student.CourseId,
                                 course => course.CourseId,
                                 (student, course) => new { student, course })
                                .Where(sc => sc.student.StudentId == studentId)
                                .Select(sc => sc.course).FirstOrDefault();
            return courseDetails;

        }

        public List<Student> GetStudentsByCourseId(int courseId)
        {
            var result = (from student in _context.Students
                          where student.CourseId == courseId
                         // orderby student.FirstName
                          select student).ToList();
            return result;
        }

        public List<Course> GetCoursesWithMinStudents(int minStudents)
        {
            //var result = //from course in _context.Courses
            //             (from student in _context.Students 
            //             group student by student.CourseId into studentGroup
            //             where studentGroup.Count() > minStudents
            //             join course in _context.Courses on studentGroup.Key equals course.CourseId  
            //             select course).ToList();


            var courses = _context.Courses
                            .Where(course => _context.Students
                                    .GroupBy(x => x.CourseId)
                                    .Where(xy => xy.Count() >= minStudents)
                                    .Select(p => p.Key).Contains(course.CourseId))
                            .ToList();

            return courses;
        }

        public List<Student> GetStudentsByGender(string gender)
        {
            var result = (from student in _context.Students
                          where student.Gender.ToLower() == gender.ToLower()
                          select student).ToList();
            return result;
        }

        public List<object> GetCoursesWithStudents()
        {
            var result = (from student in _context.Students
                          join course in _context.Courses on student.CourseId equals course.CourseId
                          select new
                          {
                              student.CourseId,
                              student.FirstName,
                              student.LastName,
                              course.CourseName,
                              course.LecturesCount,
                              course.Instructor
                          }).ToList<object>();
            return result;
        }

        public List<Course> GetCourseWithNoStudents()
        {
            var courseWithNoStudentsEnrolled = _context.Courses
                                               .Where(c => !(_context.Students.Any(s => s.CourseId == c.CourseId)))
                                               .Select(c => new
                                               {
                                                   c.CourseId
                                               }).ToList();

            var result = (from student in _context.Students
                          group student by student.CourseId into studentgroup
                          select new
                          {
                              CourseId = studentgroup.Key
                          }).ToList();
            var couresesIdList = result.Select(y => y.CourseId).ToList();
            var courseDetails = _context.Courses.Where(x => !couresesIdList.Contains(x.CourseId)).ToList();
            return courseDetails;

        }

        public List<object> GetStudentsWithCourseNamesWithCourseBlockName()
        {        


            var result = (from student in _context.Students
                         join course in _context.Courses on student.CourseId equals course.CourseId
                         join department in _context.Departments on student.StudentId equals department.StudentId
                         where department.DepartmentName == "CSE" && department.DepartmentName !="ECE"
                         select new
                         {
                             student.FirstName, student.LastName,
                             course.CourseName,course.Instructor,
                             department.DepartmentName,department.BlockName
                         }).ToList<object>();

            return result;

        }

        public async Task<string> AddStudent(Student student)
        {
            var firstName = new SqlParameter("@FirstName", student.FirstName);
            var lastName = new SqlParameter("@LastName",student.LastName);
            var dateOfBirth = new SqlParameter("@DateOfBirth", student.DateOfBirth);
            var gender = new SqlParameter("@Gender", student.Gender);
            var courseId = new SqlParameter("@CourseId", student.CourseId);
            var result = new SqlParameter("@Result", System.Data.SqlDbType.NVarChar, 10) { Direction = System.Data.ParameterDirection.Output };

            await _context.Database.ExecuteSqlRawAsync("EXEC InsertStudent @FirstName,@LastName,@DateOfBirth,@Gender,@CourseId,@Result OUTPUT",
                firstName, lastName, dateOfBirth, gender, courseId, result);
            
            return result.Value.ToString();
        }

        public List<object> GetStudentsAndCourseDetailsUsingRawQuery()
        {
            //FormattableString query = $@"Insert into [LearnDB].[dbo].[Courses] values ('Lambda','Anu',50)";

            //var result = _context.Database.ExecuteSqlInterpolated(query);

            var query = @"SELECT student.FirstName , student.LastName,course.CourseName,course.Instructor
                          FROM [LearnDB].[dbo].[Students] student
                          join [LearnDB].[dbo].[Courses] course on student.CourseId = course.CourseId;";
            var result = _context.Students.FromSqlRaw(query).ToList();

           // var result = _context.Set<StudentCourseDTO>().FromSqlRaw(query).ToList();   

            return new List<object> { result };
        }
    }
}
