using System;
using System.Globalization;
using Xamarin.Forms;

namespace x2136443.Converters
{
    public class TimeElapsedEventToSecondsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var eventArgs = value as Octane.Xamarin.Forms.VideoPlayer.Events.VideoPlayerEventArgs;
            var val = eventArgs?.CurrentTime.TotalSeconds ?? -1;
            return (int)val;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
