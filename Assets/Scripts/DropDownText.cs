using UnityEngine;
using UnityEngine.UI;

public class DropDownText : MonoBehaviour
{
    [SerializeField, TextArea(1, 5)] string dropedText;
    [SerializeField] Text text;
    [SerializeField] Transform image;
    
    // Start is called before the first frame update
    void Start()
    {
        text.text = dropedText;
        image.gameObject.SetActive(false);
    }

    public void OnMouseEnter()
    {
        image.gameObject.SetActive(true);
    }

    public void OnMouseExit()
    {
        image.gameObject.SetActive(false);
    }
}
