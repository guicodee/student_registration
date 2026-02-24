using ConceptsDB.Models;

namespace ConceptsDB.Repository.Courses;

public interface ICourseRepository
{
    Task RegisterCourse(Course course);
    Task<List<Course>> ListCourses();
    Task<Course?> GetCourseById(int id);
    Task<Course?> GetCourseByName(string courseName);
    Task UpdateCourse(Course course);
    Task DeleteCourse(int idCourse);
}   