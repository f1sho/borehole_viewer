using UnityEngine;
using UnityEngine.UI;
using System.IO;
using SimpleFileBrowser;
using System.Collections;

public class FileBrowserHandler : MonoBehaviour
{
    public Button csvButton;
    public TMPro.TextMeshProUGUI csvFilePath;

    private void Start()
    {
        csvButton.onClick.AddListener(ShowFileBrowserForCSV);

        FileBrowser.HideDialog();
        FileBrowser.SingleClickMode = true;
    }

    private void ShowFileBrowserForCSV()
    {
        FileBrowser.SetFilters(true, new FileBrowser.Filter("csv Files", ".csv"));
        //FileBrowser.SetDefaultFilter(".*");
        FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".zip", ".rar", ".exe");

        StartCoroutine(OpenFileBrowserForCSV());
    }

    private IEnumerator OpenFileBrowserForCSV()
    {
        yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.Files, false, null, null, "Load", "Select");

        if (FileBrowser.Success)
        {
            string filePath = FileBrowser.Result[0];
            Debug.Log("Selected file: " + filePath);
            
            csvFilePath.text = filePath;

            // Set the selected file path in FilePathManager
            //FilePathManager.Instance.SetCSVFilePath(filePath);
        }
    }
}
