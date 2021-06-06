using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

namespace ModBot.DAL.FileSaving
{
    public class FileSaving
    {

        //C:\Users\Admin\Documents\GitHub\DiscordBot\DiscordBot\ModBot.API\Textfiles\BannedWords.txt
        public string SetDirectoryAndFilePath()
        {
            string solutionDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            return @$"{solutionDirectory}\DiscordBot\ModBot.API\Textfiles\BannedWords.txt";
        }

        public List<T> LoadFromFile<T>()
        {
            CheckFileStatus();

            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream("ModBot.API.BannedWords.txt"))
            using (StreamReader reader = new StreamReader(stream))
            {
                var fetchedValue = JsonConvert.DeserializeObject<List<T>>(reader.ReadToEnd());
                if (fetchedValue == null)
                {
                    return new List<T>();
                }
                return fetchedValue;
            }
                 
        }

        private void CheckFileStatus()
        {
            var fileName = "Textfiles\\BannedWords.txt";
            var directory = "Textfiles";
            if (!Directory.Exists(directory))
            {              
                Directory.CreateDirectory(directory);
            }
            if (!File.Exists(fileName))
            {
                using (FileStream fileStream = File.Create(fileName))
                { }
            }
                 
        }
        public void SaveToFile<T>(T objectToBeSaved)
        {
            CheckFileStatus();

            using (StreamWriter file = File.CreateText(SetDirectoryAndFilePath()))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, objectToBeSaved);
            }
        }
    }
}
