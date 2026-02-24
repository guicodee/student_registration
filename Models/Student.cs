namespace ConceptsDB.Models;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;

    public List<Matriculation> Matriculations { get; set; } = new();
}