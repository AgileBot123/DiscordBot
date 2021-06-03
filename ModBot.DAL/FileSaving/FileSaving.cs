using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ModBot.DAL.FileSaving
{
    public static class FileSaving
    {
        public static string path = "Words";
        public static string fileName = "BannedWords.txt";



        /// <summary>
        /// Loads the file and returns a List of T which could be any Type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> LoadFromFile<T>()
        {
            CheckFileStatus();

            var asm = Assembly.GetExecutingAssembly();

            using (Stream stream = asm.GetManifestResourceStream($"ModBot.DAL.Resources.{path + "\\" + fileName}"))            
            using (StreamReader file = new StreamReader(stream))
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
        private static void CheckFileStatus()
        {
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
        public static void SaveToFile<T>(T objectToBeSaved)
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
