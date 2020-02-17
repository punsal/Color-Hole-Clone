using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Utilities.File_System {
    public static class SaveSystem
    {
        public static void Save<T1, T2>(this T1 type1, string fileName, string fileExtension)
        {
            var path = $"{Application.persistentDataPath}/{fileName}.{fileExtension}";
            var formatter = new BinaryFormatter();
            var stream = new FileStream(path, FileMode.OpenOrCreate);

            var data = (T2) Activator.CreateInstance(typeof(T2), type1);
            
            formatter.Serialize(stream, data);
            stream.Close();
        }

        public static T Load<T>(string fileName, string fileExtension) where T : class
        {
            var path = $"{Application.persistentDataPath}/{fileName}.{fileExtension}";
            if (!File.Exists(path)) return null;
            var formatter = new BinaryFormatter();
            var stream = new FileStream(path, FileMode.Open);

            var data = formatter.Deserialize(stream) as T;
            stream.Close();

            return data;
        }
    }
}