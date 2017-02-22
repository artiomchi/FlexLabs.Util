using System;

namespace FlexLabs
{
    public static class DateTimeExtensions
    {
        private static DateTime _linuxBase = new DateTime(1970, 1, 1);

        /// <summary>
        /// Converts the DateTime value to a linux timestamp
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static long ToLinuxTimestamp(this DateTime time)
        {
            if (time < _linuxBase)
                throw new ArgumentOutOfRangeException(nameof(time), "Time value has to be older than the year 1970");
            return Convert.ToInt64(time.Subtract(_linuxBase).TotalSeconds);
        }

        /// <summary>
        /// Converts the linux timestamp to a DateTime value
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static DateTime FromLinuxTimestamp(this long timestamp)
            => _linuxBase.Add(TimeSpan.FromSeconds(timestamp));
    }
}
