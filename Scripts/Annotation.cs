using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Annotation
{
    public GameObject arrow;
    public GameObject post;

    public Annotation(GameObject arrow,GameObject post)
    {
        this.arrow = arrow;
        this.post = post;
    }
}

[Serializable]
public class AnnotationToStore
{
    public string arrowName;
    public Vector3 arrowPosition;
    public Quaternion arrowRotation;

    public string postName;
    public Vector3 postPosition;
    public Quaternion postRotation;

    public double depth;

    public string noteToStore;

    public AnnotationToStore(string ArrowName, Vector3 arrowPosition, Quaternion arrowRotation,
        string PostName, Vector3 postPosition, Quaternion postRotation, double depth, string NoteToStore)
    {
        arrowName = ArrowName;
        postName = PostName;
        noteToStore = NoteToStore;
        this.depth = depth;
        this.arrowPosition = arrowPosition;
        this.arrowRotation = arrowRotation;
        this.postPosition = postPosition;
        this.postRotation = postRotation;
    }
}