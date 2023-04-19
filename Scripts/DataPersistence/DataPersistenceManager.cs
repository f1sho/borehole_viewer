using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    private NoteData notes;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;

    public static DataPersistenceManager instance { get; private set; }

    public void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Data Persistence Menager in the scene.");
        }
        instance = this;
    }

    private void Start()
    {
        //this.dataHandler = new FileDataHandler(Application.dataPath, fileName);
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        //this.dataHandler = new FileDataHandler("/mnt/sdcard", fileName);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadNotes();
    }

    public void emptyNotes()
    {
        this.notes = new NoteData();
    }

    public void LoadNotes()
    {
        this.notes = dataHandler.Load();
        
        if (this.notes == null)
        {
            Debug.Log("No notes data was found. Initializing notes data to empty notes.");
            emptyNotes();
        }

        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects) 
        {
            dataPersistenceObj.LoadData(notes);
        }
    }

    public void SaveNotes()
    {
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(notes);
        }
        dataHandler.Save(notes);
    }

    private void OnApplicationQuit()
    {
        SaveNotes();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
