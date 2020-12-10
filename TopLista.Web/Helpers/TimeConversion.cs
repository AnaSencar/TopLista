using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopLista.Web.ViewModels;

namespace TopLista.Web.Helpers
{
    public static class TimeConversion
    {
        public static int GetTimeInSeconds(int hours, int minutes, int seconds)
        {
            int hoursIntoSeconds = (hours * 60) * 60;
            int minutesIntoSeconds = minutes * 60;
            return hoursIntoSeconds + minutesIntoSeconds + seconds;
        }

        public static string GetStringRepresentationOfTime(this int time)
        {
            int secondsIntoMinutes = time / 60;
            int secondsLeft = time % 60;
            int minutesIntoHours = secondsIntoMinutes / 60;
            int minutesLeft = secondsIntoMinutes % 60;
            return $"{minutesIntoHours.ToString("D2")} : {minutesLeft.ToString("D2")} : {secondsLeft.ToString("D2")}";
        }

    }
}
