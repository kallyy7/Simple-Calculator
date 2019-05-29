namespace University_System.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using UniSys.Database.Data;
    using UniSys.Database.Data.Models.Users;
    using UniSys.Database.Data.Models.Users.Interfaces;

    public static class PersonalNumberValidation
    {
        public static bool GetValid(IUser user)
        {
            List<IUser> u = new List<IUser>(SystemDatabase.Context.GetUsersList(user));

            // проверка за повтарящо се егн
            List<string> personalNumbers = new List<string>();

            if (user is Lecturer)
                personalNumbers.AddRange(u.Select(l => l.PersonalNumber));
            else if (user is Student)
                personalNumbers.AddRange(u.Select(s => s.PersonalNumber));

            if (personalNumbers.Contains(user.PersonalNumber)) return false;

            return IsValidPersonalNumber(user.PersonalNumber);
        }

        public static bool IsValidPersonalNumber(string personalNumber)
        {
            // валидация на егн
            List<int> egnWeights = new List<int>() { 2, 4, 8, 5, 10, 9, 7, 3, 6 };

            if (personalNumber.Length < 10 && 
                !(Regex.IsMatch(personalNumber, @"\d")))
                return false;

            int day;
            int.TryParse(personalNumber[4].ToString() + personalNumber[5], out day);
            int month;
            int.TryParse(personalNumber[2].ToString() + personalNumber[3], out month);
            int year;
            int.TryParse(personalNumber[0].ToString() + personalNumber[1], out year);

            if (month > 40)
            {
                if (!CheckDate(month - 40, day, year + 200))
                    return false;
            }
            else if (month > 20)
            {
                if (!CheckDate(month - 20, day, year + 1800))
                    return false;
            }
            else
            {
                if (!CheckDate(month, day, year + 1900))
                    return false;
            }
            int checkSum = int.Parse(personalNumber.Substring(9, 1).ToString());
            int egnSum = 0;

            for (int i = 0; i < 9; i++)
                egnSum += int.Parse(personalNumber.Substring(i, 1).ToString()) * egnWeights[i];

            int validCheckSum = egnSum % 11;

            if (validCheckSum == 10)
                validCheckSum = 0;

            if (checkSum == validCheckSum)
                return true;

            return false;
        }

        /// <summary>
        /// Генериране на пол
        /// </summary>
        /// <param name="personalNumber"></param>
        /// <returns></returns>
        public static string GetGender(string personalNumber)
        {
            string gender = int.Parse(personalNumber[personalNumber.Length - 2].ToString()) % 2 == 0
                ? "Мъж"
                : "Жена";

            return gender;
        }

        /// <summary>
        /// Генериране на рожденна дата
        /// </summary>
        /// <param name="personalNumber"></param>
        /// <returns></returns>
        public static string GetBirthDate(string personalNumber)
        {
            string day = personalNumber[4].ToString() + personalNumber[5];
            string month = personalNumber[2].ToString() + personalNumber[3];
            string year = "1" + "9" + personalNumber[0].ToString() + personalNumber[1];

            string birthDate = day + "/" + month + "/" + year;

            return birthDate;
        }

        /// <summary>
        /// Валидация на датата
        /// </summary>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        private static bool CheckDate(int month, int day, int year)
        {
            if (month < 1 || month > 12) return false;
            int days = DateTime.DaysInMonth(year, month);
            if (day <= days) return true;
            return true;
        }
    }
}
