using ConceptsDB.Application.Menu;
using ConceptsDB.Dto.Matriculation;
using ConceptsDB.Services;
using ConceptsDB.Utils;

namespace ConceptsDB.Application.Actions.Matriculation;

public class EnrollStudentAction(MatriculationService service) : IMenuAction
{
    public string Key => "6";
    public string Description => "Matricular aluno";

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

            if (idStudentFormatted == null || idCourseFormatted == null)
            {
                return;
            }


            var newMatriculation = new RegisterMatriculationDto()
            {
                StudentId = idStudentFormatted.Value,
                CourseId = idCourseFormatted.Value
            };

            await service.EnrollStudent(newMatriculation);

            Console.WriteLine("Aluno matriculado com sucesso.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }
}