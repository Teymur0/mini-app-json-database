using mini_app_json_database.Database.Services;
using mini_app_json_database.Exceptions;
using mini_app_json_database.Helpers.Enums;
using mini_app_json_database.Helpers.Methods;
using mini_app_json_database.Models;

bool isTerminateApp = false;

while (!isTerminateApp)
{
    Console.WriteLine("App Menu:");
    Console.WriteLine("1. Create a new classroom.");
    Console.WriteLine("2. Create a new student.");
    Console.WriteLine("3. Print all students.");
    Console.WriteLine("4. Print students in a specific classroom.");
    Console.WriteLine("5. Delete a student.");
    Console.WriteLine("6. Exit.");

    Console.Write("Enter your choice: ");
    int choice = int.Parse(Console.ReadLine());

    switch (choice)
    {
        case 1:
            CreateNewClassroom();
            break;

        case 2:
            CreateNewStudent();
            break;
        case 3:
            Student.PrintALlStudents();
            break;
        case 4:
            try
            {
                Console.Write("Enter classroom ID: ");

                int classroomId = int.Parse(Console.ReadLine());
                var classrooms = CustomDataBaseService.LoadFiles();
                Student.PrintStudentsByClassRoomId(classroomId);
            }
            catch (CustomInvalidDataException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            break;
        case 5:

            DeleteStudentById();


            break;
        case 6:
            isTerminateApp = true;
            break;

        default:
            Console.WriteLine("Please enter valid choise.");
            break;

    }
}
static void CreateNewClassroom()
{
    try
    {
        Console.Write("Enter classroom name: ");
        string className = Console.ReadLine();
        Helper.ValidateClassRoomName(className);
        Console.Write("Enter class type (Backend/Frontend): ");
        Enum.TryParse(Console.ReadLine(), out ClassType classType);

        Classroom newClassroom = new Classroom(className, classType);
        CustomDataBaseService.SaveFiles(newClassroom);

        Console.WriteLine("Classroom created successfully!");
    }
    catch (CustomInvalidDataException ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}
static void CreateNewStudent()
{
    try
    {
        Console.Write("Enter student name: ");
        string studentName = Console.ReadLine();
        studentName.ValidateNameOrSurname(isName: true);

        Console.Write("Enter student surname: ");
        string studentSurname = Console.ReadLine();
        studentSurname.ValidateNameOrSurname(isName: false);

        Student newStudent = new Student(studentName, studentSurname);

        Console.Write("Enter classroom ID.: ");
        int classId = int.Parse(Console.ReadLine());

        var classrooms = CustomDataBaseService.LoadFiles();
        Classroom selectedClassroom = null;

        foreach (var classroom in classrooms)
        {
            if (classroom.Id == classId)
            {
                selectedClassroom = classroom;
                break;
            }
        }
        if (selectedClassroom != null)
        {
            selectedClassroom.AddStudent(newStudent);
            CustomDataBaseService.SaveFiles(selectedClassroom);
            Console.WriteLine("Student added to classroom.");
        }
        else
        {
            Console.WriteLine($"Classroom with ID: {classId} not found.");
        }
    }
    catch (CustomInvalidDataException ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }



}

static void DeleteStudentById()
{
    try
    {
        Console.Write("Enter student ID to delete: ");
        int studentId = int.Parse(Console.ReadLine());

        Console.Write("Enter classroom ID: ");
        int classroomId = int.Parse(Console.ReadLine());

        var classrooms = CustomDataBaseService.LoadFiles();
        Classroom classroomToDelete = null;

        foreach (var classroom in classrooms)
        {
            if (classroom.Id == classroomId)
            {
                classroomToDelete = classroom;
                break;
            }
        }
        if (classroomToDelete != null)
        {
            classroomToDelete.DeleteStudentById(studentId);
            CustomDataBaseService.SaveFiles(classroomToDelete);
            Console.WriteLine("Student deleted.");
        }
        else
        {
            Console.WriteLine($"Classroom with ID:{classroomId} not found.");
        }
    }
    catch (StudentNotFoundException ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }

}

