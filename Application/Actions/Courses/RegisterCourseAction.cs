using ConceptsDB.Application.Menu;
using ConceptsDB.Dto.Course;
using ConceptsDB.Services;

namespace ConceptsDB.Application.Actions.Courses;

public class RegisterCourseAction(CourseService service) : IMenuAction
{
    public string Key => "9";
    public string Description => "Registrar curso";
    public async Task ExecuteAsync()
    {
        Console.Write("Nome do curso: ");
        var nameCourse = Console.ReadLine()!;
    
        try
        {
            var registerCourseDto = new RegisterCourseDto()
            {
                Name = nameCourse,
            };
        
            await service.RegisterCourse(registerCourseDto);
            Console.WriteLine("Curso cadastrado com sucesso.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }
}