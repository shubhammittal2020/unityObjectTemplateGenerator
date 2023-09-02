using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

//public class Positions {
//    public float x { get; set; }
//    public float y { get; set; }
//    public float z { get; set; }
//}

//public class Rotations {
//    public float a { get; set; }
//    public float b { get; set; }
//    public float c { get; set; }
//    public float w { get; set; }
//}

//public class ImageTemplate {
//    public string name;
//    public Transform parent;

//    public float parentPosX;
//    public float parentPosY;
//    public float parentPosZ;

//    public float posX;
//    public float posY;
//    public float posZ;
    
//    public float rotX;
//    public float rotY;
//    public float rotZ;
//    public float rotW;

//    //public Texture texture;

//    //public ImageTemplate() { }

//    public ImageTemplate(string name, Transform parent, float a, float b, float c, float x, float y, float z, float t, float u, float v, float w) { 
//        this.name = name;
//        this.parent = parent;

//        this.parentPosX = a;
//        this.parentPosY = b; 
//        this.parentPosZ = c;
        
//        this.posX = x;
//        this.posY = y; 
//        this.posZ = z;
        
//        this.rotX = t;
//        this.rotY = u;
//        this.rotZ = v;
//        this.rotZ = w;

//        //this.texture = texture;
//    }
//}

//public class TextTemplate {
//    public string name;
//    public Transform parent;

//    public float x;
//    public float y;
//    public float z;

//    public float a;
//    public float b;
//    public float c;
//    public float w;

//    public string text;
//    public Color textColor;

//    //public TextTemplate() { }

//    public TextTemplate(string name, float x, float y, float z, float a, float b, float c, float w, string text, Color textColor) {
//        this.name = name;
        
//        //this.parent = parent;

//        this.x = x;
//        this.y = y;
//        this.z = z;

//        this.a = a;
//        this.b = b;
//        this.c = c;
//        this.w = w;

//        this.text = text;
//        this.textColor = textColor;
//    }
//}

public class AdTemplate {
    public string appName;
    public int starRate;
    public string price;
    public string description;
    public string buttonColor;
    public string buttonText;

    public AdTemplate(string appName, int starRate, string price, string description, string buttonColor, string buttonText) {
        this.appName = appName;
        this.starRate = starRate;
        this.price = price;
        this.description = description;
        this.buttonColor = buttonColor;
        this.buttonText = buttonText;
    }
}

public class TemplateManager : MonoBehaviour {

    new Camera camera;
    RectTransform parent;

    [SerializeField] Texture2D adLogo;
    [SerializeField] Texture2D appLogo;
    [SerializeField] string appName;
    [SerializeField] Texture2D starFilled;
    [SerializeField] Texture2D starEmpty;
    [SerializeField] int starCount;
    [SerializeField] int totalStars;
    [SerializeField] string price;
    [SerializeField] string description;
    [SerializeField] string buttonColor;
    [SerializeField] string buttonText;
    

    private void Awake() {
        camera = Camera.main;
    }

    void Start() {
        parent = CreateCanvas();
    }

    public void AssignValuesAndDisplayAd() {
        AssignValues();
    }

    void AssignValues() {
        AdTemplate adTemplate = new AdTemplate(appName, starCount, price, description, buttonColor, buttonText);
        SetValues(adTemplate);
        
        DisplayAd();
    }

    void DisplayAd() {
        CreateHolder(parent);
    }

    void SetValues(AdTemplate adTemplate) {
        appName = adTemplate.appName;
        starCount = adTemplate.starRate;
        price = adTemplate.price;
        description = adTemplate.description;
        buttonColor = adTemplate.buttonColor;
        buttonText = adTemplate.buttonText;
    }

