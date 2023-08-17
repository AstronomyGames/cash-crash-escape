using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.VisualScripting;

namespace HCStandards
{
    public static class DataManager
    {
        public static Data data;

        public static void Save()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/Data.Astronomy";
            FileStream stream = new FileStream(path, FileMode.Create);

            formatter.Serialize(stream, data);
            stream.Close();

        }

        public static void Load()
        {
            if(data!=null) return;
            string path = Application.persistentDataPath + "/Data.Astronomy";
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);
                data = formatter.Deserialize(stream) as Data;
                stream.Close();
            }
            else
            {
                Debug.LogWarning("Could not reload the data");
                data = new Data();
            }
        }

        public static Data GetData()
        {
            if (data == null)
                Load();

            return data;
        }   
    }
}

