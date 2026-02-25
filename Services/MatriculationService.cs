using ConceptsDB.Dto.Matriculation;
using ConceptsDB.Models;
using ConceptsDB.Repository.Matriculations;

namespace ConceptsDB.Services;

public class MatriculationService
{
    private readonly IMatriculationRepository _matriculationRepository;

    public MatriculationService(IMatriculationRepository matriculationRepository)
    {
        _matriculationRepository = matriculationRepository;
    }

    public async Task EnrollStudent(RegisterMatriculationDto matriculation)
    {
        var studentIsAlreadyEnrolled = await IsAlreadyEnrolled(
            matriculation.StudentId, 
            matriculation.CourseId
        );

        if (studentIsAlreadyEnrolled)
        {
            throw new Exception("Estudante já está matriculado nesse curso.");
        }
        
        var newMatriculation = new Matriculation()
        {
            StudentId = matriculation.StudentId,
            CourseId = matriculation.CourseId
        };

        await _matriculationRepository.EnrollStudent(newMatriculation);
    }

    public async Task UnenrollStudent(int idStudent, int idCourse)
    {
        var studentIsAlreadyEnrolled = await IsAlreadyEnrolled(idStudent, idCourse);

        if (!studentIsAlreadyEnrolled)
        {
            throw new Exception("Estudante não está matriculado nesse curso.");
        }

        await _matriculationRepository.UnenrollStudent(idStudent, idCourse);
    }

    public async Task<bool> IsAlreadyEnrolled(int idStudent, int idCourse)
    {
        var enrolled = await _matriculationRepository.IsAlreadyEnrolled(idStudent, idCourse);

        return enrolled;
    }

    public async Task<List<Matriculation>> GetMatriculationsOfStudent(int idStudent)
    {
        var matriculations = await _matriculationRepository.GetMatriculationsOfStudent(idStudent);

        return matriculations;
    }
}