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

        var studentService = host.Services.GetRequiredService<StudentService>();
        var courseService = host.Services.GetRequiredService<CourseService>();
        var matriculationService = host.Services.GetRequiredService<MatriculationService>();

        await RunMenu(studentService, courseService, matriculationService);
    }

    public static async Task RunMenu(StudentService studentService, CourseService courseService, MatriculationService matriculationService)
    {
        bool runningMenu = true;
        
        while (runningMenu)
        {
            var menu = """
                       ============== MENU ============== 
                       [ 1 ] - Criar um aluno
                       [ 2 ] - Listar alunos
                       [ 3 ] - Buscar aluno por email
                       [ 4 ] - Registrar curso
                       [ 5 ] - Listar cursos
                       [ 6 ] - Buscar curso por nome
                       [ 7 ] - Atualizar curso
                       [ 8 ] - Deletar curso
                       [ 9 ] - Atualizar aluno 
                       [ 10 ] - Deletar aluno
                       [ 11 ] - Matricular aluno
                       [ 12 ] - Desmatricular aluno
                       [ 13 ] - Buscar matriculas de um aluno
                       [ 14 ] - Sair
                       ==================================
                       """;
            
            Console.WriteLine(menu);

            var numberDigited = Console.ReadLine();
            
            switch (numberDigited)
            {
                case "1":
                    await CreateStudent(studentService);
                    break;
                case "2":
                    await ListStudents(studentService);
                    break;
                case "3":
                    await GetStudentByEmail(studentService);
                    break;
                case "4":
                    await RegisterCourse(courseService);
                    break;
                case "5":
                    await ListCourses(courseService);
                    break;
                case "6":
                    await GetCourseByName(courseService);
                    break;
                case "7":
                    await UpdateCourse(courseService);
                    break;
                case "8":
                    await DeleteCourse(courseService);
                    break;
                case "9":
                    await UpdateStudent(studentService);
                    break;
                case "10":
                    await DeleteStudent(studentService);
                    break;
                case "11":
                    await EnrollStudent(matriculationService);
                    break;
                case "12":
                    await UnenrollStudent(matriculationService);
                    break;
                case "13":
                    await GetMatriculationsOfStudent(matriculationService);
                    break;
                case "14":
                    runningMenu = false;
                    Console.WriteLine("Saindo...");
                    break;
            }
        }
    }
    
    public static async Task CreateStudent(StudentService service)
    {
        Console.Write("Nome do aluno: ");
        var nameStudent = Console.ReadLine()!;
        
        Console.Write("E-mail do aluno: ");
        var emailStudent = Console.ReadLine()!;

        try
        {
            var student = new CreateStudentDto()
            {
                Name = nameStudent,
                Email = emailStudent
            };
            
            await service.CreateStudent(student);
            Console.WriteLine("Aluno cadastrado com sucesso.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }

    public static async Task ListStudents(StudentService service)
    {
        try
        {
            var students = await service.ListStudents();

            if (students.Count == 0)
            {
                Console.WriteLine("Nenhum aluno cadastrado no sistema.");
            }

            foreach (var student in students)
            {
                Console.WriteLine($"{student.Id} | {student.Name} | {student.Email}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }

    public static async Task GetStudentByEmail(StudentService service)
    {
        Console.Write("E-mail do aluno: ");
        var email = Console.ReadLine()!;
        
        try
        {
            var student = await service.GetStudentByEmail(email);

            if (student == null)
            {
                Console.WriteLine("Aluno não encontrado.");
                return;
            }
            
            Console.WriteLine($"{student.Id} | {student.Name} | {student.Email}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }

    public static async Task RegisterCourse(CourseService service)
    {
        Console.Write("Nome do curso: ");
        var nameCourse = Console.ReadLine()!;
        
        try
        {
            var registerCourseDto = new RegisterCourseDto()
            {
                Name = nameCourse,
            };
            
            await service.RegisterCourse(registerCourseDto);
            Console.WriteLine("Curso cadastrado com sucesso.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }

    public static async Task ListCourses(CourseService service)
    {
        try
        {
            var courses = await service.ListCourses();

            if (courses.Count == 0)
            {
                Console.WriteLine("Nenhum curso cadastrado no sistema.");
                return;
            }

            foreach (var course in courses)
            {
                Console.WriteLine($"{course.Id} | {course.Name} | {course.Matriculations.Count()} alunos matriculados");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");    
        }
    }

    public static async Task GetCourseByName(CourseService service)
    {
        Console.Write("Nome do curso: ");
        var nameCourse = Console.ReadLine()!;
        
        try
        {
            var course = await service.GetCourseByName(nameCourse);

            if (course == null)
            {
                Console.WriteLine("Nenhum curso encontrado.");
                return;
            }
            
            Console.WriteLine($"{course.Id} | {course.Name} | {course.Matriculations.Count()} alunos matriculados ");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }

    public static async Task UpdateCourse(CourseService service)
    {
        Console.Write("Id do curso: "); 
        var idCourse = Console.ReadLine()!;
        
        Console.Write("Nome do curso: ");
        var nameCourse = Console.ReadLine()!;

        try
        {
            var idCourseFormatted = VerifyToNumberReceived.ParseInt(idCourse);

            if (idCourseFormatted == null)
            {
                return;
            }

            
            var updateCouseDto = new UpdateCourseDto()
            {
                IdCourse = idCourseFormatted.Value,
                NameCourse = nameCourse
            };
            
            await service.UpdateCourse(updateCouseDto);
            
            Console.WriteLine("Curso atualizado com sucesso.");
        }   
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }

    public static async Task DeleteCourse(CourseService service)
    {
        Console.Write("Id do curso: ");
        var idCourse = Console.ReadLine()!;
        
        try
        {
            var idFormatted = VerifyToNumberReceived.ParseInt(idCourse);

            if (idFormatted == null)
            {
                return;    
            }
            
            await service.DeleteCourse(idFormatted.Value);
            
            Console.WriteLine("Curso deletado com sucesso.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }

    private static async Task UpdateStudent(StudentService studentService)
    {
        Console.Write("ID do aluno: ");
        var idStudent = Console.ReadLine()!;
        
        Console.Write("Nome do aluno: ");
        var nameStudent = Console.ReadLine()!;
        
        Console.Write("E-mail do aluno: ");
        var emailStudent =  Console.ReadLine()!;

        try
        {
            var idStudentFormatted = VerifyToNumberReceived.ParseInt(idStudent);

            if (idStudentFormatted == null)
            {
                return;
            }

            var student = new UpdateStudentDto()
            {
                Id = idStudentFormatted.Value,
                Name = nameStudent,
                Email = emailStudent
            };

            await studentService.UpdateStudent(student);
            
            Console.WriteLine("Aluno atualizado com sucesso.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }

    private static async Task DeleteStudent(StudentService studentService)
    {
        Console.Write("ID do aluno: ");
        var idStudent = Console.ReadLine()!;
        
        try
        {
            var idStudentFormatted = VerifyToNumberReceived.ParseInt(idStudent);

            if (idStudentFormatted == null)
            {
                return;
            }

            await studentService.DeleteStudent(idStudentFormatted.Value);
            
            Console.WriteLine("Aluno e suas matrículas deletadas com sucesso.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }
    
    private static async Task EnrollStudent(MatriculationService matriculationService)
    {
        Console.Write("Id do aluno: ");
        var idStudent = Console.ReadLine()!;
        
        Console.Write("Id do curso: ");
        var idCourse = Console.ReadLine()!;

        try
        {
            var idStudentFormatted = VerifyToNumberReceived.ParseInt(idStudent);
            var idCourseFormatted = VerifyToNumberReceived.ParseInt(idCourse);

            if (idStudentFormatted == null ||  idCourseFormatted == null)
            {
                return;
            }

            
            var newMatriculation = new RegisterMatriculationDto()
            {
                StudentId = idStudentFormatted.Value,
                CourseId = idCourseFormatted.Value
            };

            await matriculationService.EnrollStudent(newMatriculation);
            
            Console.WriteLine("Aluno matriculado com sucesso.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }

    private static async Task UnenrollStudent(MatriculationService matriculationService)
    {
        Console.Write("Id do aluno: ");
        var idStudent = Console.ReadLine()!;
        
        Console.Write("Id do curso: ");
        var idCourse = Console.ReadLine()!;

        try
        {
            var idStudentFormatted = VerifyToNumberReceived.ParseInt(idStudent);
            var idCourseFormatted = VerifyToNumberReceived.ParseInt(idCourse);

            if (idStudentFormatted == null ||  idCourseFormatted == null)
            {
                return;
            }

            await matriculationService
                .UnenrollStudent(
                    idStudentFormatted.Value,
                    idCourseFormatted.Value
                );
            
            Console.WriteLine("Aluno desmatriculado do curso com sucesso.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }

    private static async Task GetMatriculationsOfStudent(MatriculationService matriculationService)
    {
        Console.Write("Id do aluno: ");
        var idStudent = Console.ReadLine()!;

        try
        {
            var idStudentFormatted = VerifyToNumberReceived.ParseInt(idStudent);

            if (idStudentFormatted == null)
            {
                return;
            }
            
            var matriculations = await matriculationService
                .GetMatriculationsOfStudent(idStudentFormatted.Value);

            if (matriculations.Count == 0)
            {
                Console.WriteLine("O aluno não tem matricula em nenhum curso.");
            }

            foreach (var matriculation in matriculations)
            {
                Console.WriteLine($"{matriculation.Id} | {matriculation.Course.Name} | {matriculation.Student.Name}");                
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }
}
