using ConceptsDB.Application.Menu;
using ConceptsDB.Services;

namespace ConceptsDB.Application.Actions.Courses;

public class GetCourseByNameAction(CourseService service) : IMenuAction
{
    public string Key => "11";
    public string Description => "Buscar curso por nome";
    public async Task ExecuteAsync()
    {
        Console.Write("Nome do curso: ");
        var nameCourse = Console.ReadLine()!;
    
        try
        {
            var course = await service.GetCourseByName(nameCourse);

            if (course == null)
            {
                Console.WriteLine("Nenhum curso encontrado.");
                return;
            }
        
            Console.WriteLine($"{course.Id} | {course.Name} | {course.Matriculations.Count()} alunos matriculados ");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }
}