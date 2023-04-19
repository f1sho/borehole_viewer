using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NoteData
{
    public List<AnnotationToStore> annotationToStores;

    public NoteData()
    {
        annotationToStores = new List<AnnotationToStore>();
    }
}
