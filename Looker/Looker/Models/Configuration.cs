using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows.Forms;

namespace Looker.Models
{
    public class Configuration
    {
        public string ImagesPath { get; set; }

        public int VisibleInterval { get; set; }

        public int Top { get; set; }

        public int Left { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public void Save()
        {
            File.WriteAllText(Application.StartupPath + "\\config.json", JsonConvert.SerializeObject(this));
        }

        public static Configuration Load()
        {
            try
            {
                return JsonConvert.DeserializeObject<Configuration>(File.ReadAllText(Application.StartupPath + "\\config.json"));
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}