    RectTransform CreateCanvas() {
        CreateEventSystem();

        GameObject obj = new GameObject("Canvas");

        obj.AddComponent<RectTransform>();
        obj.AddComponent<Canvas>();
        obj.AddComponent<CanvasScaler>();
        obj.AddComponent<GraphicRaycaster>();

        Canvas canvas = obj.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = camera;
        canvas.planeDistance = 100;

        CanvasScaler canvasScaler = obj.GetComponent<CanvasScaler>();
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScaler.referenceResolution = new Vector2(1080, 1920);
        canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        canvasScaler.matchWidthOrHeight = 1;
        canvasScaler.referencePixelsPerUnit = 100;

        GraphicRaycaster graphicRaycaster = obj.GetComponent<GraphicRaycaster>();
        graphicRaycaster.ignoreReversedGraphics = true;

        return obj.GetComponent<RectTransform>();
    }

    void CreateEventSystem() {
        if (EventSystem.current != null) return;

        GameObject obj = new GameObject("Event System");

        obj.AddComponent<EventSystem>();
        obj.AddComponent<StandaloneInputModule>();
    }

    void CreateHolder(RectTransform myTransform) {
        RectTransform holderTransform = AddChildInHierarchy("Holder", myTransform);

        // Set position and size
        holderTransform.localPosition = Vector3.zero;
        holderTransform.sizeDelta = new Vector2(500, 300);

        holderTransform.gameObject.AddComponent<Image>();

        AddAdLogo(holderTransform);
        AddAdData(holderTransform);
    }

    void AddAdLogo(RectTransform myTransform) {
        RectTransform childTransform = AddChildInHierarchy("adTagHolder", myTransform);

        // Set position and size
        childTransform.localPosition = new Vector3(-225, 130, 0);
        childTransform.sizeDelta = new Vector2(40, 20);

        CreateImage("RawImage_adChoiceTag", childTransform, 0, 0, 0, 40, 20, adLogo);
    }

    void AddAdData(RectTransform myTransform) {
        RectTransform childTransform = AddChildInHierarchy("Padding", myTransform);

        // Set position and size
        childTransform.localPosition = new Vector3(0, 15, 0);
        childTransform.sizeDelta = new Vector2(500, 200);

        AddAdDetails(childTransform);
        GetTextData("TextBody", childTransform, 0, -55, 0, 490, 75, description, true, Color.black, TextAlignmentOptions.Left);
        AddInstallButton("InstallButton", "Text_CallToAction", buttonText, childTransform);
    }

    void AddAdDetails(RectTransform myTransform) {
        RectTransform childTransform = AddChildInHierarchy("AppDetail", myTransform);

        // Set position and size
        childTransform.localPosition = new Vector3(0, 40, 0);
        childTransform.sizeDelta = new Vector2(500, 120);

        DrawAdLogo(childTransform);
        FillAdDetails(childTransform);
    }

    void DrawAdLogo(RectTransform myTransform) {
        RectTransform childTransform = AddChildInHierarchy("AppIconHolder", myTransform);

        // Set position and size
        childTransform.localPosition = new Vector3(-195, 0, 0);
        childTransform.sizeDelta = new Vector2(100, 100);

        CreateImage("RawImage_adIcon", childTransform, 0, 0, 0, 100, 100, appLogo);
    }

    void FillAdDetails(RectTransform myTransform) {
        RectTransform childTransform = AddChildInHierarchy("AppInfo", myTransform);

        // Set position and size
        childTransform.localPosition = new Vector3(50, 0, 0);
        childTransform.sizeDelta = new Vector2(380, 100);

        GetTextData("Text_adHeadline", childTransform, 0, 30, 0, 380, 40, appName, true, Color.black, TextAlignmentOptions.Left);

        RectTransform child2Transform = AddChildInHierarchy("StarRating", childTransform);

        // Set position and size
        child2Transform.localPosition = new Vector3(0, -6.5f, 0);
        child2Transform.sizeDelta = new Vector2(385, 25);

        for (int i = 0; i < starCount; i++) {
            CreateImage("Star" + (i+1).ToString(), child2Transform, -185 + (i * 15), 0, 0, 15, 15, starFilled);
        }
        for (int i = starCount; i < totalStars; i++) {
            CreateImage("Star" + (i+1).ToString(), child2Transform, -185 + (i * 15), 0, 0, 15, 15, starEmpty);
        }

        GetTextData("priceText", childTransform, 0, -35, 0, 380, 25, price, true, Color.black, TextAlignmentOptions.Left);
    }

