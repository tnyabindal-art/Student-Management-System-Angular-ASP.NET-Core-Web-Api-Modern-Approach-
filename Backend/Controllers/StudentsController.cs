
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Student_Management_System_2.Models;
using Student_Management_System_2.Data;


namespace Student_Management_System_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {

        private readonly StudentDbContext dbContext;

        public StudentsController(StudentDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await dbContext.Students.ToListAsync();

            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentbyId(Guid id)
        {
            var student = await dbContext.Students.FindAsync(id);
            
            if (student == null)
            {
                return NotFound();
            }
            await dbContext.SaveChangesAsync();
            return Ok(student);

        }




        [HttpPost]
        public async Task<IActionResult> AddStudent(Student student)
        {
            student.Id = Guid.NewGuid();
            dbContext.Students.Add(student);
            await dbContext.SaveChangesAsync();
            return Ok(student);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Updatestudent(Guid id, Student updatedstudent)
        {
            var student = await dbContext.Students.FindAsync(id);

            if (student == null)

            {
                return NotFound();
            }

            student.Name = updatedstudent.Name;
            student.Email = updatedstudent.Email;
            student.Course = updatedstudent.Course;

            await dbContext.SaveChangesAsync();
            return Ok(student);
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Deletestudent(Guid id)
        {
            var student = await dbContext.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            dbContext.Students.Remove(student);
            await dbContext.SaveChangesAsync();
            return Ok(student);

        }


    }
}

