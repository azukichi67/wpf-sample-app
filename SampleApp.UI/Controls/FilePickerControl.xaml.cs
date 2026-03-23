using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace SampleApp.UI.Controls
{
    public partial class FilePickerControl : UserControl
    {
        public static readonly DependencyProperty FilePathProperty =
            DependencyProperty.Register(nameof(FilePath), typeof(string), typeof(FilePickerControl));

        public static readonly DependencyProperty FilterProperty =
            DependencyProperty.Register(nameof(Filter), typeof(string), typeof(FilePickerControl),
                new PropertyMetadata("すべてのファイル|*.*"));

        public static readonly DependencyProperty IsSaveProperty =
            DependencyProperty.Register(nameof(IsSave), typeof(bool), typeof(FilePickerControl),
                new PropertyMetadata(false));

        public string FilePath
        {
            get => (string)GetValue(FilePathProperty);
            set => SetValue(FilePathProperty, value);
        }

        public string Filter
        {
            get => (string)GetValue(FilterProperty);
            set => SetValue(FilterProperty, value);
        }

        public bool IsSave
        {
            get => (bool)GetValue(IsSaveProperty);
            set => SetValue(IsSaveProperty, value);
        }

        public FilePickerControl()
        {
            InitializeComponent();
        }

        private void OnBrowseClicked(object sender, RoutedEventArgs e)
        {
            if (IsSave)
            {
                var dialog = new SaveFileDialog { Filter = Filter };
                if (dialog.ShowDialog() == true)
                {
                    FilePath = dialog.FileName;
                }
            }
            else
            {
                var dialog = new OpenFileDialog { Filter = Filter };
                if (dialog.ShowDialog() == true)
                {
                    FilePath = dialog.FileName;
                }
            }
        }
    }
}
