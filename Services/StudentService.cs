using ConceptsDB.Models;
using ConceptsDB.Repository;

namespace ConceptsDB.Services;

public class StudentService
{
    private readonly IStudentRepository _studentRepository;

    public StudentService(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task CreateStudent(Student student)
    {
        var studentAlreadyExists = await _studentRepository.GetStudentByEmail(student.Email);

        if (studentAlreadyExists != null)
        {
            throw new Exception("Um aluno já tem esse e-mail cadastrado.");
        }

        await _studentRepository.CreateStudent(student);
    }

    public async Task<List<Student>> ListStudents()
    {
        var students = await _studentRepository.ListStudents();

        if (students.Count == 0)
        {
            throw new Exception("Não temos alunos cadastradas.");
        }

        return students;
    }

    public async Task<Student?> GetStudentByEmail(string email)
    {
        var student = await _studentRepository.GetStudentByEmail(email);

        if (student == null)
        {
            throw new Exception("Nenhum aluno encontrado com esse e-mail.");
        }

        return student;
    }

    public async Task<Student?> GetStudentById(int id)
    {
        var student = await _studentRepository.GetStudentById(id);

        if (student == null)
        {
            throw new Exception("Nenhum aluno encontrado com esse e-mail");
        }

        return student;
    }

    public async Task UpdateStudent(Student student)
    {
        var studentAlreadyExists = await GetStudentById(student.Id);

        if (studentAlreadyExists == null)
        {
            throw new Exception("Nenhum aluno encontrado no sistema.");
        }

        await _studentRepository.UpdateStudent(student);
    }

    public async Task DeleteStudent(int id)
    {
        var student = await _studentRepository.GetStudentById(id);

        if (student == null)
        {
            throw new Exception("Nenhum aluno encontrado no sistema.");
        }
        
        await _studentRepository.DeleteStudent(id);
    }
}