using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class dataPlot : MonoBehaviour
{
    [SerializeField]
    private Sprite circleSprite;

    private RectTransform graphContainer;

    private List<float> valueList = new List<float>();
    private void Start()
    {
        graphContainer = transform.Find("GraphContainer").GetComponent<RectTransform>();
        //����һ���б����ڲ���
        List<float> valueList = new List<float>() { 1, 2, 4, 9, 16, 25, 36, 49, 64, 81, 100, 80, 50, 20, 10 };
        //valueList = readcsv.instance.getData0().ToList();
        ShowGraph(valueList);
    }

    private void CreateCircle(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));	//���������壬���������һ��ͼƬ���
        gameObject.transform.SetParent(graphContainer, false);			//��ͼƬ��ΪgraphContainer��������
        gameObject.GetComponent<Image>().sprite = circleSprite;			//��ͼƬ��ֵΪInspector�����õ�ͼƬ

        //��ȡ�½�ͼƬ�����RectTransform����ֵ
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;			//����ͼƬλ��
        rectTransform.sizeDelta = new Vector2(2, 2);              //����ͼƬ��С������Ϊ�����������޸�

        //�������佫����ͼƬ��ê����Ϊ�˸��������½�(ԭ��)
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
    }

    private void ShowGraph(List<float> valueList)
    {
        float maxValue = 0;
        foreach (float value in valueList)	//�ҳ��б��е����ֵ
        {
            if (maxValue <= value)
            {
                maxValue = value;
            }
        }

        float graphWidth = graphContainer.sizeDelta.y;	    //��ȡ����graphContainer�Ŀ��
        float graphHeight = graphContainer.sizeDelta.x;	    //��ȡ����graphContainer�ĸ߶�

        float xSpace = graphWidth / (valueList.Count - 1);  //���ݵ�x����ļ��
        float ySpace = graphHeight / maxValue;		    //���ݵ�y����ı���

        
        
        for (int i = 0; i < valueList.Count; i++)
        {
            float xPos = i * xSpace;			    //x����Ϊ���Թ̶�����
            float yPos = ySpace * valueList[i];		    //y���������б������ֵΪ�����߶ȣ���ֵ�Ĵ�С�����ֵ�ı���ȡ�߶�
            CreateCircle(new Vector2(xPos, yPos));	    //������
            CreateBar(new Vector2(xPos, yPos), xSpace * 0.01f);
        }
        
    }

    private GameObject CreateBar(Vector2 pos, float width)
    {
        GameObject gameObject = new GameObject("bar", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();

        rectTransform.anchoredPosition = new Vector2(pos.x, 0f);
        rectTransform.sizeDelta = new Vector2(width, pos.y);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);

        rectTransform.pivot = new Vector2(0.5f, 0f);
        return gameObject;
    }
}
