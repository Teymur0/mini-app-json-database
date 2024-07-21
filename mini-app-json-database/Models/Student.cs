using mini_app_json_database.Database.Services;
using mini_app_json_database.Helpers.Methods;
namespace mini_app_json_database.Models
{
    public class Student
    {

        private static int _id = 0;
        public int Id { get; private set; }

        private string _name;
        public string Name
        {
            get => _name; set
            {
                value.ValidateNameOrSurname(isName: true);
                _name = value;
            }
        }
        private string _surname;
        public string Surname
        {
            get => _surname; set
            {
                value.ValidateNameOrSurname(isName: false);
                _surname = value;
            }
        }

        public Student(string name, string surname)
        {
            Id = ++_id;
            Name = name;
            Surname = surname;
        }
        public void PrintALlStudents()
        {
            var classrooms = CustomDataBaseService.LoadFiles();
            foreach (var classroom in classrooms)
            {

                foreach (var student in classroom.Students)
                {

                    Console.WriteLine($"Classroom ID: {classroom.Id} , Student name: {student.Name}");
                }
            }


        }
        public void PrintStudentsByClassRoomId(int classroomId)
        {
            var classrooms = CustomDataBaseService.LoadFiles();
            Classroom classroom = null;

            foreach (var classRoom in classrooms)
            {
                if (classRoom.Id == classroomId)
                {
                    classroom = classRoom;
                    break;
                }
                if (classroom == null)
                {
                    Console.WriteLine($"Classroom with ID:{classroomId} not found.");
                    return;
                }
                if (classroom.Students.Count == 0)
                {
                    Console.WriteLine("No students found in this classroom.");
                    return;
                }
                foreach (var student in classroom.Students)
                {
                    Console.WriteLine($"Classroom ID: {classroomId},Student ID: {student.Id},");
                }
            }


        }

    }
}
