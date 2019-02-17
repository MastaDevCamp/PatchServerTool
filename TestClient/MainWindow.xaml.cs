using Newtonsoft.Json;
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
            ServerConnect serverConnect = new ServerConnect();
            serverConnect.Url = ServerConnect.UPDATE_SERVER_URL + "/updateResource/lastVersion";
            string lastestVersion = serverConnect.GetResponse();
            serverConnect.Reset();
            UpdateDownloadButton(lastestVersion);
            VersionText.Text = lastestVersion;

            logTextBox.Text += "Successfully getting lastest version in update server\n";

            serverConnect.Url = ServerConnect.UPDATE_SERVER_URL + "/updateResource/Merge";
            serverConnect.Content = nowVersionText.Text;
            string response = serverConnect.PostResponse();
            dynamic stuff = JsonConvert.DeserializeObject<UpdateInfoRes>(response);
            List<string> fileList = stuff.responseData;

            logTextBox.Text += "Successfully getting update file list in file server\n";

            AddUpdatelistInListView(fileList);
        }

        public void AddUpdatelistInListView(List<string> fileList)
        {
            List<UpdateFile> updateFiles = new List<UpdateFile>();
            foreach(string fileLine in fileList)
            {
                UpdateFile updateFile = new UpdateFile();
                updateFiles.Add(updateFile.StringToClass(fileLine));
            }
            updateListView.ItemsSource = updateFiles;
            logTextBox.Text += "Add filelist in UpdateList\n";
        }

        public void UpdateDownloadButton(string version)
        {
            downloadButton.IsEnabled = true;
        }

        private void DownloadButton_Click(object sender, RoutedEventArgs e)
        {
            logTextBox.Text += "Start " + downloadButton.Content + "\n";
        }
    }


}
