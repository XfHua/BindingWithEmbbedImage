using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App220
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            ViewModel vm = new ViewModel();
            vm.imageName = "ttt.png";
            this.BindingContext = vm;
        }
    }

    class ViewModel : INotifyPropertyChanged
    {
        string _imageName;

        public event PropertyChangedEventHandler PropertyChanged;

        public ViewModel()
        {
        }

        public string imageName
        {
            set
            {
                if (_imageName != value)
                {
                    _imageName = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("imageName"));
                    }
                }
            }
            get
            {
                return _imageName;
            }
        }
    }

    public class EmbeddedToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string fileName && parameter is String assemblyName)
            {
                try
                {
                    var imageSource = ImageSource.FromResource(assemblyName + "." + fileName, typeof(EmbeddedToImageSourceConverter).GetTypeInfo().Assembly);
                    return imageSource;
                }
                catch (Exception)
                {
                    return value;
                }
            }
            else
                return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
