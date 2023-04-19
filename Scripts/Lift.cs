using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{    
    public GameObject model0;
    public GameObject marks;
    public GameObject user;
    public Canvas raycastCanvas;
    float h = 0;
    float[] hight;

    // Start is called before the first frame update
    void Start()
    {
        user= GameObject.FindGameObjectWithTag("Player");
        marks = GameObject.FindGameObjectWithTag("marks");

        calculateH();
    }

    // Update is called once per frame
    void Update()
    {
        var menuButtonPressed = OVRInput.GetDown(OVRInput.Button.Start);
        if (menuButtonPressed)
        {
            if (raycastCanvas.gameObject.activeSelf) 
            {
                raycastCanvas.gameObject.SetActive(false);
            }
            else
            {
                raycastCanvas.gameObject.SetActive(true);
            }
        }
    }

    private void calculateH()
    {
        if (h == 0) 
        {
            //Debug.Log("dddddddddddd: " + model0.transform.position);
            h = model0.GetComponent<Collider>().bounds.size.y / 5;
            //Debug.Log("dddddddddddd: " + h);
            hight = new float[5];
            hight[2] = model0.transform.position.y + model0.GetComponent<Collider>().bounds.size.y / 2; //middle
            hight[4] = hight[2] + 2 * h; //top
            hight[0] = hight[2] - 2 * h; //bottom
            hight[3] = hight[2] + h; //midtop
            hight[1] = hight[2] - h; //midbott
                                     //Debug.Log("dddddddddddd: " + hight[4]);
                                     //inputPanel.SetActive(false);
        }
        else return;
    }

    public void toTop()
    {
        user.SetActive(false);
        user.transform.position = new Vector3(user.transform.position.x, hight[4], user.transform.position.z);
        user.SetActive(true);
    }
    public void toMid()
    {
        user.SetActive(false);
        user.transform.position = new Vector3(user.transform.position.x, hight[2], user.transform.position.z);
        user.SetActive(true);
    }
    public void toMidtop()
    {
        user.SetActive(false);
        user.transform.position = new Vector3(user.transform.position.x, hight[3], user.transform.position.z);
        user.SetActive(true);
    }
    public void tobott()
    {
        user.SetActive(false);
        user.transform.position = new Vector3(user.transform.position.x, hight[0], user.transform.position.z);
        user.SetActive(true);
    }
    public void tomidbott()
    {
        user.SetActive(false);
        user.transform.position = new Vector3(user.transform.position.x, hight[1], user.transform.position.z);
        user.SetActive(true);
    }

    public void left30() //À≥ ±’Î
    {
        model0.transform.Rotate(0f, 0f, 30f);
        //rotate around model's z axis
        marks.transform.RotateAround(model0.transform.position, model0.transform.forward, 30f);
    }

    public void right30()
    {
        model0.transform.Rotate(0f, 0f, -30f);
        marks.transform.RotateAround(model0.transform.position, model0.transform.forward, -30f);
    }

    public void hideMarks()
    {
        if (marks.activeSelf)
        {
            marks.SetActive(false);
        }
        else
        {
            marks.SetActive(true);
        }
    }

    public void full()
    {
        user.SetActive(false);
        user.transform.position = new Vector3(user.transform.position.x, user.transform.position.y, -70f);
        user.transform.LookAt(new Vector3(0, user.transform.position.y, 3f));
        user.SetActive(true);
    }

    public void inside()
    {
        user.SetActive(false);
        user.transform.position = new Vector3(0, user.transform.position.y, 3f);
        user.SetActive(true);
    }
}
