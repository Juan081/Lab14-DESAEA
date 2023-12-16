using Lab13.Controllers;
using Lab13.Models;
using Lab13.Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab13.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentCustomController : ControllerBase
    {




        private readonly SchoolContext _context;
        public StudentCustomController(SchoolContext context)
        {
            _context = context;
        }
        // GET: api/Student

        [HttpPost(Name = "UpdateContacts")]
        public void UpdateContacts(StudentRequestV1 request)
        {
            //Busco al estudiante que voy a editar
            var student = _context.Students.Find(request.Id);

            //Cambio los valores
            student.Email = request.Email;
            student.Phone = request.Phone;

            //Hago la transacciÃ³n
            _context.Entry(student).State = EntityState.Modified;
            _context.SaveChanges();
        }

        [HttpPost(Name = "InsertByGrade")]
        public void InsertByGrade(StudentRequestV2 request)
        {


            var students = request.Students.Select(x => new Student
            {
                Email = x.Email,
                FirstName = x.FirstName,
                Phone = x.Phone,
                Grade = x.Grade,
                Grade_idGrade = request.GradeID
            }).ToList();

            _context.Students.AddRange(students);
            _context.SaveChanges();




        }




        [HttpGet(Name = "GetByFilters")]
        public List<Student> GetByFilters(string firstName, string lastName, string email)
        {

            List<Student> response = _context.Students.
                Where(x => x.FirstName.Contains(firstName)
                || x.LastName.Contains(lastName)
                || x.Email.Contains(email)
                )
                .OrderByDescending(x => x.LastName)
                .ToList();

            return response;
        }

        [HttpGet(Name = "GetWithGrade")]
        public List<Student> GetWithGrade(string firstName, string grade)
        {

            List<Student> response = _context.Students.
                 Include(x => x.Grade)
                .Where(x => x.FirstName.Contains(firstName)
                         || x.Grade.Name.Contains(grade))
                .OrderByDescending(x => x.LastName)
                .ToList();

            return response;
        }

        [HttpGet(Name = "GetEnrollment")]
        public List<Enrollment> GetEnrollment()
        {

            var response = _context.Enrollments
                .Include(x => x.Student)
                .ThenInclude(x => x.Grade)
                .ToList();
            return response;
        }

        [HttpPost("InsertarEstudiante")]
        public IActionResult InsertarEstudiante([FromBody] StudentRequestV1 request)
        {
            
            var nuevoEstudiante = new Student
            {
                Grade_idGrade = request.Grade,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Phone = request.Phone,
                Email = request.Email
            };

            
            _context.Students.Add(nuevoEstudiante);
            _context.SaveChanges();

            return Ok("Estudiante insertado exitosamente");
        }
        [HttpPost("InsertarCurso")]
        public IActionResult InsertarCurso([FromBody] CursoRequest request)
        {
            // Validar los datos de la solicitud
            if (!ModelState.IsValid)
            {
                return BadRequest("Los datos no son válidos");
            }

            var nuevoCurso = new Course
            {
                Name = request.Name,
                Credit = request.Credit
            };

            _context.Courses.Add(nuevoCurso);
            _context.SaveChanges();

            return Ok("Curso insertado exitosamente");
        }

        [HttpDelete("EliminarCurso")]
        public IActionResult EliminarCurso([FromBody] CursoRequestV2 request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("El id del curso no es válido");
            }

            var idCurso = request.Id;

            var curso = _context.Courses.Find(idCurso);

            if (curso == null)
            {
                return NotFound("El curso no existe");
            }

            _context.Courses.Remove(curso);
            _context.SaveChanges();

            return Ok("Curso eliminado exitosamente");
        }

        [HttpPost("InsertarGrado")]
        public IActionResult InsertarGrado([FromBody] GradeRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Los datos proporcionados no son válidos");
            }

            var nuevoGrado = new Grade
            {
                Name = request.Name,
                Description = request.Description
            };

            _context.Grades.Add(nuevoGrado);
            _context.SaveChanges();

            return Ok("Grado insertado exitosamente");
        }
        [HttpDelete("EliminarGrado")]
        public IActionResult EliminarGrado([FromBody] GradeRequestV2 request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Los datos proporcionados no son válidos");
            }

            var idGrado = request.Id;
            var grado = _context.Grades.Find(idGrado);

            if (grado == null)
            {
                return NotFound("El grado no existe");
            }

            _context.Grades.Remove(grado);
            _context.SaveChanges();

            return Ok("Grado eliminado exitosamente");
        }
        [HttpPut("ActualizarDatosPersonales")]
        public IActionResult ActualizarDatosPersonales([FromBody] StudentRequestV1 request)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest("Los datos proporcionados no son válidos");
            }


            var student = _context.Students.Find(request.Id);

            if (student == null)
            {
                return NotFound("El usuario no existe");
            }

            student.FirstName = request.FirstName;
            student.LastName = request.LastName;

            _context.SaveChanges();

            return Ok("Datos personales actualizados exitosamente");
        }
        [HttpGet(Name = "EliminarListaCursos")]
        public IActionResult EliminarListaCursos([FromQuery] List<int> Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Los datos proporcionados no son válidos");
            }
            _context.Courses.RemoveRange(_context.Courses.Where(x => Id.Contains(x.idCourse)));

            _context.SaveChanges();

            return Ok("Lista de cursos eliminada exitosamente");
        }


    }

}
