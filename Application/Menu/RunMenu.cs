namespace ConceptsDB.Application.Menu;

public class RunMenu
{
    private readonly Dictionary<string, IMenuAction> _actions;

    public RunMenu(IEnumerable<IMenuAction> actions)
    {
        _actions = actions.ToDictionary(a => a.Key);
    }
    
    public void ShowMenu()
    {
        foreach (var action in _actions.Values)
        {
            Console.WriteLine($"[ {action.Key} ] - {action.Description}");

        }
        
        Console.WriteLine("[ 0 ] - Sair");
        Console.WriteLine("==================================");
    }

    public string ReadOptions()
    {
        Console.WriteLine("Escolha uma opção: ");
        return Console.ReadLine()!;
    }

    public async Task<bool> ExecuteAsync(string option)
    {
        if (option == "0")
        {
            Console.WriteLine("Saindo...");
            return false;
        }

        if (_actions.TryGetValue(option, out var action))
        {
            await action.ExecuteAsync();
        }
        else
        {
            Console.WriteLine("Opção inválida.");
        }

        return true;
    }
}