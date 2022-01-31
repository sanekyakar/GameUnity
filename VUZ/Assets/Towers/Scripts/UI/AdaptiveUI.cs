using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class AdaptiveUI : MonoBehaviour
{
    public Rect RelativeRect;
    public Vector2 MinSize;
    public bool AbsoluteSize;
    public bool OnlySize;
    RectTransform tr, parent;

    private void Start()
    {
        parent = transform.parent.GetComponent<RectTransform>();
        tr = GetComponent<RectTransform>();
        UpdSize();
    }

    private void Update()
    {
        UpdSize();
    }

    void UpdSize()
    {
        var par = parent.sizeDelta;
        if (AbsoluteSize) par = new Vector2(Screen.width, Screen.height);
        var siz = new Vector2(par.x * RelativeRect.width, par.y * RelativeRect.height);
        if (MinSize.x != 0) siz.x = Mathf.Max(siz.x, MinSize.x);
        if (MinSize.y != 0) siz.y = Mathf.Max(siz.y, MinSize.y);
        tr.sizeDelta = siz;
        if(!OnlySize) tr.anchoredPosition = new Vector2(par.x * RelativeRect.x, par.y * RelativeRect.y);
    }

}
