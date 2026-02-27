namespace ConceptsDB.Application.Menu;

public interface IMenuAction
{
    string Key { get; }
    string Description { get; }
    Task ExecuteAsync();
}