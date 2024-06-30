using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BackOffice.UI.Common.View.Navigation
{
    /// <summary>
    /// Interaction logic for Navigation.xaml
    /// </summary>
    public partial class Navigation : UserControl
    {
        public Navigation()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty LogoContentProperty = DependencyProperty.Register("LogoContent", 
            typeof(object), typeof(Navigation), new PropertyMetadata(null));
        public object LogoContent
        {
            get { return (object)GetValue(LogoContentProperty); }
            set
            {
                SetValue(LogoContentProperty, value);
            }
        }
    }
}
