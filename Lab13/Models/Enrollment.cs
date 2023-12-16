using System.ComponentModel.DataAnnotations;

namespace Lab13.Models
{
    public class Enrollment
    {
        [Key]
        public int idEnrollment { get; set; }
        public int Student_idStudent { get; set; }
        public int Course_idCourse { get; set; }
        public DateTime Date { get; set; }

        // Relación con Student
        public Student Student { get; set; }

        // Relación con Course
        public Course Course { get; set; }
    }
}