    RectTransform AddChildInHierarchy(string name, RectTransform myTransform) {
        GameObject obj = new GameObject(name);

        // Add RectTransform
        obj.AddComponent<RectTransform>();
        RectTransform rect = obj.GetComponent<RectTransform>();

        // Local Local & Set Parent
        RectTransform localRect = rect;
        rect.SetParent(myTransform);

        // Set Transform
        rect.localPosition = new Vector3(localRect.localPosition.x, localRect.localPosition.x, localRect.localPosition.x);
        rect.localRotation = new Quaternion(localRect.localRotation.x, localRect.localRotation.y, localRect.localRotation.z, localRect.localRotation.w);
        rect.localScale = Vector3.one;

        return rect;
    }

    void OnButtonClicked() {
        Debug.LogError("Clicked on install button");  //Redirect to application
    }

    void CreateImage(string name, RectTransform parent, float posX, float posY, float posZ, float width, float height, Texture2D imagetexture) {
        GameObject obj = new GameObject(name);

        
        // Add RectTransform
        obj.AddComponent<RectTransform>();

        // Set Parent
        obj.GetComponent<RectTransform>().SetParent(parent);

        // Set Transform
        obj.GetComponent<RectTransform>().localPosition = new Vector3(posX, posY, posZ);
        obj.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        obj.GetComponent<RectTransform>().localRotation = new Quaternion(0, 0, 0, 0);
        obj.GetComponent<RectTransform>().localScale = Vector3.one;

        // Attach Image
        obj.AddComponent<RawImage>();

        // Set Image
        obj.GetComponent<RawImage>().texture = imagetexture;
    }

    void GetTextData(string name, RectTransform parent, float posX, float posY, float posZ, float width, float height, string text, bool autoSize, Color color, TextAlignmentOptions alignmentOption) {
        GameObject obj = new GameObject(name);

        // Add RectTransform
        obj.AddComponent<RectTransform>();

        // Set Parent
        obj.GetComponent<RectTransform>().SetParent(parent);

        // Set Transform
        obj.GetComponent<RectTransform>().localPosition = new Vector3(posX, posY, posZ);
        obj.GetComponent<RectTransform>().localRotation = new Quaternion(0, 0, 0, 0);
        obj.GetComponent<RectTransform>().localScale = Vector3.one;
        obj.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);


        // Add TextMeshProUGUI
        obj.AddComponent<TextMeshProUGUI>();

        // Set Text Elements
        obj.GetComponent<TextMeshProUGUI>().text = text;
        obj.GetComponent<TextMeshProUGUI>().enableAutoSizing = autoSize;
        obj.GetComponent<TextMeshProUGUI>().color = color;
        obj.GetComponent<TextMeshProUGUI>().alignment = alignmentOption;
    }

    void AddInstallButton(string buttonObjName, string textObjName, string text, RectTransform myTransform) {
        RectTransform buttonTransform = AddChildInHierarchy(buttonObjName, myTransform);

        // Set position and size
        buttonTransform.localPosition = new Vector3(0, -130, 0);
        buttonTransform.sizeDelta = new Vector2(480, 50);

        GameObject buttonObj = buttonTransform.gameObject;

        buttonObj.AddComponent<Image>();
        Color colorFromHex;
        UnityEngine.ColorUtility.TryParseHtmlString("#2FCCB2", out colorFromHex);
        buttonObj.GetComponent<Image>().color = colorFromHex;

        buttonObj.AddComponent<Button>();
        buttonObj.GetComponent<Button>().onClick.AddListener(OnButtonClicked);

        RectTransform TextTransform = AddChildInHierarchy(textObjName, buttonTransform);

        // Set position and size
        TextTransform.localPosition = new Vector3(0, 0, 0);
        TextTransform.sizeDelta = new Vector2(460, 40);

        GameObject textObj = TextTransform.gameObject;
        textObj.AddComponent<TextMeshProUGUI>();
        textObj.GetComponent<TextMeshProUGUI>().text = text;
        textObj.GetComponent<TextMeshProUGUI>().color = Color.black;
        textObj.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
    }

}
