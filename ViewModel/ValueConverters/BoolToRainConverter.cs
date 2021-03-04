using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Weather_App_MVVM.ViewModel.ValueConverters
{
    class BoolToRainConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isRainy =(bool)value;
            
            
            if (isRainy)
                return "Currently raining";


            return "Currently not raining";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string isRaining = value as string;

            if (isRaining == "Currently raining")
                return true;


            return false;
        }
    }
}
