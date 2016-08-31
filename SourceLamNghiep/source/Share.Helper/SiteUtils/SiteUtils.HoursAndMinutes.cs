using System;
using System.Collections.Generic;
using System.Linq;
using Share.Helper.Models;

namespace Share.Helper
{
    public static partial class SiteUtils
    {
        public static List<int> GetHours()
        {
            var hours = new List<int>();
            for (int i = 0; i < 24; i++)
            {
                // 0 to 23
                hours.Add(i);
            }
            return hours.OrderBy(i => i).ToList();
        }

        public static List<int> GetMinutes()
        {
            var minutes = new List<int>();
            for (int i = 0; i < 60; i++)
            {
                // 0 to 59
                minutes.Add(i);
            }
            return minutes.OrderBy(i => i).ToList();
        }

        public static List<TextValueModel> GetExpireMonths()
        {
            var list = new List<TextValueModel>();

            for (var i = 1; i <= 9; ++i)
            {
                list.Add(new TextValueModel
                {
                    Text = i.ToString(),
                    Value = i.ToString()
                });
            }

            list.Add(new TextValueModel
            {
                Text = "10",
                Value = "10"
            });

            list.Add(new TextValueModel
            {
                Text = "11",
                Value = "11"
            });

            list.Add(new TextValueModel
            {
                Text = "12",
                Value = "12"
            });

            return list;
        }

        public static string SecondsToMeaningTime(double seconds)
        {
            var ts = TimeSpan.FromSeconds(seconds);
            return (new DateTime(ts.Ticks).ToString("HH:mm:ss"));
        }
    }
}
