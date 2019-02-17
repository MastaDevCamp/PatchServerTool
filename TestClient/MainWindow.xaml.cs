using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UpdateSystem;

namespace TestClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            AddUpdatelistInListView(updateListView);
            UpdateDownloadButton("1.0.1");
        }

        public void AddUpdatelistInListView(ListView listView)
        {
            List<UpdateFile> updateFiles = new List<UpdateFile>();
            updateFiles.Add(new UpdateFile()
            {
                Type = "D",
                Path = "/contents/images/sg1"
            });
            updateFiles.Add(new UpdateFile()
            {
                Type = "D",
                Path = "/contents/images/sg2"
            });
            updateFiles.Add(new UpdateFile()
            {
                Type = "F",
                Path = "/contents/images/sg1/img1"
            });
            updateFiles.Add(new UpdateFile()
            {
                Type = "F",
                Path = "/contents/images/sg1/img2"
            });
            updateFiles.Add(new UpdateFile()
            {
                Type = "F",
                Path = "/contents/images/sg2/img1"
            });
            updateFiles.Add(new UpdateFile()
            {
                Type = "F",
                Path = "/contents/images/sg2/img2"
            });
            listView.ItemsSource = updateFiles;
            logTextBox.Text += "Add filelist in UpdateList\n";
        }

        public void UpdateDownloadButton(string version)
        {
            downloadButton.IsEnabled = true;
            downloadButton.Content = "Download "+version;
        }

        private void DownloadButton_Click(object sender, RoutedEventArgs e)
        {
            logTextBox.Text += "Start " + downloadButton.Content + "\n";
        }
    }


}
