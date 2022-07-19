using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1
{
    public class FormatService : IFormatService
    {
        public FormatViewModel Convert(Root data)
        {
            var result = new FormatViewModel();
            var dic = new Dictionary<DaysOfTheWeek, OpeningHours[]>
            {
                { DaysOfTheWeek.Monday, data.Monday},
                { DaysOfTheWeek.Tuesday, data.Tuesday},
                { DaysOfTheWeek.Wednesday, data.Wednesday},
                { DaysOfTheWeek.Thursday, data.Thursday},
                { DaysOfTheWeek.Friday, data.Friday},
                { DaysOfTheWeek.Saturday, data.Saturday},
                { DaysOfTheWeek.Sunday, data.Sunday},
            };

            var days = new List<string>();
            foreach (var (day, hours) in dic)
            {
                string workingHours = "";
                if (hours is null || hours.Length < 1)
                {
                    workingHours = "closed";
                }
                else
                {
                    workingHours = ComputeWorkingHours(day, hours.OrderBy(x => x.Value).ToList(), dic);
                }
                if (workingHours is null)
                {
                    result.ErrorMessage = "Invalid data";
                    return result;
                }
                var readableFormat = $"{day}: {workingHours}";
                days.Add(readableFormat);
            }
            result.IsSuccessful = true;
            result.Data = string.Join('\n', days);
            return result;
        }

        private string ComputeWorkingHours(DaysOfTheWeek day, List<OpeningHours> hours, Dictionary<DaysOfTheWeek, OpeningHours[]> schedule)
        {
            // If first element is a closing time, it must be from previous day
            if (hours.First().Type.Equals(OpeningType.close))
                hours.RemoveAt(0);
            // If last element is a opening time, then find closing time from next day
            if (hours.Last().Type.Equals(OpeningType.open))
            {
                var nextDay = day + 1;
                var tomorrowHours = schedule[nextDay];
                if (tomorrowHours is null || tomorrowHours.Length < 1)
                    return null;
                hours.Add(tomorrowHours.OrderBy(x => x.Value).First());
            }
            var ho = hours.Select((c, i) =>
            {
                var result = "";
                if (i % 2 == 0)
                {
                    result = $"{DateTimeOffset.FromUnixTimeSeconds(hours[i].Value):hh:mm tt} - {DateTimeOffset.FromUnixTimeSeconds(hours[i + 1].Value):hh:mm tt}";
                }
                return result;
            });
            return string.Join(", ", ho.Where(x => !string.IsNullOrWhiteSpace(x)));
        }

    }
}
