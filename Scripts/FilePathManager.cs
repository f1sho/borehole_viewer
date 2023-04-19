using UnityEngine;
using UnityEngine.UI;

public class FilePathManager : MonoBehaviour
{
    public static FilePathManager Instance;

    public string username { get; private set; }

    public TMPro.TextMeshProUGUI displayUsername;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void readStringInput(string textIn)
    {
        username = textIn;
        displayUsername.text = "Your user name is: " + username;
    }
}

