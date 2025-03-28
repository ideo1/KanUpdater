namespace KanUpdater.Services.Extensions
{
    public static class StringExtensions
    {
        public static int GetAgeRestriction(this IEnumerable<string> ageRestriction)
        {
            if (ageRestriction == null || !ageRestriction.Any())
            {
                return 0;
            }

            var ages = ageRestriction.Select(x => int.TryParse(new string(x.Where(ch => char.IsDigit(ch)).ToArray()), out var age) ? age : 0);

            return ages.Min();
        }
    }
}
