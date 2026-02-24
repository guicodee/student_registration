using ConceptsDB.Data;
using ConceptsDB.Models;
using Microsoft.EntityFrameworkCore;

namespace ConceptsDB.Repository.Courses;

public class CourseRepository : ICourseRepository
{
    private readonly AppDbContext _context;

    public CourseRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task RegisterCourse(Course course)
    {
        await _context.Courses.AddAsync(course);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Course>> ListCourses()
    {
        var courses = await _context.Courses
            .AsNoTracking()
            .Include(course => course.Matriculations)
            .OrderBy(course => course.Name)
            .ToListAsync();

        return courses;
    }

    public async Task<Course?> GetCourseById(int id)
    {
        var course = await _context.Courses.FirstOrDefaultAsync(course => course.Id == id);

        return course;
    }

    public async Task<Course?> GetCourseByName(string courseName)
    {
        var course = await _context.Courses
            .Include(course => course.Matriculations)
            .FirstOrDefaultAsync(course => course.Name == courseName);

        return course;
    }

    public async Task UpdateCourse(Course course)
    {
        await _context.Courses
            .Where(courses => courses.Id == course.Id)
            .ExecuteUpdateAsync(setters => 
                setters.SetProperty(c => c.Name, course.Name)
            );
    }

    public async Task DeleteCourse(int idCourse)
    {
        await _context.Courses
            .Where(course => course.Id == idCourse)
            .Include(course => course.Matriculations)
            .ExecuteDeleteAsync();
    }
}