using ConceptsDB.Application.Menu;
using ConceptsDB.Services;
using ConceptsDB.Utils;

namespace ConceptsDB.Application.Actions.Courses;

public class DeleteCourseAction(CourseService service) : IMenuAction
{
    public string Key => "13";
    public string Description => "Deletar curso";
    public async Task ExecuteAsync()
    {
        Console.Write("Id do curso: ");
        var idCourse = Console.ReadLine()!;
    
        try
        {
            var idFormatted = VerifyToNumberReceived.ParseInt(idCourse);

            if (idFormatted == null)
            {
                return;    
            }
        
            await service.DeleteCourse(idFormatted.Value);
        
            Console.WriteLine("Curso deletado com sucesso.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }
}