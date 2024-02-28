using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace PA.Desktop.CustomControls
{
    public partial class CircularProgressBar : UserControl
    {
        public CircularProgressBar()
        {
            InitializeComponent();
        }
        public Brush IndicatorBrush
        {
            get { return (Brush)GetValue(IndicatorBrushProperty); }
            set { SetValue(IndicatorBrushProperty, value); }
        }
        public static readonly DependencyProperty IndicatorBrushProperty =
            DependencyProperty.Register("IndicatorBrush", typeof(Brush), typeof(CircularProgressBar));
        public Brush BackgroundBrush
        {
            get { return (Brush)GetValue(BackgroundBrushProperty); }
            set { SetValue(BackgroundBrushProperty, value); }
        }
        public static readonly DependencyProperty BackgroundBrushProperty =
            DependencyProperty.Register("BackgroundBrush", typeof(Brush), typeof(CircularProgressBar));

        public int ArcThickness
        {
            get { return (int)GetValue(ArcThicknessProperty); }
            set { SetValue(ArcThicknessProperty, value); } 
        }
        public static readonly DependencyProperty ArcThicknessProperty =
            DependencyProperty.Register("ArcThickness", typeof(int), typeof(CircularProgressBar));
        public int PercentFontSize
        {
            get { return (int)GetValue(PercentFontSizeProperty); }
            set { SetValue(PercentFontSizeProperty, value); }
        }
        public static readonly DependencyProperty PercentFontSizeProperty =
            DependencyProperty.Register("PercentFontSize", typeof(int), typeof(CircularProgressBar));
        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(int), typeof(CircularProgressBar));
    }
}
