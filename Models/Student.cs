namespace LearnStudentAPI.Models
{
    public class Student
    {        
        public int StudentId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public  int  CourseId { get; set; }
    }
}
