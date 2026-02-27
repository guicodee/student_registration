using ConceptsDB.Application.Menu;
using ConceptsDB.Services;

namespace ConceptsDB.Application.Actions.Students;

public class GetStudentByEmailAction(StudentService service) : IMenuAction
{
    public string Key => "3";
    public string Description => "Buscar aluno por e-mail";
    public async Task ExecuteAsync()
    {
        Console.Write("E-mail do aluno: ");
        var email = Console.ReadLine()!;
        
        try
        {
            var student = await service.GetStudentByEmail(email);

            if (student == null)
            {
                Console.WriteLine("Aluno não encontrado.");
                return;
            }
        
            Console.WriteLine($"{student.Id} | {student.Name} | {student.Email}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
        }

    }
}