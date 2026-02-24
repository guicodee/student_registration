namespace ConceptsDB.Models;

public class Course
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public List<Matriculation> Matriculations { get; set; } = new();
}