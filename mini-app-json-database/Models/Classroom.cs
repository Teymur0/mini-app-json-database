using mini_app_json_database.Database.Services;
using mini_app_json_database.Exceptions;
using mini_app_json_database.Helpers.Enums;
using mini_app_json_database.Helpers.Methods;
namespace mini_app_json_database.Models
{
    public class Classroom
    {
        private static int _id = 0;
        public int Id { get; private set; }
        private string _name;
        public string Name
        {
            get => _name; set
            {
                Helper.ValidateClassRoomName(value);
                _name = value;
            }
        }
        public ClassType Type { get; set; }
        public List<Student> Students;

        public Classroom(string name, ClassType type)
        {

            Id = ++_id;
            Name = name;
            Type = type;
            Students = new List<Student>();
        }

        public void AddStudent(Student student)
        {
            int studentLimit = Type == ClassType.Backend ? 20 : 15;

            if (Students.Count >= studentLimit)
            {
                throw new CustomInvalidDataException("Class limit reached.");
            }
            Students.Add(student);
        }

        public Student FindStudentById(int id)
        {
            foreach (Student student in Students)
            {

                if (student.Id == id)
                {
                    return student;
                }
            }
            throw new StudentNotFoundException($"Student with ID:{id} not found.");
        }

        public void DeleteStudentById(int id)
        {
            foreach (Student student in Students)
            {
                if (student.Id == id)
                {
                    Students.Remove(student);
                }
            }


        }
    }
}

