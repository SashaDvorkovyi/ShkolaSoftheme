using System;

namespace Zodiak
{
    public class MonthsImages
    {
        public readonly DateTime monthsAhdDays;
        public readonly string imageName;

        public MonthsImages(DateTime date, string image)
        {
            monthsAhdDays = date;
            imageName = image;
        }
    }
}
