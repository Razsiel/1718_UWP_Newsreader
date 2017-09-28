using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace Final_excersise.Converters
{
    public class FavoriteBoolToSymbolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (targetType != typeof(Symbol))
                throw new InvalidOperationException("target type must be a Symbol");

            return (bool) value ? Symbol.SolidStar : Symbol.OutlineStar;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}