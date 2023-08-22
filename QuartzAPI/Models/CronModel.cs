using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QuartzAPI.Models
{
    public class CronModel
    {
        public SecondTimeset? Second { get; set; }
        public MinuteTimeset? Minute { get; set; }
        public HourTimeset? Hour { get; set; }
        public DayTimeset? Day { get; set; }
        public MonthTimeset? Month { get; set; }

        public bool IsScheduled { get; set; }

        public override string ToString()
        {
            var second = Second != null ? TimeSetToString(Second) : "*";
            var minute = Minute != null ? TimeSetToString(Minute) : "*";
            var hour = Hour != null ? TimeSetToString(Hour) : "*";
            var day = Day != null ? TimeSetToString(Day) : "*";
            var month = Month != null ? TimeSetToString(Month) : "*";

            return $"{second} {minute} {hour} ? {month} {day} *";
        }

        private string TimeSetToString(ITimeSet timeSet)
        {
            var str = new StringBuilder();

            if (timeSet.FromTimeSpan != null)
                str.Append(timeSet.FromTimeSpan);
            if (timeSet.ToTimeSpan != null)
                str.Append($"-{timeSet.ToTimeSpan}");

            if (timeSet.EveryTimeSpan != null)
            {
                if (timeSet.FromTimeSpan != null || timeSet.ToTimeSpan != null)
                    str.Append($"/{timeSet.EveryTimeSpan}");
                else
                    str.Append($"0/{timeSet.EveryTimeSpan}");
            }
            

            return str.ToString();
        }
    }

    public interface ITimeSet
    {
        public int? EveryTimeSpan { get; set; }
        public int? FromTimeSpan { get; set; }
        public int? ToTimeSpan { get; set; }
    }

    public class SecondTimeset : ITimeSet
    {
        [Range(0, 59)]
        public new int? EveryTimeSpan { get; set; }
        [Range(0, 59)]
        public new int? FromTimeSpan { get; set; }
        [Range(0, 59)]
        public new int? ToTimeSpan { get; set; }
    }
    public class MinuteTimeset : ITimeSet
    {
        [Range(0, 59)]
        public new int? EveryTimeSpan { get; set; }
        [Range(0, 59)]
        public new int? FromTimeSpan { get; set; }
        [Range(0, 59)]
        public new int? ToTimeSpan { get; set; }
    }
    public class HourTimeset : ITimeSet
    {
        [Range(0, 23)]
        public new int? EveryTimeSpan { get; set; }
        [Range(0, 23)]
        public new int? FromTimeSpan { get; set; }
        [Range(0, 23)]
        public new int? ToTimeSpan { get; set; }
    }
    public class DayTimeset : ITimeSet
    {
        [Range(1, 7)]
        public new int? EveryTimeSpan { get; set; }
        [Range(1, 7)]
        public new int? FromTimeSpan { get; set; }
        [Range(0, 7)]
        public new int? ToTimeSpan { get; set; }
    }
    public class MonthTimeset : ITimeSet
    {
        [Range(1, 12)]
        public new int? EveryTimeSpan { get; set; }
        [Range(1, 12)]
        public new int? FromTimeSpan { get; set; }
        [Range(0, 12)]
        public new int? ToTimeSpan { get; set; }
    }
}
