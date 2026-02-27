using ConceptsDB.Application.Menu;
using ConceptsDB.Services;

namespace ConceptsDB.Application.Actions.Students;

public class ListAllStudentsAction(StudentService service) : IMenuAction
{
    public string Key => "2";
    public string Description => "Listar todos os estudantes";
    
    public async Task ExecuteAsync()
    {
        try
        {
            var students = await service.ListStudents();

            if (students.Count == 0)
            {
                Console.WriteLine("Nenhum aluno cadastrado no sistema.");
            }

            foreach (var student in students)
            {
                Console.WriteLine($"{student.Id} | {student.Name} | {student.Email}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }
}