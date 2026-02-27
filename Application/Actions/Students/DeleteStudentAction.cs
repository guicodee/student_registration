using ConceptsDB.Application.Menu;
using ConceptsDB.Services;
using ConceptsDB.Utils;

namespace ConceptsDB.Application.Actions.Students;

public class DeleteStudentAction(StudentService service) : IMenuAction
{
    public string Key => "5";
    public string Description => "Deletar aluno";
    public async Task ExecuteAsync()
    {
        Console.Write("ID do aluno: ");
        var idStudent = Console.ReadLine()!;

        try
        {
            var idStudentFormatted = VerifyToNumberReceived.ParseInt(idStudent);

            if (idStudentFormatted == null)
            {
                return;
            }

            await service.DeleteStudent(idStudentFormatted.Value);

            Console.WriteLine("Aluno e suas matrículas deletadas com sucesso.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }
}