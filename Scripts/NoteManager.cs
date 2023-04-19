using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class NoteManager : MonoBehaviour,IDataPersistence
{
    public TMPro.TMP_InputField chatInput;
    public TMPro.TextMeshProUGUI chatText;
    public ScrollRect scrollRect;
    string username;
    public Canvas inputPanel;
    public TMPro.TextMeshProUGUI noteTitle;
    public GameObject currentClicked;
    public int currentClickedPostIndex;
    List<AnnotationToStore> annoted = new List<AnnotationToStore>();

    public void LoadData(NoteData notes)
    {
        annoted = notes.annotationToStores;
    }

    public void SaveData(NoteData notes)
    {
        notes.annotationToStores = annoted;
    }

    void Start()
    {
        inputPanel.gameObject.SetActive(false);
        if (FilePathManager.Instance.username == null) 
        {
            username = "Default";
        }
        else
        {
            username = FilePathManager.Instance.username;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showNotes()
    {
        if (inputPanel.gameObject.activeSelf)
        {
            inputPanel.gameObject.SetActive(false);
        }
        else
        {
            inputPanel.gameObject.SetActive(true);
            findNote();
        }
    }

    public void getCurrentClick()
    {
        Canvas parentCanvas = EventSystem.current.currentSelectedGameObject.GetComponentInParent<Canvas>();
        currentClicked = parentCanvas.transform.parent.gameObject;
        if (currentClicked != null)
        {
            for (int i = 0; i < annoted.Count; i++)
            {
                if (annoted[i] == null) continue;
                else
                {
                    if (annoted[i].postName == currentClicked.name)
                    {
                        currentClickedPostIndex = i;
                    }
                }
            }
        }
        else return;
    }

    public void findNote()
    {
        noteTitle.text = "Note of " + currentClicked.name;
        chatText.text = annoted[currentClickedPostIndex].noteToStore;
    }

    public void clearAllInThisNote()
    {
        chatText.text = "New note...";
        annoted[currentClickedPostIndex].noteToStore = "";
        DataPersistenceManager.instance.SaveNotes();
    }

    public void deletePost()
    {
        QuadTreeLoader.instance.Posts()[currentClickedPostIndex].arrow.SetActive(false);
        QuadTreeLoader.instance.Posts()[currentClickedPostIndex].post.SetActive(false);
        //QuadTreeLoader.instance.Posts()[currentClickedPostIndex] = null;
        annoted[currentClickedPostIndex] = null;
        DataPersistenceManager.instance.SaveNotes();
    }

    public void enterText()
    {
        if (chatInput.text != "")
        {
            string addText = "\n  " + "<color=blue>" + username + "</color>:\u00A0" + chatInput.text;
            chatText.text += addText;
            chatInput.text = "";
            chatInput.ActivateInputField();
            Canvas.ForceUpdateCanvases();
            scrollRect.verticalNormalizedPosition = 0f;
            Canvas.ForceUpdateCanvases();
        }
        annoted[currentClickedPostIndex].noteToStore = chatText.text;
    }

    public void deleteAllPosts()
    {
        foreach(Annotation postToDestroy in QuadTreeLoader.instance.Posts())
        {
            Destroy(postToDestroy.post);
            Destroy(postToDestroy.arrow);
        }
        annoted.Clear();
        QuadTreeLoader.instance.markedPositions.Clear();
        DataPersistenceManager.instance.SaveNotes();
    }
}
