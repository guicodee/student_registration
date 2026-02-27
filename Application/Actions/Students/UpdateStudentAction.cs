using ConceptsDB.Application.Menu;
using ConceptsDB.Dto.Student;
using ConceptsDB.Services;
using ConceptsDB.Utils;

namespace ConceptsDB.Application.Actions.Students;

public class UpdateStudentAction(StudentService studentService) : IMenuAction
{

    public string Key => "4";
    public string Description => "Atualizar aluno";

    public async Task ExecuteAsync()
    {
        Console.Write("ID do aluno: ");
        var idStudent = Console.ReadLine()!;

        Console.Write("Nome do aluno: ");
        var nameStudent = Console.ReadLine()!;

        Console.Write("E-mail do aluno: ");
        var emailStudent = Console.ReadLine()!;

        try
        {
            var idStudentFormatted = VerifyToNumberReceived.ParseInt(idStudent);

            if (idStudentFormatted == null)
            {
                return;
            }

            var student = new UpdateStudentDto()
            {
                Id = idStudentFormatted.Value,
                Name = nameStudent,
                Email = emailStudent
            };

            await studentService.UpdateStudent(student);

            Console.WriteLine("Aluno atualizado com sucesso.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }
}