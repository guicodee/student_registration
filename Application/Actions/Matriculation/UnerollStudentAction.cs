using ConceptsDB.Application.Menu;
using ConceptsDB.Services;
using ConceptsDB.Utils;

namespace ConceptsDB.Application.Actions.Matriculation;

public class UnerollStudentAction(MatriculationService service) : IMenuAction
{
    public string Key => "7";
    public string Description => "Desmatricular aluno";
    
    public async Task ExecuteAsync()
    {
        Console.Write("Id do aluno: ");
        var idStudent = Console.ReadLine()!;
        
        Console.Write("Id do curso: ");
        var idCourse = Console.ReadLine()!;

        try
        {
            var idStudentFormatted = VerifyToNumberReceived.ParseInt(idStudent);
            var idCourseFormatted = VerifyToNumberReceived.ParseInt(idCourse);

            if (idStudentFormatted == null ||  idCourseFormatted == null)
            {
                return;
            }

            await service
                .UnenrollStudent(
                    idStudentFormatted.Value,
                    idCourseFormatted.Value
                );
            
            Console.WriteLine("Aluno desmatriculado do curso com sucesso.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }
}