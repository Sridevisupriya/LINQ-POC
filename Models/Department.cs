namespace LearnStudentAPI.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string BlockName { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
    }
}
