using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ModBot.DAL.FileSaving
{
    public class FileSaving
    {
        public string fileName = "FileSaving.txt";



        /// <summary>
        /// Loads the file and returns a List of T which could be any Type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> LoadFromFile<T>()
        {
            CheckFileStatus();

            var assembly = this.GetType().Assembly;
            var resourceStream = assembly.GetManifestResourceStream($"{this.GetType().Namespace}.{fileName}");
            using (StreamReader file = new StreamReader(resourceStream))
            {
                var fetchedValue = JsonConvert.DeserializeObject<List<T>>(file.ReadToEnd());
                if (fetchedValue == null)
                {
                    return new List<T>();
                }
                return fetchedValue;
            }
                 
        }


        /// <summary>
        /// Checks if the Directory exist, if not creates one.
        /// Checks if the file exists. If not, creates one.
        /// </summary>
        private void CheckFileStatus()
        {
            var path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;


            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (!File.Exists(fileName))
            {
                using (FileStream fileStream = File.Create(fileName))
                { }
            }
                 
        }


        /// <summary>
        /// This method saves to a file and overrides its content.
        /// There for you ALWAYS have to fetch the information inside the file first and then append the information to that list
        /// before saving again.. 
        /// 
        /// Check BannedWordService -> CreateBannedWord for example
        /// 
        /// <typeparam name="T"></typeparam>
        /// <param name="objectToBeSaved"></param>
        public void SaveToFile<T>(T objectToBeSaved)
        {
            CheckFileStatus();

            using (StreamWriter file = File.CreateText(fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, objectToBeSaved);
            }
        }
    }
}
