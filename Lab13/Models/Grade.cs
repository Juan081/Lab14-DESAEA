using System.ComponentModel.DataAnnotations;

namespace Lab13.Models
{
    public class Grade
    {
        [Key]
        public int idGrade { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // Relación con Student
        public ICollection<Student> Students { get; set; }
    }
}
