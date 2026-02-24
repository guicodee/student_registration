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

        return services;
    }
}