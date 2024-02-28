namespace PA.Desktop.Converters
{
    public class AggregatedDataHelper
    {
        public static float CalculateMean(List<float> values)
        {
            if (values == null || values.Count == 0)
            {
                return 0;
            }

            return values.Average();
        }
        public static float CalculateMedian(List<float> values)
        {
            // Check if the list is empty
            if (values == null || values.Count == 0)
            {
                return 0;
            }

            int numberCount = values.Count;
            int halfIndex = numberCount / 2;
            var sortedNumbers = values.OrderBy(n => n).ToList();
            float median;
            if ((numberCount % 2) == 0)
            {
                median = (sortedNumbers[halfIndex] + sortedNumbers[halfIndex - 1]) / 2;
            }
            else
            {
                median = sortedNumbers[halfIndex];
            }
            return median;
        }

        public static float CalculatePeak(List<float> values)
        {
            // Check if the list is empty
            if (values == null || values.Count == 0)
            {
                return 0;
            }

            // If the list is not empty, safely find the maximum value
            return values.Max();
        }
    }
}
