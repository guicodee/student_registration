using ConceptsDB.Dto.Course;
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

    public async Task RegisterCourse(RegisterCourseDto course)
    {
        var courseAlreadyExists = await GetCourseByName(course.Name);

        if (courseAlreadyExists != null)
        {
            throw new Exception("Esse curso já está registrado no sistema.");
        }
        
        var newCourse = new Course()
        {
            Name = course.Name,
            Matriculations = new List<Matriculation>()
        };

        await _courseRepository.RegisterCourse(newCourse);
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

    public async Task UpdateCourse(UpdateCourseDto course)
    {
        var courseAlreadyExists = await GetCourseById(course.IdCourse);
        
        if (courseAlreadyExists == null)
        {
            throw new Exception("Nenhum curso com esse ID encontrado no sistema.");
        }
        
        var updateCourse = new Course()
        {
            Id = course.IdCourse,
            Name = course.NameCourse
        };

        
        await _courseRepository.UpdateCourse(updateCourse);
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