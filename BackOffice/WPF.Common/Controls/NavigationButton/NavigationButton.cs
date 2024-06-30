using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPF.Common.Controls.NavigationButton
{
    public class NavigationButton:Button
    {
        static NavigationButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NavigationButton), new FrameworkPropertyMetadata(typeof(NavigationButton)));
        }

        public static readonly DependencyProperty IconPathProperty = DependencyProperty.Register("IconPath",
            typeof(object), typeof(NavigationButton), new PropertyMetadata(null));

        public object IconPath
        {
            get { return (object)GetValue(IconPathProperty); }
            set
            {
                SetValue(IconPathProperty, value);
            }
        }
    }
}
