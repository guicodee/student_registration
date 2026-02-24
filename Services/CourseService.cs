using ConceptsDB.Models;
using ConceptsDB.Repository.Courses;

namespace ConceptsDB.Services;

public class CourseService
{
    private readonly ICourseRepository _courseRepository;

    public CourseService(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task RegisterCourse(Course course)
    {
        var courseAlreadyExists = await GetCourseByName(course.Name);

        if (courseAlreadyExists != null)
        {
            throw new Exception("Esse curso já está registrado no sistema.");
        }

        await _courseRepository.RegisterCourse(course);
    }

    public async Task<List<Course>> ListCourses()
    {
        var courses = await _courseRepository.ListCourses();

        if (courses.Count == 0)
        {
            throw new Exception("Nenhum curso registrado no sistema.");
        }

        return courses;
    }

    public async Task<Course?> GetCourseById(int id)
    {
        var course = await _courseRepository.GetCourseById(id);

        return course;
    }

    public async Task<Course?> GetCourseByName(string courseName)
    {
        var course = await _courseRepository.GetCourseByName(courseName);

        return course;
    }

    public async Task UpdateCourse(Course course)
    {
        var courseAlreadyExists = await GetCourseById(course.Id);
        
        if (courseAlreadyExists == null)
        {
            throw new Exception("Nenhum curso com esse ID encontrado no sistema.");
        }
        
        await _courseRepository.UpdateCourse(course);
    }

    public async Task DeleteCourse(int idCourse)
    {
        var courseAlreadyExists = await GetCourseById(idCourse);
        
        if (courseAlreadyExists == null)
        {
            throw new Exception("Nenhum curso com esse ID encontrado no sistema.");
        }
        
        await _courseRepository.DeleteCourse(idCourse);
    }
}