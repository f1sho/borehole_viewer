using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;

[Serializable]
public class FileDataHandler //: MonoBehaviour
{
    private string saveDataDirPath = "";
    private string saveDataFileName = "";

    public FileDataHandler(string saveDataDirPath, string saveDataFileName)
    {
        this.saveDataDirPath = saveDataDirPath;
        this.saveDataFileName = saveDataFileName;
    }

    public NoteData Load()
    {
        string fullPath = Path.Combine(saveDataDirPath, saveDataFileName);
        //string fullPath = saveDataDirPath + "/" + saveDataFileName;
        NoteData loadedNoteData = null;
        if(File.Exists(fullPath))
        {
            try
            {
                string noteToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream)) 
                    {
                        noteToLoad = reader.ReadToEnd();
                    }
                }
                //Debug.Log(Path.GetFullPath(saveDataFileName));
                loadedNoteData = new NoteData();
                loadedNoteData.annotationToStores = JsonHelper.FromJson<AnnotationToStore>(noteToLoad).ToList();
                //Debug.Log(noteToLoad);
            }
            catch(Exception e)
            {
                Debug.LogError("Error occured when trying to load notes data from file: " + fullPath + "\n" + e);
            }
        }
        return loadedNoteData;
    }

    public void Save(NoteData notes)
    {
        string fullPath = Path.Combine(saveDataDirPath, saveDataFileName);
        //string fullPath = saveDataDirPath + "/" + saveDataFileName;
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            string noteToStore = JsonHelper.ToJson(notes.annotationToStores.ToArray(), true);

            using (FileStream stream = new FileStream(fullPath, FileMode.Create)) 
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(noteToStore);
                }
            }
            Debug.Log(Path.GetFullPath(saveDataFileName));
            //Debug.Log(noteToStore);
        }
        catch(Exception e)
        {
            Debug.LogError("Error occured when trying to save notes data to file: " + fullPath + "\n" + e);
        }
    }

    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }

        public static string ToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }
}
