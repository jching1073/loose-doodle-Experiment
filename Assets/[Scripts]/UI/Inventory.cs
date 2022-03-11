// Inventory.cs
// Owned by Garabatos Inc.
// Created by: Andrea Navarro Zagarra (301185124)

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    public GameObject keyObject;
    public GameObject flashlightObject;
    public GameObject firstAidObject;
    public GameObject eraserObject;
    public TMP_Text firstAidQuantityText;

    private bool k, uE, fl, flA, uHP, uK;

    void Start()
    {
        flashlightObject.SetActive(false);
        keyObject.SetActive(false);
        firstAidObject.SetActive(false);
        eraserObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        uK = UIManager.Instance.keyUse;
        uE = UIManager.Instance.eraseUse;
        flA = UIManager.Instance.flActive;
        uHP = UIManager.Instance.healthpackUse;

        keyObject.SetActive(UIManager.Instance.keyOn);
        eraserObject.SetActive(UIManager.Instance.eraser);
        flashlightObject.SetActive(UIManager.Instance.flashlight);
        firstAidObject.SetActive(UIManager.Instance.firstAid);
        firstAidQuantityText.text = UIManager.Instance.firstAidQuantity.ToString();

        if(uK) ActiveObject(keyObject);
        else inActiveObject(keyObject);
        if(uE) ActiveObject(eraserObject);
        else inActiveObject(eraserObject);
        if(flA) ActiveObject(flashlightObject);
        else inActiveObject(flashlightObject);
        if(uHP) ActiveObject(firstAidObject);
        else inActiveObject(firstAidObject);
        
        if(UIManager.Instance.firstAidQuantity < 1)
        {
            firstAidObject.SetActive(false);
        }
    }

    void ActiveObject(GameObject gO)
    {
        gO.GetComponent<RectTransform>().localScale = new Vector3(1.5f, 1.5f, 1f);
        //gO.rectTransform.localScale = new Vector3(newScale, 1.1f, 1.1f);
        gO.GetComponent<Image>().color = new Color(0.16f, 0.67f, 0.87f, 0.8f);  //Blue for interface #29ABE2
        gO.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1f);
    }

    void inActiveObject(GameObject gO)
    {
        //gO.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 0.2f);
       gO.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.5f);  
        
    }
}
