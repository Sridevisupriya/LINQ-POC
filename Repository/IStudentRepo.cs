using LearnStudentAPI.Models;

namespace LearnStudentAPI.Repository
{
    public interface IStudentRepo
    {
        List<Student> GetStudents();
        Student GetStudent(int id);

        Course GetCourseByStudentId(int studentId);

        List<Student> GetStudentsByCourseId(int courseId);

        List<Course> GetCoursesWithMinStudents(int minStudents);
        List<Student> GetStudentsByGender(string gender);
        List<Object> GetCoursesWithStudents();
        List<Course> GetCourseWithNoStudents();
        List<Object> GetStudentsWithCourseNamesWithCourseBlockName();

        Task<string> AddStudent(Student student);

        List<object> GetStudentsAndCourseDetailsUsingRawQuery();
    }
}
