using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using System;
using UnityEngine.UI;
using System.Runtime.Serialization;

public class QuadTreeLoader : MonoBehaviour,IDataPersistence
{
    public QuadTreeY createTree;
    public QuadTreePt top;
    public QuadTreePt bottom;
    public GameObject layer;
    public GameObject post;
    public GameObject arrow;
    public Canvas raycastCanvas;
    private TMPro.TextMeshProUGUI raycastText;
    private TMPro.TextMeshPro postNumber;
    private GameObject Marks;
    public List<Vector3> markedPositions = new List<Vector3>();
    List<Annotation> posts = new List<Annotation>();
    List<AnnotationToStore> postsToStore = new List<AnnotationToStore>();

    string[] lines;
    public double topY;
    public double bottomY;

    public static QuadTreeLoader instance;

    public void Awake()
    {
        //grab gameobjects in scene
        raycastCanvas = GameObject.FindGameObjectWithTag("raycastCanvas").GetComponent<Canvas>();
        raycastText = GameObject.FindGameObjectWithTag("raycastText").GetComponent<TMPro.TextMeshProUGUI>();
        Marks = GameObject.FindGameObjectWithTag("marks");
    }
    public void LoadData(NoteData notes)
    {
        postsToStore = notes.annotationToStores;
        //Debug.Log(postsToStore);
        if (postsToStore != null)
        {
            for (int i = 0; i < postsToStore.Count; i++)
            {
                this.markedPositions.Add(postsToStore[i].arrowPosition);

                GameObject Arrow = Instantiate(arrow);
                Arrow.name = postsToStore[i].arrowName;
                Arrow.transform.SetParent(Marks.transform, false);
                Arrow.transform.position = postsToStore[i].arrowPosition;
                Arrow.transform.rotation = postsToStore[i].arrowRotation;
                Arrow.SetActive(true);

                GameObject Post = Instantiate(post);
                Post.name = postsToStore[i].postName;
                Post.transform.SetParent(Marks.transform, false);
                Post.transform.position = postsToStore[i].postPosition;
                Post.transform.rotation = postsToStore[i].postRotation;
                Post.GetComponentInChildren<TMPro.TextMeshPro>().text = postsToStore[i].postName;
                Post.SetActive(true);

                posts.Add(new Annotation(Arrow, Post));
            }
        }
    }

    public void SaveData(NoteData notes)
    {
        notes.annotationToStores = postsToStore;
    }

    private void Start()
    {
        instance = this;

        postNumber = post.GetComponentInChildren<TMPro.TextMeshPro>();

        //GameObject ob = GameObject.FindGameObjectWithTag("dataDisplay");
        //var read = ob.GetComponent<readcsv>();
        //lines = read.getLines();
        lines = readcsv.instance.getLines();
        Debug.Log("bbbbbbbbb" + lines[400]);

        //canvas on
        raycastCanvas.enabled = true;

        //make point for boundaries
        topY = layer.GetComponent<Collider>().bounds.max.y;
        bottomY = layer.GetComponent<Collider>().bounds.min.y;
        top = new QuadTreePt(topY);
        bottom = new QuadTreePt(bottomY);
               
        //create a new tree object passing in those point boundaries of the layer
        createTree = new QuadTreeY(top, bottom);

        //call build tree on this new tree object
        createTree.buildTree(lines);
    }

    //method to get the coordinates of the post object spawned in unity and search for the closest point from the depth file to get the other data
    public void buildnewTree(GameObject pNew)
    {
        //get the local vector3 position of the post relative to the layer
        Vector3 postPosition = pNew.transform.localPosition;

        //save the Y coord and create a new point object to search for in the tree
        double postY = postPosition.y;

        QuadTreePt findPoint = new QuadTreePt(postY);

        //call the quadtree search function on the post position
        QuadTreeNode foundNode = createTree.search(findPoint);
        if (foundNode == null)
        {
            Debug.Log("Point was not found");
        }
        else
        {
            //if the closest point is found in the tree, save the coordinate and the data of the found node and print it out
            double closestPointY = foundNode.y.y;
            //Debug.Log("cccccccc"+closestPointY);
            //double closestPointDepth = foundNode.dataDepth;
            //double[] closestPointData = foundNode.data;

            //instantiate a new post at the location of the closest point
            GameObject pinPost = Instantiate(post);
            pinPost.name = "Pin" + markedPositions.Count;
            pinPost.transform.SetParent(Marks.transform, false);
            //set the position of the new post
            Vector3 postTransform = new Vector3(postPosition.x, (float)closestPointY, postPosition.z);
            pinPost.transform.localPosition = postTransform;
            pinPost.transform.rotation = pNew.transform.rotation;
            pinPost.transform.Rotate(0, -90f, 0);
            pinPost.SetActive(true);

            posts.Add(new Annotation(pNew,pinPost));
            postsToStore.Add(new AnnotationToStore(pNew.name, pNew.transform.position, pNew.transform.rotation,
                pinPost.name, pinPost.transform.position, pinPost.transform.rotation, closestPointY, ""));
            //Debug.Log(postsToStore.Count);
            DataPersistenceManager.instance.SaveNotes();
            

            //display to canvas
            raycastText.text = "Pin"+markedPositions.Count+"\nUnity coordinate: " + pinPost.transform.position 
                + "\nDepth: " + foundNode.dataDepth + "\nSpontaneous Potential: " + foundNode.data[0] 
                + "\nNatural Gamma Av: " + foundNode.data[1] + "\nSingle Point Resistance: " + foundNode.data[2] 
                + "\nShort Normal Resistivity: " + foundNode.data[3]+ "\nLong Normal Resistivity: " + foundNode.data[4] 
                + "\nDensity (g/cc): " + foundNode.data[5] + "\nPorosity: " + foundNode.data[6];
        }
    }

    public List<Annotation> Posts()
    {
        return posts;
    }

    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            if (CustomLaserPointer.instance.LaserHitModel()) 
            {
                //StartCoroutine(clicked());
                RaycastHit hit = CustomLaserPointer.instance.getHit();

                Vector3 normal = (hit.point - layer.transform.position);
                normal.y = 0;
                normal.Normalize();

                markedPositions.Add(hit.point);
                postNumber.text = "Pin" + markedPositions.Count;

                GameObject pNew = Instantiate(arrow);
                pNew.name = "Arrow" + markedPositions.Count;
                pNew.transform.SetParent(Marks.transform,false);
                pNew.transform.position = markedPositions[markedPositions.Count - 1];
                pNew.transform.forward = normal;
                pNew.SetActive(true);
                buildnewTree(pNew);
            }
        }
    }
}
