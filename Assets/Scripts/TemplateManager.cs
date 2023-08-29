using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TemplateManager : MonoBehaviour {

    [SerializeField] Transform parent;

    [SerializeField] Sprite sprite;

    private string Heading = "This is my heading";

    void Start() {
        CreateSampleImage();
        CreateSampleText();
    }

    void CreateSampleImage() { 
        GameObject obj = new GameObject("Sample Image Template");

        // Set Parent
        obj.AddComponent<RectTransform>();
        obj.GetComponent<RectTransform>().SetParent(parent);
        
        // Set Position
        obj.GetComponent<RectTransform>().localPosition = Vector3.zero;
        obj.GetComponent<RectTransform>().localRotation = Quaternion.identity;
        obj.GetComponent<RectTransform>().localScale = Vector3.one;

        // Attach Image
        obj.AddComponent<RawImage>();
        obj.GetComponent<RawImage>().texture = sprite.texture;        
    }
    
    void CreateSampleText() { 
        GameObject obj = new GameObject("Sample Text Template");

        // Set Parent
        obj.AddComponent<RectTransform>();
        obj.GetComponent<RectTransform>().SetParent(parent);
        
        // Set Position
        obj.GetComponent<RectTransform>().localPosition = Vector3.zero;
        obj.GetComponent<RectTransform>().localRotation = Quaternion.identity;
        obj.GetComponent<RectTransform>().localScale = Vector3.one;

        // Attach Text
        obj.AddComponent<TextMeshProUGUI>();
        obj.GetComponent<TextMeshProUGUI>().text = Heading;
        obj.GetComponent<TextMeshProUGUI>().enableAutoSizing = true;
        obj.GetComponent<TextMeshProUGUI>().color = Color.black;
        obj.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
    }

}
