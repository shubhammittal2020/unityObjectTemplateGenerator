using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;
using UnityEngine.UIElements.Experimental;

public class ImageTemplate {
    public string name;
    public Vector3 position;
    public Quaternion rotation;
    public Texture texture;

    //public ImageTemplate() { }

    public ImageTemplate(string name, Vector3 position, Quaternion rotation, Texture texture) { 
        this.name = name;
        this.position = position;
        this.rotation = rotation;
        this.texture = texture;
    }
}

public class TextTemplate {
    public string name;
    public Vector3 position;
    public Quaternion rotation;
    public string text;
    public Color textColor;

    //public TextTemplate() { }

    public TextTemplate(string name, Vector3 position, Quaternion rotation, string text, Color textColor) {
        this.name = name;
        this.position = position;
        this.rotation = rotation;
        this.text = text;
        this.textColor = textColor;
    }
}

public class TemplateManager : MonoBehaviour {

    public static TemplateManager Instance;

    [SerializeField] Transform parent;
    [SerializeField] Texture sampleImage;

    ImageTemplate image1 = new ImageTemplate(string.Empty, Vector3.zero, Quaternion.identity, null);
    TextTemplate text1 = new TextTemplate(string.Empty, Vector3.zero, Quaternion.identity, "", Color.black);

    void Start() {

        SetImageData(image1);
        SetTextData(text1);

        CreateSampleImage(GetImageData(image1));
        CreateSampleText(GetTextData(text1));
    }

    #region Image Template

    void SetImageData(ImageTemplate image) {
        // Manually assigning all values
        image.name = "SampleImageTemplate";

        image.position = Vector3.zero;
        image.rotation = Quaternion.identity;

        image.texture = sampleImage;
    }


    void CreateSampleImage(GameObject obj) {  
        obj.GetComponent<RectTransform>().SetParent(parent);
    }

    GameObject GetImageData(ImageTemplate image) {
        GameObject obj = new GameObject(image.name);

        // Add RectTransform
        obj.AddComponent<RectTransform>();

        // Set Transform
        obj.GetComponent<RectTransform>().localPosition = image.position;
        obj.GetComponent<RectTransform>().localRotation = image.rotation;
        obj.GetComponent<RectTransform>().localScale = Vector3.one;

        // Attach Image
        obj.AddComponent<RawImage>();

        // Set Image
        obj.GetComponent<RawImage>().texture = image.texture;

        return obj;
    }

    #endregion

    #region Text Template

    void SetTextData(TextTemplate myText) {
        // Manually assigning all values
        myText.name = "SampleTextTemplate";

        myText.position = Vector3.zero;
        myText.rotation = Quaternion.identity;

        myText.text = "This is the sample Text";
        myText.textColor = Color.black;
    }

    GameObject GetTextData(TextTemplate myText) {
        GameObject obj = new GameObject(myText.name);

        // Add RectTransform
        obj.AddComponent<RectTransform>();

        // Set Transform
        obj.GetComponent<RectTransform>().localPosition = myText.position;
        obj.GetComponent<RectTransform>().localRotation = myText.rotation;
        obj.GetComponent<RectTransform>().localScale = Vector3.one;

        // Add TextMeshProUGUI
        obj.AddComponent<TextMeshProUGUI>();

        // Set Text
        obj.GetComponent<TextMeshProUGUI>().text = myText.text;
        obj.GetComponent<TextMeshProUGUI>().enableAutoSizing = true;
        obj.GetComponent<TextMeshProUGUI>().color = myText.textColor;
        obj.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;

        return obj;
    }

    void CreateSampleText(GameObject obj) {
        obj.GetComponent<RectTransform>().SetParent(parent);
    }

    #endregion

}
