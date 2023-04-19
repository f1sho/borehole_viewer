using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AddressableAssets.ResourceLocators;


public class AddresablesManager : MonoBehaviour
{
    [SerializeField]
    private AssetReference modelReference;

    [SerializeField]
    private AssetReference _addressableTextAsset;

    [SerializeField]
    private AssetReferenceTexture2D chart1Reference;

    [SerializeField]
    private AssetReferenceTexture2D chart2Reference;

    [SerializeField]
    private AssetReferenceTexture2D chart3Reference;

    [SerializeField]
    private AssetReferenceTexture2D chart4Reference;

    [SerializeField]
    private AssetReferenceTexture2D chart5Reference;

    [SerializeField]
    private AssetReferenceTexture2D chart6Reference;

    [SerializeField]
    private AssetReferenceTexture2D chart7Reference;

    [SerializeField]
    private GameObject geoLayer;

    [SerializeField]
    private Lift lift;

    [SerializeField]
    private QuadTreeLoader quadeTreeLo;

    [SerializeField]
    private readcsv readcs;

    [SerializeField]
    private RawImage chart1;

    [SerializeField]
    private RawImage chart2;

    [SerializeField]
    private RawImage chart3;

    [SerializeField]
    private RawImage chart4;

    [SerializeField]
    private RawImage chart5;

    [SerializeField]
    private RawImage chart6;

    [SerializeField]
    private RawImage chart7;

    private GameObject model;

    private void Awake()
    {
        //Addressables.InitializeAsync().Completed += AddresablesManager_Completed;
        lift.enabled = false;
        readcs.enabled = false;
        quadeTreeLo.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        Addressables.InitializeAsync().Completed += AddresablesManager_Completed;
    }

