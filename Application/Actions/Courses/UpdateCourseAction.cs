using ConceptsDB.Application.Menu;
using ConceptsDB.Dto.Course;
using ConceptsDB.Services;
using ConceptsDB.Utils;

namespace ConceptsDB.Application.Actions.Courses;

public class UpdateCourseAction(CourseService service) : IMenuAction
{
    public string Key => "12";
    public string Description => "Atualizar curso";
    
    public async Task ExecuteAsync()
    {
        Console.Write("Id do curso: "); 
        var idCourse = Console.ReadLine()!;
    
        Console.Write("Nome do curso: ");
        var nameCourse = Console.ReadLine()!;

        try
        {
            var idCourseFormatted = VerifyToNumberReceived.ParseInt(idCourse);

            if (idCourseFormatted == null)
            {
                return;
            }

        
            var updateCouseDto = new UpdateCourseDto()
            {
                IdCourse = idCourseFormatted.Value,
                NameCourse = nameCourse
            };
        
            await service.UpdateCourse(updateCouseDto);
        
            Console.WriteLine("Curso atualizado com sucesso.");
        }   
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
        }

    }
}