using ConceptsDB.Data;
using ConceptsDB.Models;
using Microsoft.EntityFrameworkCore;

namespace ConceptsDB.Repository;

public class StudentRepository : IStudentRepository
{
    private readonly AppDbContext _context;
    
    public StudentRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task CreateStudent(Student student)
    {
        Student newStudent = new()
        {
            Name = student.Name,
            Email = student.Email,
        };

        await _context.AddAsync(newStudent);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Student>> ListStudents()
    {
        var students = await _context.Students
            .AsNoTracking()
            .ToListAsync();

        return students;
    }

    public async Task<Student?> GetStudentByEmail(string email)
    {
        var student = await _context.Students.FirstOrDefaultAsync(student => student.Email == email);

        return student;
    }

    public async Task<Student?> GetStudentById(int id)
    {
        var student = await _context.Students.FirstOrDefaultAsync(student => student.Id == id);

        return student;
    }

    public async Task UpdateStudent(Student student)
    {
        await _context.Students
            .Where(students => students.Id == student.Id)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(s => s.Name, student.Name)
                .SetProperty(s => s.Email, student.Email)
            );
    }

    public async Task DeleteStudent(int id)
    {
        await _context.Students
            .Where(student => student.Id == id)
            .Include(student => student.Matriculations)
            .ExecuteDeleteAsync();
    }
}