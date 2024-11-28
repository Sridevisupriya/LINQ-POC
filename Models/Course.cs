using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearnStudentAPI.Models
{
    public class Course
    {       
        public int CourseId { get; set; }
        public string? CourseName { get; set; }
        public string? Instructor { get; set; }
        public int LecturesCount { get; set; }
    }
}
