using mini_app_json_database.Exceptions;

namespace mini_app_json_database.Helpers.Methods
{
    public static class Helper
    {
        public static bool ValidateNameOrSurname(this string value, bool isName)
        {
            bool isValid = value.Length > 3 || char.IsUpper(value[0]);

            if (isValid)
            {
                return true;

            }
            else
            {
                string nameOrSurname = isName ? "name" : "surname";
                throw new System.IO.InvalidDataException($"Enter valid {nameOrSurname}");
            }
        }

        public static bool ValidateClassRoomName(string name)
        {
            if (name.Length != 5)
            {
                throw new CustomInvalidDataException("Classromm name should be  5 charracters");
            }
            int upperLetterCount = 0;
            int digitCount = 0;
            for (int i = 0; i < name.Length; i++)
            {
                if (char.IsUpper(name[i]))
                {
                    upperLetterCount++;
                }
                if (char.IsDigit(name[i]))
                {

                    digitCount++;
                }
            }
            if (upperLetterCount == 2 && digitCount == 3)
            {
                return true;
            }
            else
            {
                throw new CustomInvalidDataException("Classroom name is invalid.Name must contain 2 uppercase letters and 3 digits.");
            }

        }
    }
}
