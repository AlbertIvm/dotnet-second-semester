using System;
using System.Windows;
using System.Windows.Data;

namespace MKLBenchmarkApp
{
    [ValueConversion(typeof(bool), typeof(string))]
    internal class ChangesNotSavedStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return $"Changes to benchmark are not saved: {value}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }

    [ValueConversion(typeof(double), typeof(string))]
    internal class LeastLaToHaTimingRatioStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return $"Least LA to HA timings' ratio in benchmark: {value}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }

    [ValueConversion(typeof(double), typeof(string))]
    internal class LeastEpToHaTimingRatioStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return $"Least EP to HA timings' ratio in benchmark: {value}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }

    [ValueConversion(typeof(MKLWrapper.VMTime), typeof(string))]
    internal class VMTimeExtraInfoStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                MKLWrapper.VMTime vmtime = (MKLWrapper.VMTime)value;
                return $"EP to HA timings' ratio: {vmtime.EpToHaTimingRatio:0.####};\nLA to HA timings' ratio: {vmtime.LaToHaTimingRatio:0.####}";
            }
            catch (NullReferenceException)
            {
                return DependencyProperty.UnsetValue;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }

    [ValueConversion(typeof(MKLWrapper.VMTime), typeof(string))]
    internal class VMAccuracyMaxErrorArgStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                MKLWrapper.VMAccuracy vmaccuracy = (MKLWrapper.VMAccuracy)value;
                return $"Point at which the error is maximal: {vmaccuracy.MaxAbsErrorArgument}";
            }
            catch (NullReferenceException)
            {
                return DependencyProperty.UnsetValue;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }

    [ValueConversion(typeof(MKLWrapper.VMTime), typeof(string))]
    internal class VMAccuracyMaxErrorValuesStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                MKLWrapper.VMAccuracy vmaccuracy = (MKLWrapper.VMAccuracy)value;
                return $"Function's values at this point:\nHA: {vmaccuracy.MaxAbsErrorValueHa},\nLA: {vmaccuracy.MaxAbsErrorValueLa},\nEP: {vmaccuracy.MaxAbsErrorValueEp}";
            }
            catch (NullReferenceException)
            {
                return DependencyProperty.UnsetValue;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
