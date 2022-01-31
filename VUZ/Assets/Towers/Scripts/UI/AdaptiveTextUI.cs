using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class AdaptiveTextUI : MonoBehaviour
{
    public float minSize = 8;
    public float Size = 10;
    Text text;
    float coef;

    void Start()
    {
        text = GetComponent<Text>();
    }
    
    void Update()
    {
        UpdateSize();
    }

    void UpdateSize()
    {
        if (text != null)
        {
            coef = Screen.width / 100f;
            text.fontSize = (int)(Mathf.Max(Size * coef, minSize));
        }
    }

}
