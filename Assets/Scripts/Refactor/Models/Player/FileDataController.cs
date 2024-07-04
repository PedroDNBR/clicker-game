using UnityEngine;
using System;
using System.IO;
using Newtonsoft.Json;

namespace RC
{
    public class FileDataController
    {
        private string dataDirPath = "";

        private string dataFileName = "";

        public FileDataController(string dataDirPath, string dataFileName)
        {
            this.dataDirPath = dataDirPath;
            this.dataFileName = dataFileName;
        }

        public GameData Load()
        {
            //string fullPath = Path.Combine(dataDirPath, dataFileName);

            GameData loadedData = null;
            string dataToLoad = "";

            dataToLoad = PlayerPrefs.GetString("savefile");

            // loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            loadedData = JsonConvert.DeserializeObject<GameData>(dataToLoad, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });

            //if (File.Exists(fullPath))
            //{
            //    try
            //    {
            //        //string dataToLoad = "";

            //        //using (FileStream stream = new FileStream(fullPath, FileMode.Open))
            //        //{
            //        //    using (StreamReader reader = new StreamReader(stream))
            //        //    {
            //        //        dataToLoad = reader.ReadToEnd();
            //        //    }
            //        //}

            //        //dataToLoad = PlayerPrefs.GetString("savefile");

            //        //// loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            //        //loadedData = JsonConvert.DeserializeObject<GameData>(dataToLoad, new JsonSerializerSettings
            //        //{
            //        //    TypeNameHandling = TypeNameHandling.Auto
            //        //});

            //    }
            //    catch (Exception e)
            //    {
            //        Debug.LogError("Error Loading the file" + fullPath + "\n" + e);
            //    }
            //}

            return loadedData;
        }

        public void Save(GameData data)
        {
            //string fullPath = Path.Combine(dataDirPath, dataFileName);
            string dataToStore = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });

            PlayerPrefs.SetString("savefile", dataToStore);
            PlayerPrefs.Save();
            //try
            //{
            //    //Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            //    //string dataToStore = JsonUtility.ToJson(data, true);

            //    //string dataToStore = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings
            //    //{
            //    //    TypeNameHandling = TypeNameHandling.Auto
            //    //});

            //    //PlayerPrefs.SetString("savefile", dataToStore);
            //    //PlayerPrefs.Save();
            //    //using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            //    //{
            //    //    using (StreamWriter writer = new StreamWriter(stream))
            //    //    {
            //    //        writer.Write(dataToStore);
            //    //    }
            //    //}

            //}
            //catch (Exception e)
            //{
            //    Debug.LogError("Error Loading the file" + fullPath + "\n" + e);
            //}
        }
    }
}
