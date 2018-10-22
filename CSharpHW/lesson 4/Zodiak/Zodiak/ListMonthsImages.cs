using System;
using System.Collections.Generic;

namespace Zodiak
{
    public class ListMonthsImages
    {
        public readonly List<MonthsImages> listMonthsImages;

        public ListMonthsImages()
        {
                        listMonthsImages = new List<MonthsImages>();
            listMonthsImages.Add(new MonthsImages(new DateTime(0001, 01, 19), "01_kozerog.jpg"));
            listMonthsImages.Add(new MonthsImages(new DateTime(0001, 02, 18), "02_vodoley.jpg"));
            listMonthsImages.Add(new MonthsImages(new DateTime(0001, 03, 20), "03_fish.jpg"));
            listMonthsImages.Add(new MonthsImages(new DateTime(0001, 04, 20), "04_oven.jpg"));
            listMonthsImages.Add(new MonthsImages(new DateTime(0001, 05, 20), "05_telec.jpg"));
            listMonthsImages.Add(new MonthsImages(new DateTime(0001, 06, 20), "06_bleznec.jpg"));
            listMonthsImages.Add(new MonthsImages(new DateTime(0001, 07, 22), "07_rak.jpg"));
            listMonthsImages.Add(new MonthsImages(new DateTime(0001, 08, 22), "08_lev.jpg"));
            listMonthsImages.Add(new MonthsImages(new DateTime(0001, 09, 23), "09_deva.jpg"));
            listMonthsImages.Add(new MonthsImages(new DateTime(0001, 10, 23), "10_vesu.jpg"));
            listMonthsImages.Add(new MonthsImages(new DateTime(0001, 11, 21), "11_skorpion.jpg"));
            listMonthsImages.Add(new MonthsImages(new DateTime(0001, 12, 21), "12_strelec.jpg"));
            listMonthsImages.Add(new MonthsImages(new DateTime(0001, 12, 31), "01_kozerog.jpg"));
        }
    }
}
