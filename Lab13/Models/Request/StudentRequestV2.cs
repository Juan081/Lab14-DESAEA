namespace Lab13.Models.Request
{
    public class StudentRequestV2
    {
        public int GradeID { get; set; }
        public List<Student> Students { get; set; }
        public List<Course> Courses { get; set; }
    }
}
