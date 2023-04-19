using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XCharts.Runtime;
using UnityEngine.UI;

public class chartManager : MonoBehaviour
{
    public Canvas chartCanvas;
    public GameObject[] charts;
    void Start()
    {
        chartCanvas.enabled = true;
    }

    public void hideCHarts()
    {
        foreach (GameObject chart in charts)
        {
            chart.SetActive(false);
        }
    }

    public void showChart1()
    {
        if (charts[0].activeSelf)
        {
            charts[0].SetActive(false);
        }
        else
        {
            charts[0].SetActive(true);
        }
    }

    public void showChart2()
    {
        if (charts[1].activeSelf)
        {
            charts[1].SetActive(false);
        }
        else
        {
            charts[1].SetActive(true);
        }
    }

    public void showChart3()
    {
        if (charts[2].activeSelf)
        {
            charts[2].SetActive(false);
        }
        else
        {
            charts[2].SetActive(true);
        }
    }

    public void showChart4()
    {
        if (charts[3].activeSelf)
        {
            charts[3].SetActive(false);
        }
        else
        {
            charts[3].SetActive(true);
        }
    }

    public void showChart5()
    {
        if (charts[4].activeSelf)
        {
            charts[4].SetActive(false);
        }
        else
        {
            charts[4].SetActive(true);
        }
    }

    public void showChart6()
    {
        if (charts[5].activeSelf)
        {
            charts[5].SetActive(false);
        }
        else
        {
            charts[5].SetActive(true);
        }
    }

    public void showChart7()
    {
        if (charts[6].activeSelf)
        {
            charts[6].SetActive(false);
        }
        else
        {
            charts[6].SetActive(true);
        }
    }
}
