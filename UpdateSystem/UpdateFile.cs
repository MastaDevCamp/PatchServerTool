using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UpdateSystem
{
    public class UpdateFile
    {
        public char Type { get; set; }
        public string Path { get; set; }
        public string CompressType { get; set; }
        public int OriginalSize { get; set; }
        public int CompressedSize { get; set; }
        public string OriginalHash { get; set; }
        public string CompressedHash { get; set; }
        public string Version { get; set; }
        public char DiffType { get; set; }

        public UpdateFile StringToClass(string str)
        {
            string[] words = str.Replace(" ", "").Split('|');
            Type = words[0].ToCharArray()[0];
            Path = words[1];
            CompressType = words[2];
            switch (Type)
            {
                case 'D':
                    {
                        Version = words[3];
                        DiffType = words[4].ToCharArray()[0];
                        break;
                    }
                case 'F':
                    {
                        OriginalSize = Int32.Parse(words[3]);
                        CompressedSize = Int32.Parse(words[4]);
                        OriginalHash = words[5];
                        CompressedHash = words[6];
                        Version = words[7];
                        DiffType = words[8].ToCharArray()[0];
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Cannot add in list");
                        break;
                    }
            }

            return this;
        }

        public string GetDownPath()
        {
            return this.Version + this.Path + "." + this.CompressType;
        }

        public string GetPathFileName()
        {
            return this.Path + "." + this.CompressType;
        }
    }
}
