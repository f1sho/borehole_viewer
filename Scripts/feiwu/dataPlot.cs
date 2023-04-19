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
        //声明一个列表用于测试
        List<float> valueList = new List<float>() { 1, 2, 4, 9, 16, 25, 36, 49, 64, 81, 100, 80, 50, 20, 10 };
        //valueList = readcsv.instance.getData0().ToList();
        ShowGraph(valueList);
    }

    private void CreateCircle(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));	//生成新物体，该物体包含一个图片组件
        gameObject.transform.SetParent(graphContainer, false);			//将图片设为graphContainer的子物体
        gameObject.GetComponent<Image>().sprite = circleSprite;			//将图片赋值为Inspector中设置的图片

        //获取新建图片物体的RectTransform并赋值
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;			//设置图片位置
        rectTransform.sizeDelta = new Vector2(2, 2);              //设置图片大小，可设为公共变量来修改

        //下面两句将生成图片的锚点设为了父物体左下角(原点)
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
    }

    private void ShowGraph(List<float> valueList)
    {
        float maxValue = 0;
        foreach (float value in valueList)	//找出列表中的最大值
        {
            if (maxValue <= value)
            {
                maxValue = value;
            }
        }

        float graphWidth = graphContainer.sizeDelta.y;	    //获取画布graphContainer的宽度
        float graphHeight = graphContainer.sizeDelta.x;	    //获取画布graphContainer的高度

        float xSpace = graphWidth / (valueList.Count - 1);  //数据点x坐标的间距
        float ySpace = graphHeight / maxValue;		    //数据的y坐标的比例

        
        
        for (int i = 0; i < valueList.Count; i++)
        {
            float xPos = i * xSpace;			    //x坐标为线性固定增长
            float yPos = ySpace * valueList[i];		    //y坐标是以列表中最大值为画布高度，按值的大小与最大值的比例取高度
            CreateCircle(new Vector2(xPos, yPos));	    //画出点
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
