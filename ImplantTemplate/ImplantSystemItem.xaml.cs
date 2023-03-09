using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace ImplantTemplate
{
    /// <summary>
    /// ImplantSystemItem.xaml 的互動邏輯
    /// </summary>
    public partial class ImplantSystemItem : UserControl
    {
        public delegate void SelectedHandler(ImplantSystemItem item);
        public event SelectedHandler SelectedFromISItem;

        public static readonly DependencyProperty IsFavoriteProperty = DependencyProperty.Register(
         "IsFavorite", typeof(bool), typeof(ImplantSystemItem), new FrameworkPropertyMetadata(null));
        public bool IsFavorite
        {
            get => (bool)GetValue(IsFavoriteProperty);
            set => SetValue(IsFavoriteProperty, value);
        }

        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(
         "IsSelected", typeof(bool), typeof(ImplantSystemItem), new FrameworkPropertyMetadata(null));
        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
         "Text", typeof(string), typeof(ImplantSystemItem), new FrameworkPropertyMetadata(null));
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public ImplantSystemItem()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (IsSelected == false) { SelectedFromISItem?.Invoke(this); }
            IsSelected = true;
        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Image srcImage = sender as Image;
            IsFavorite = srcImage.Name == "imageNoFavorite" ? true : false;
            e.Handled = true;
        }
    }

    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class IsFavoriteConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        { return (bool)value ? Visibility.Visible : Visibility.Collapsed; }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        { throw new NotSupportedException(); }
    }

    [ValueConversion(typeof(bool), typeof(SolidColorBrush))]
    public class IsSelectedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        { return (bool)value ? (SolidColorBrush)new BrushConverter().ConvertFrom("#ff0000") : (SolidColorBrush)new BrushConverter().ConvertFrom("#00ffffff"); }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        { throw new NotSupportedException(); }
    }
}
