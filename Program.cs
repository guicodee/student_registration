using ConceptsDB.Configuration;
using ConceptsDB.Dto.Course;
using ConceptsDB.Dto.Matriculation;
using ConceptsDB.Dto.Student;
using ConceptsDB.Services;
using ConceptsDB.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConceptsDB;

public class Program
{
    public static async Task Main()
    {
        var host = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.Services();
            })
            .Build();

        var app = host.Services.GetRequiredService<Application.Application>();
        
        await app.Run();
    }
}