    private void AddresablesManager_Completed(AsyncOperationHandle<IResourceLocator> obj)
    {
        modelReference.InstantiateAsync().Completed += (go) =>
          {
              model = go.Result;
              model.gameObject.tag = "model";
              model.transform.SetParent(geoLayer.transform, false);
              model.transform.localPosition = Vector3.zero;
              lift.model0 = model;
              quadeTreeLo.layer = model;
              readcs.layer = model;
              Debug.Log("loaded");
              lift.enabled = true;
              readcs.enabled = true;
              quadeTreeLo.enabled = true;

              chart1Reference.LoadAssetAsync<Texture2D>().Completed += (c1) =>
              {
                  chart1.texture = c1.Result;
                  chart1.SetNativeSize();
                  var sc = model.GetComponent<Collider>().bounds.size.y / chart1.rectTransform.sizeDelta.y;
                  chart1.transform.localScale = new Vector3(sc, sc, sc);
                  chart1.transform.position = geoLayer.transform.position + new Vector3(model.GetComponent<Collider>().bounds.max.x
                      + chart1.rectTransform.sizeDelta.x * sc / 2, model.GetComponent<Collider>().bounds.size.y / 2, 0);
                  //chart1.transform.Rotate(new Vector3(0, 0, -90f));
              };
              chart2Reference.LoadAssetAsync<Texture2D>().Completed += (c2) =>
              {
                  chart2.texture = c2.Result;
                  chart2.SetNativeSize();
                  var sc = model.GetComponent<Collider>().bounds.size.y / chart2.rectTransform.sizeDelta.y;
                  chart2.transform.localScale = new Vector3(sc, sc, sc);
                  chart2.transform.position = geoLayer.transform.position + new Vector3(model.GetComponent<Collider>().bounds.max.x
                      + chart2.rectTransform.sizeDelta.x * sc / 2, model.GetComponent<Collider>().bounds.size.y / 2, 0);
                  //chart2.transform.Rotate(new Vector3(0, 0, -90f));
              };
              chart3Reference.LoadAssetAsync<Texture2D>().Completed += (c3) =>
              {
                  chart3.texture = c3.Result;
                  chart3.SetNativeSize();
                  var sc = model.GetComponent<Collider>().bounds.size.y / chart3.rectTransform.sizeDelta.x;
                  chart3.transform.localScale = new Vector3(sc, sc, sc);
                  chart3.transform.position = geoLayer.transform.position + new Vector3(model.GetComponent<Collider>().bounds.max.x 
                      + chart3.rectTransform.sizeDelta.y * sc/ 2, model.GetComponent<Collider>().bounds.size.y/2, 0);
                  chart3.transform.Rotate(new Vector3(0, 0, -90f));
                  Color co = chart3.color;
                  co.a = 0.7f;
              };
              chart4Reference.LoadAssetAsync<Texture2D>().Completed += (c4) =>
              {
                  chart4.texture = c4.Result;
                  chart4.SetNativeSize();
                  chart4.transform.position = geoLayer.transform.position + new Vector3(model.GetComponent<Collider>().bounds.max.x + 0.2f, 0, 0);
                  var sc = model.GetComponent<Collider>().bounds.size.y / chart4.rectTransform.sizeDelta.x;
                  chart4.transform.localScale = new Vector3(sc, sc, sc);
                  chart4.transform.position = geoLayer.transform.position + new Vector3(model.GetComponent<Collider>().bounds.max.x
                      + chart4.rectTransform.sizeDelta.y * sc / 2, model.GetComponent<Collider>().bounds.size.y / 2, 0);
                  chart4.transform.Rotate(new Vector3(0, 0, -90f));
                  Color co = chart4.color;
                  co.a = 0.7f;
              };
              chart5Reference.LoadAssetAsync<Texture2D>().Completed += (c5) =>
              {
                  chart5.texture = c5.Result;
                  chart5.SetNativeSize();
                  chart5.transform.position = geoLayer.transform.position + new Vector3(model.GetComponent<Collider>().bounds.max.x + 0.2f, 0, 0);
                  var sc = model.GetComponent<Collider>().bounds.size.y / chart5.rectTransform.sizeDelta.x;
                  chart5.transform.localScale = new Vector3(sc, sc, sc);
                  chart5.transform.position = geoLayer.transform.position + new Vector3(model.GetComponent<Collider>().bounds.max.x
                      + chart5.rectTransform.sizeDelta.y * sc / 2, model.GetComponent<Collider>().bounds.size.y / 2, 0);
                  chart5.transform.Rotate(new Vector3(0, 0, -90f));
                  Color co = chart5.color;
                  co.a = 0.7f;
              };
              chart6Reference.LoadAssetAsync<Texture2D>().Completed += (c6) =>
              {
                  chart6.texture = c6.Result;
                  chart6.SetNativeSize();
                  chart6.transform.position = geoLayer.transform.position + new Vector3(model.GetComponent<Collider>().bounds.max.x + 0.2f, 0, 0);
                  var sc = model.GetComponent<Collider>().bounds.size.y / chart6.rectTransform.sizeDelta.x;
                  chart6.transform.localScale = new Vector3(sc, sc, sc);
                  chart6.transform.position = geoLayer.transform.position + new Vector3(model.GetComponent<Collider>().bounds.max.x
                      + chart6.rectTransform.sizeDelta.y * sc / 2, model.GetComponent<Collider>().bounds.size.y / 2, 0);
                  chart6.transform.Rotate(new Vector3(0, 0, -90f));
                  Color co = chart6.color;
                  co.a = 0.7f;
              };
              chart7Reference.LoadAssetAsync<Texture2D>().Completed += (c7) =>
              {
                  chart7.texture = c7.Result;
                  chart7.SetNativeSize();
                  chart7.transform.position = geoLayer.transform.position + new Vector3(model.GetComponent<Collider>().bounds.max.x + 0.2f, 0, 0);
                  var sc = model.GetComponent<Collider>().bounds.size.y / chart7.rectTransform.sizeDelta.x;
                  chart7.transform.localScale = new Vector3(sc, sc, sc);
                  chart7.transform.position = geoLayer.transform.position + new Vector3(model.GetComponent<Collider>().bounds.max.x
                      + chart7.rectTransform.sizeDelta.y * sc / 2, model.GetComponent<Collider>().bounds.size.y / 2, 0);
                  chart7.transform.Rotate(new Vector3(0, 0, -90f));
                  Color co = chart7.color;
                  co.a = 0.7f;
              };
          };
        
        _addressableTextAsset.LoadAssetAsync<TextAsset>().Completed += handle =>
        {
            readcs.datafile = handle.Result;
            Addressables.Release(handle);
        };
        
    }

    private void OnDestroy()
    {
        modelReference.ReleaseInstance(model);
        chart1Reference.ReleaseAsset();
        chart2Reference.ReleaseAsset();
        chart3Reference.ReleaseAsset();
        chart4Reference.ReleaseAsset();
        chart5Reference.ReleaseAsset();
        chart6Reference.ReleaseAsset();
        chart7Reference.ReleaseAsset();
    }
}
