using ConceptsDB.Application.Menu;
using ConceptsDB.Dto.Student;
using ConceptsDB.Services;

namespace ConceptsDB.Application.Actions.Students;

public class CreateStudentAction(StudentService service) : IMenuAction
{
    public string Key => "1";
    public string Description => "Criar um aluno";

    public async Task ExecuteAsync()
    {
        Console.Write("Nome do aluno: ");
        var nameStudent = Console.ReadLine()!;
        
        Console.Write("E-mail do aluno: ");
        var emailStudent = Console.ReadLine()!;

        try
        {
            var student = new CreateStudentDto()
            {
                Name = nameStudent,
                Email = emailStudent
            };
            
            await service.CreateStudent(student);
            Console.WriteLine("Aluno cadastrado com sucesso.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }
}