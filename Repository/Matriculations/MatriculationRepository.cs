using ConceptsDB.Data;
using ConceptsDB.Models;
using Microsoft.EntityFrameworkCore;

namespace ConceptsDB.Repository.Matriculations;

public class MatriculationRepository : IMatriculationRepository
{
    private readonly AppDbContext _context;

    public MatriculationRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task EnrollStudent(Matriculation matriculation)
    {
        await _context.Matriculations.AddAsync(matriculation);
        await _context.SaveChangesAsync();
    }

    public async Task UnenrollStudent(int idStudent, int idCourse)
    {
        await _context.Matriculations
            .Where(matriculation => matriculation.StudentId == idStudent && matriculation.CourseId == idCourse)
            .ExecuteDeleteAsync();
    }

    public async Task<bool> IsAlreadyEnrolled(int idStudent, int idCourse)
    {
        var studentEnrolled = await _context.Matriculations
            .AnyAsync(matriculation => matriculation.StudentId == idStudent && matriculation.CourseId == idCourse);

        return studentEnrolled;
    }

    public async Task<List<Matriculation>> GetMatriculationsOfStudent(int idStudent)
    {
        var matriculationStudent = await _context.Matriculations
            .AsNoTracking()
            .Include(m => m.Course)
            .Include(m => m.Student)
            .Where(matriculation => matriculation.StudentId == idStudent)
            .ToListAsync();

        return matriculationStudent;
    }
}