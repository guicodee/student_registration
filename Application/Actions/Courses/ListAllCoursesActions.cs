using ConceptsDB.Application.Menu;
using ConceptsDB.Services;

namespace ConceptsDB.Application.Actions.Courses;

public class ListAllCoursesActions(CourseService service) : IMenuAction
{
    public string Key => "10";
    public string Description => "Listar todos os cursos"; 

    public async Task ExecuteAsync()
    {
        try
        {
            var courses = await service.ListCourses();

            if (courses.Count == 0)
            {
                Console.WriteLine("Nenhum curso cadastrado no sistema.");
                return;
            }

            foreach (var course in courses)
            {
                Console.WriteLine($"{course.Id} | {course.Name} | {course.Matriculations.Count()} alunos matriculados");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");    
        }
    }
}