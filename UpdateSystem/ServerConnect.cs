using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.IO.Compression;
using System.ComponentModel;

namespace UpdateSystem
{
    public class UpdateInfoRes
    {
        public int statusCode { get; set; }
        public string responseMessage { get; set; }
        public List<string> responseData { get; set; }
    }

    public class ServerConnect
    {
        public const string UPDATE_SERVER_URL = "http://localhost:8082";
        public const string FILE_SERVER_URL = "http://aws.nage.wo.tc:8021";

        public string Url { get; set; }
        public string Content { get; set; }
        public string ResourceServerUrl { get; set; }

        public string PostResponse()
        {
            string response = "";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(Content);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                response = streamReader.ReadToEnd();
            }

            return response;
        }

        public string GetResponse()
        {
            string response = "";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "GET";

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                response = streamReader.ReadToEnd();
            }

            return response;
        }

        public void DownloadPatchFile(object sender, DoWorkEventArgs e)
        {
            List<UpdateFile> patchFileList = (List<UpdateFile>)(((List<Object>)e.Argument)[0]);
            string savePath = (string)(((List<Object>)e.Argument)[1]);

            using (WebClient client = new WebClient())
            {
                foreach (UpdateFile patchFile in patchFileList)
                {
                    switch (patchFile.Type)
                    {
                        case 'D':
                            {
                                string localPath = savePath + patchFile.Path;
                                System.IO.Directory.CreateDirectory(localPath);
                                break;
                            }
                        case 'F':
                            {
                                string url = (ResourceServerUrl + patchFile.GetDownPath()).Replace("\\", "/");
                                string localPath = savePath + patchFile.GetPathFileName();
                                string parentDir = localPath.Remove(localPath.LastIndexOf("\\"));
                                System.IO.Directory.CreateDirectory(parentDir);
                                client.DownloadFile(url, localPath);
                                ZipFile.ExtractToDirectory(localPath, parentDir);
                                File.Delete(localPath);
                                break;
                            }
                    }
                }
            }
        }

        public void Reset()
        {
            this.Url = "";
            this.Content = "";
        }
    }
}
