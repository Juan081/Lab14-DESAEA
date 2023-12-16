using System.ComponentModel.DataAnnotations;

namespace Lab13.Models
{
    public class Course
    {
        [Key]
        public int idCourse { get; set; }
        public string Name { get; set; }
        public int Credit { get; set; }

        // Relación con Enrollment
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
