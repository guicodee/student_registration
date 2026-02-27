using ConceptsDB.Application.Actions.Courses;
using ConceptsDB.Application.Actions.Matriculation;
using ConceptsDB.Application.Actions.Students;
using ConceptsDB.Application.Menu;
using ConceptsDB.Data;
using ConceptsDB.Repository;
using ConceptsDB.Repository.Courses;
using ConceptsDB.Repository.Matriculations;
using ConceptsDB.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ConceptsDB.Configuration;

public static class ServiceColletion
{
    public static IServiceCollection Services(this IServiceCollection services)
    {
        services.AddScoped<AppDbContext>();
        
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<StudentService>();
        
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<CourseService>();
        
        services.AddScoped<IMatriculationRepository, MatriculationRepository>();
        services.AddScoped<MatriculationService>();

        services.AddSingleton<Application.Application>();
        services.AddSingleton<RunMenu>();
        
        services.AddSingleton<IMenuAction, CreateStudentAction>();
        services.AddSingleton<IMenuAction, ListAllStudentsAction>();
        services.AddSingleton<IMenuAction, GetStudentByEmailAction>();
        services.AddSingleton<IMenuAction, UpdateStudentAction>();
        services.AddSingleton<IMenuAction, DeleteStudentAction>();
        
        services.AddSingleton<IMenuAction, EnrollStudentAction>();
        services.AddSingleton<IMenuAction, UnerollStudentAction>();
        services.AddSingleton<IMenuAction, GetMatriculationOfStudentAction>();
        
        services.AddSingleton<IMenuAction, RegisterCourseAction>();
        services.AddSingleton<IMenuAction, ListAllCoursesActions>();
        services.AddSingleton<IMenuAction, GetCourseByNameAction>();
        services.AddSingleton<IMenuAction, UpdateCourseAction>();
        services.AddSingleton<IMenuAction, DeleteCourseAction>();
        return services;
    }
}