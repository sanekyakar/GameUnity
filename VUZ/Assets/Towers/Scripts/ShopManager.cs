using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerGame
{

    public class ShopManager : MonoBehaviour
    {

        public static ShopManager instance;

        [System.Serializable]
        public class ShopTower
        {
            public int Price = 100;
            public Tower Tower;
        }

        public ShopTower[] towers;

        void Awake()
        {
            instance = this;
        }

        public ShopTower TryBuy(int i)
        {
            if (GameManager.instance.Money >= towers[i].Price)
            {
                GameManager.instance.Money -= towers[i].Price;
                return towers[i];
            }
            return null;
        }

    }
}