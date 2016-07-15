using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_Bar : MonoBehaviour {

    public RectTransform Bar;

    Vector2 BarDimension;

    public void SetProgress(float t)
    {
        if(BarDimension == Vector2.zero)
        {
            BarDimension = GetComponent<RectTransform>().sizeDelta;
        }
        t = Mathf.Clamp(t, 0, 1);

        Bar.sizeDelta = new Vector2(t * BarDimension.x, Bar.sizeDelta.y);
    }    
    public void SetColor(Color c)
    {
        Bar.GetComponent<Image>().color = c;
    }
    
}
