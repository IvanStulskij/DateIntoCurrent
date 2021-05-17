using System;

namespace DateIntoCurrent
{
    public static class Program
    {
        static void Main()
        {
            try
            {
                Console.WriteLine("Введите год");
                int year = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Введите месяц");
                int month = Convert.ToInt32(Console.ReadLine());
                if (month > 12 || month < 1)
                {
                    throw new Exception("Введите месяц корректно");
                }
                Console.WriteLine("Введите день");
                int day = Convert.ToInt32(Console.ReadLine());
                if (day > 31 || day < 1)
                {
                    throw new Exception("Введите день корректно");
                }
                Date date = new Date(year, month, day);
                Console.WriteLine("от этой даты прошло " + date.NumberDaysToYearEnd() + " дней");
                date.ReturnWeekDayString();
            }
            catch (FormatException)
            {
                Console.WriteLine("Дату нужно вводить в числовом формате");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }

    public class Date
    {
        private int _year;
        private int _month;
        private int _date;
        public Date(int year, int month, int date)
        {
            _year = year;
            _month = month;
            _date = date;
        }
        
        public void ReturnWeekDayString()
        {
            string weekdayInString = "";
            switch (Math.Abs(NumberDaysToYearEnd() - WeeksInDaysToDate() - (int) DateTime.Now.DayOfWeek) % 7)
            {
                case 1:
                    weekdayInString = "Понедельник";
                    break;
                case 2:
                    weekdayInString = "Вторник";
                    break;
                case 3:
                    weekdayInString = "Среда";
                    break;
                case 4:
                    weekdayInString = "Четверг";
                    break;
                case 5:
                    weekdayInString = "Пятница";
                    break;
                case 6:
                    weekdayInString = "Суббота";
                    break;
                case 0:
                    weekdayInString = "Воскресенье";
                    break;
            }
            Console.WriteLine(weekdayInString);
        }

        private int WeeksInDaysToDate()
        {
            int daysInYearFromDate = NumberDaysToYearEnd();
            int nearestInteger = 0;
            for (int i = 1; i <= daysInYearFromDate; i++)
            {
                if (i % 7 == 0)
                {
                    nearestInteger++;
                }
            }

            return nearestInteger * 7;
        }

        private int DaysToYearBalace()
        {
            int sumDays = 0;
            for (int i = _year; i < DateTime.Today.Year; i++)
            {
                if (i % 4 == 0)
                {
                    sumDays += 366;
                }
                else
                {
                    sumDays += 365;
                }
            }

            return sumDays;
        }

        public int NumberDaysToYearEnd()
        {
            int daysToYear = DaysToYearBalace();
            int days = 0;
            for (int i = 1; i < _month; i++)
            {
                days += NumberDaysInMonthToCurrent(i, _year);
            }


            return daysToYear - days - _date + DaysThatYear();
        }

        private int DaysThatYear()
        {
            int daysThatMonth = 0;
            for (int i = 1; i < DateTime.Today.Month; i++)
            {
                daysThatMonth += NumberDaysInMonthToCurrent(i, _year);
            }

            return daysThatMonth + DateTime.Today.Day;
        }
        
        private int NumberDaysInMonthToCurrent(int month, int year)
        {
            int numberDaysInMonth;
            if (month == 4 || month == 6 || month == 9 || month == 11)
            {
                numberDaysInMonth = 30;
            }
            else if (month == 2)
            {
                if (year % 4 == 0)
                {
                    numberDaysInMonth = 29;
                }
                else
                {
                    numberDaysInMonth = 28;
                }
            }
            else
            {
                numberDaysInMonth = 31;
            }

            return numberDaysInMonth;
        }
    }
}
