namespace DirectFerriesTechTest.Models
{
    public class UserDetailsService
    {
        public UserDetails GetUniqueUserData(string fullName, DateTime dateOfBirth)
        {
            return new UserDetails
            {
                NumberOfVowels = GetAllVowelsFromString(fullName),
                AgeInfo = CalculateAge(dateOfBirth),
                DaysUntilNextBirthday = CalculateDaysUntilNextBirthday(dateOfBirth),
                DaysBeforeBirthday = GetDaysBeforeNextBirthday(dateOfBirth)
            };
        }

        private int GetAllVowelsFromString(string data)
        {
            // Define the set of vowels
            char[] vowelsArray = { 'a', 'e', 'i', 'o', 'u', 'A', 'E', 'I', 'O', 'U' };

            // Use LINQ to filter and retrieve only the vowels from the input string
            return data.Count(x => vowelsArray.Contains(x));
        }

        private Age CalculateAge(DateTime dateOfBirth)
        {
            DateTime now = DateTime.UtcNow;

            int years = now.Year - dateOfBirth.Year;

            // Check if the current date is before the birth date in the current year
            if (now.DayOfYear < dateOfBirth.DayOfYear)
            {
                years--;
            }

            DateTime lastBirthday = dateOfBirth.AddYears(years);
            TimeSpan timeSinceLastBirthday = now - lastBirthday;

            return new Age
            {
                Years = years,
                Days = timeSinceLastBirthday.Days,
                Hours = timeSinceLastBirthday.Hours
            };
        }

        private int CalculateDaysUntilNextBirthday(DateTime dateOfBirth)
        {
            DateTime nextBirthday = CalculateNextBirthday(dateOfBirth);
            DateTime now = DateTime.UtcNow;

            return (nextBirthday - now).Days;
        }

        private DateTime CalculateNextBirthday(DateTime dateOfBirth)
        {
            DateTime now = DateTime.UtcNow;
            DateTime nextBirthday = new DateTime(now.Year, dateOfBirth.Month, dateOfBirth.Day);

            // If the birthday has already passed this year, use next year's birthday
            if (nextBirthday < now)
            {
                nextBirthday = new DateTime(now.Year + 1, dateOfBirth.Month, dateOfBirth.Day);
            }

            return nextBirthday;
        }

        private List<string> GetDaysBeforeNextBirthday(DateTime dateOfBirth)
        {
            DateTime nextBirthday = CalculateNextBirthday(dateOfBirth);
            List<string> daysBeforeBirthday = new List<string>();

            // Add the names of the 14 days before the next birthday to the list
            for (int i = 1; i <= 14; i++)
            {
                daysBeforeBirthday.Add(nextBirthday.AddDays(-i).DayOfWeek.ToString());
            }

            return daysBeforeBirthday;
        }

    }
}
