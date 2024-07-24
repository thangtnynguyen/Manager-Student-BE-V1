namespace UTEHY.DatabaseCoursePortal.Api.Helpers
{
    public static class DocumentHelper
    {
        public static int CalculateReadingTime(string content, int wordsPerMinute)
        {
            int wordCount = CountWords(content);
            int readingTime = (int)Math.Ceiling((double)wordCount / wordsPerMinute);

            return readingTime;
        }

        public static int CountWords(string text)
        {
            return text.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Length;
        }
    }
}
