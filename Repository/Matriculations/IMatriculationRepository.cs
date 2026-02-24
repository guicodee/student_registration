using ConceptsDB.Models;

namespace ConceptsDB.Repository.Matriculations;

public interface IMatriculationRepository
{
    Task EnrollStudent(Matriculation matriculation);
    Task UnenrollStudent(int idStudent, int idCourse);
    Task<bool> IsAlreadyEnrolled(int idStudent, int idCourse);
    Task<List<Matriculation>> GetMatriculationsOfStudent(int idStudent);
}