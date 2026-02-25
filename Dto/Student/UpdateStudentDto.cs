namespace ConceptsDB.Dto.Student;

public class UpdateStudentDto
{
    public int Id { get; set; } 
    public string Name { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
}