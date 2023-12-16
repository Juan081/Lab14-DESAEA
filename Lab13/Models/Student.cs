using System.ComponentModel.DataAnnotations;

namespace Lab13.Models
{
    public class Student
    {
        [Key]
        public int idStudent { get; set; }
        public int Grade_idGrade { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        // Relación con Grade
        public Grade Grade { get; set; }

        // Clave foránea
        public int GradeidGrade { get; set; }
    }
}
