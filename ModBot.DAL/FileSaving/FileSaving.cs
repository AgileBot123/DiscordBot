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

        public string SetDirectoryAndFilePath()
        {
            string solutionDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            return @$"{solutionDirectory}\DiscordBot\ModBot.DAL\Textfiles\BannedWords.txt";
        }

        public List<T> LoadFromFile<T>()
        {
            CheckFileStatus();

            using (StreamReader file = File.OpenText(SetDirectoryAndFilePath()))
            {
                var fetchedValue = JsonConvert.DeserializeObject<List<T>>(file.ReadToEnd());
                if (fetchedValue == null)
                {
                    return new List<T>();
                }
                return fetchedValue;
            }
                 
        }

        private void CheckFileStatus()
        {
            string solutionDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            var fileName = $"{solutionDirectory}\\DiscordBot\\ModBot.DAL\\Textfiles\\BannedWords.txt";
            var directory = $"{solutionDirectory}\\DiscordBot\\ModBot.DAL\\Textfiles";
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
