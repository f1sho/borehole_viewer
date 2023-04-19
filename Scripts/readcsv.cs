using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;
using System.Runtime.Serialization;
//using System.Linq;

public class readcsv : MonoBehaviour
{
    double[] depths;
    string[] lines;
    double[] unityDepthY;
    public List<List<float>> numericDataSets;

    public GameObject layer;
    public string filePath;
    public TextAsset datafile;

    public List<string> lineslist;
    List<string> dataName = new List<string>();
    List<string> dataUnit = new List<string>();

    public static readcsv instance;

    public void Start()
    {
        instance = this;

        //filePath = Path.Combine(Application.persistentDataPath, filename);
        //filePath = Path.Combine(Application.dataPath, "DeganwyBoreholeData.csv");
        //filePath = FilePathManager.Instance.csvFilePath;
        //filePath = Path.Combine("/storage/emulated/0/boreholeData", filename);
        //filePath = "Assets/Materials/Materials/DeganwyBoreholeData.csv";
        layer = GameObject.FindGameObjectWithTag("model");



        //ReadData(filePath);

        //datafile = Resources.Load<TextAsset>("DeganwyBoreholeData");
        lines = datafile.text.Split('\n');
        //Debug.Log("aaaaaaaaaaaaaaaa"+lines[5]);

        depths = new double[lines.Length-2];
        for (int i = 2; i < lines.Length; i++)
        {
            if (string.IsNullOrEmpty(lines[i])) continue;
            lines[i] = lines[i].Replace("\r", "");
            depths[i-2] = double.Parse(lines[i].Split(',')[0]);            
        }
        Debug.Log("de65" + depths[depths.Length - 2]);
        Debug.Log("de40" + depths[0]);

        //double totalDepth = depths[depths.Length-1] - depths[0]; //streamReader
        double totalDepth = depths[depths.Length - 2] - depths[0]; //textAsset
        double accuracy = depths[1] - depths[0];
        int parts = (int)(totalDepth / accuracy);
        double h = layer.GetComponent<Collider>().bounds.size.y / parts;
        double maxY = layer.GetComponent<Collider>().bounds.max.y;
        
        //Debug.Log("totalDepth" + totalDepth);
        //Debug.Log("accuracy" + accuracy);
        //Debug.Log("parts" + parts);
        //Debug.Log("hhhhhh" + h);
        //Debug.Log("maxYYYYY" + maxY);
        //Debug.Log("minYYYYY" + minY);
        
        unityDepthY = new double[depths.Length];
        for (int i = 0; i < depths.Length; i++)
        {
            //unityDepthY[i] = maxY + i * h; //textAsset read
            unityDepthY[i] = maxY - i * h; //streamReader read
            //Debug.Log("eeeeeeee" + unityDepthY[i]);
        }

        //Read7DataSets();
        //Debug.Log(numericDataSets[0][50]);
    }

    void Read7DataSets()
    {
        numericDataSets = new List<List<float>>();
        for (int i = 0; i < 8; i++)
        {
            numericDataSets.Add(new List<float>());
            for (int j = 2; j < lines.Length; j++)
            {
                if (string.IsNullOrEmpty(lines[j])) continue;
                lines[j] = lines[j].Replace("\r", "");
                numericDataSets[i].Add(float.Parse(lines[j].Split(',')[i]));
            }
            dataName.Add(lines[0].Split(',')[i]);
            dataUnit.Add(lines[1].Split(',')[i]);
        }
    }


    public void ReadData(string path)
    {
        FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);
        StreamReader sr = new StreamReader(fs, System.Text.Encoding.Default);
        if (null == sr) return;
        while (!sr.EndOfStream)
        {
            var line = sr.ReadLine();

            lineslist.Add(line);
        }
        sr.Close();
        lines = lineslist.ToArray();
    }

    public double getUnityY(int j)
    {
        return unityDepthY[j];
    }

    public string[] getLines()
    {
        return lines;
    }

    public List<string> getDataName()
    {
        return dataName;
    }

    public List<string> getDataUnit()
    {
        return dataUnit;
    }
}
