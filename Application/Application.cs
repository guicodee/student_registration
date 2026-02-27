using ConceptsDB.Application.Menu;

namespace ConceptsDB.Application;

public class Application(RunMenu menu)
{
    public async Task Run()
    {
        bool running = true;

        while (running)
        {
            menu.ShowMenu();
            var option = menu.ReadOptions();

            running = await menu.ExecuteAsync(option);
        }
    }    
}