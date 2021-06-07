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

        public string SetDirectoryAndFilePath(ulong guildId)
        {
            string solutionDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            if (solutionDirectory.Contains("bin"))
            {
                solutionDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName;
            }
        
            return @$"{solutionDirectory}\DiscordBot\ModBot.DAL\Textfiles\{guildId}.txt";
        }

        public List<T> LoadFromFile<T>(ulong guildId)
        {
            CheckFileStatus(guildId);

            using (StreamReader file = File.OpenText(SetDirectoryAndFilePath(guildId)))
            {
                var fetchedValue = JsonConvert.DeserializeObject<List<T>>(file.ReadToEnd());
                if (fetchedValue == null)
                {
                    return new List<T>();
                }
                return fetchedValue;
            }
                 
        }

        private void CheckFileStatus(ulong guildId)
        {       
            string solutionDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            if (solutionDirectory.Contains("bin"))
            {
                solutionDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName;
            }

            var fileName = $"{solutionDirectory}\\DiscordBot\\ModBot.DAL\\Textfiles\\{guildId}.txt";
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
        public void SaveToFile<T>(T objectToBeSaved, ulong guildId)
        {
            CheckFileStatus(guildId);

            using (StreamWriter file = File.CreateText(SetDirectoryAndFilePath(guildId)))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, objectToBeSaved);
            }
        }
    }
}
