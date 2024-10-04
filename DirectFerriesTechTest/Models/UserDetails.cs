namespace DirectFerriesTechTest.Models
{
    public class UserDetails
    {
        public string FullName { get; set; }
        public double epochDateOfBirth { get; set; }
        public int NumberOfVowels { get; set; }
        public Age AgeInfo { get; set; }
        public int DaysUntilNextBirthday { get; set; }
        public List<string> DaysBeforeBirthday { get; set; }
    }
    public class Age
    {
        public int Years { get; set; }
        public int Days { get; set; }
        public int Hours { get; set; }
    }
}
