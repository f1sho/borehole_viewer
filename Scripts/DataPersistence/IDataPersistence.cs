using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistence //: MonoBehaviour
{
    void LoadData(NoteData notes);
    void SaveData(NoteData notes);
}
