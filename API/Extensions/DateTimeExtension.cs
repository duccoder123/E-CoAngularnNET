namespace API.Extensions
{
    public static class DateTimeExtension
    {
        public static int CalculateAge(this DateOnly dob){
            var today = DateOnly.FromDateTime(DateTime.Now);
            var age = today.Year - dob.Year;
            if(today > dob.AddYears(-1)) age--;
            return age; 
        }
    }
}