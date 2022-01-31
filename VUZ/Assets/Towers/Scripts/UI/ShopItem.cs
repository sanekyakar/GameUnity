using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerGame
{
    public class ShopItem : MonoBehaviour
    {
        void Start()
        {
            var text = transform.Find("Text").GetComponent<Text>();
            text.text = ShopManager.instance.towers[transform.GetSiblingIndex()].Price.ToString();
        }

    }
}