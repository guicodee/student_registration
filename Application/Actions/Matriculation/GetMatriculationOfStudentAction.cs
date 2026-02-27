using System.Formats.Asn1;
using ConceptsDB.Application.Menu;
using ConceptsDB.Services;
using ConceptsDB.Utils;

namespace ConceptsDB.Application.Actions.Matriculation;

public class GetMatriculationOfStudentAction(MatriculationService service) : IMenuAction
{
    public string Key => "8";
    public string Description => "Buscar matricula do aluno";
    
    public async Task ExecuteAsync()
    {
        Console.Write("Id do aluno: ");
        var idStudent = Console.ReadLine()!;

        try
        {
            var idStudentFormatted = VerifyToNumberReceived.ParseInt(idStudent);

            if (idStudentFormatted == null)
            {
                return;
            }
            
            var matriculations = await service
                .GetMatriculationsOfStudent(idStudentFormatted.Value);

            if (matriculations.Count == 0)
            {
                Console.WriteLine("O aluno não tem matricula em nenhum curso.");
            }

            foreach (var matriculation in matriculations)
            {
                Console.WriteLine($"{matriculation.Id} | {matriculation.Course.Name} | {matriculation.Student.Name}");                
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }
}