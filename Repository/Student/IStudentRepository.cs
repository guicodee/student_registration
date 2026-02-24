using ConceptsDB.Models;

namespace ConceptsDB.Repository;

public interface IStudentRepository
{
    Task CreateStudent(Student student);
    Task<List<Student>> ListStudents();
    Task<Student?> GetStudentByEmail(string email); 
    Task<Student?> GetStudentById(int id); 
    Task UpdateStudent(Student student);
    Task DeleteStudent(int id);
}