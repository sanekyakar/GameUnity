using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TowerGame
{

    public class Tower : MonoBehaviour, IPointerClickHandler, ITowerPlace
    {
        public float MaxDistance = 5;

        public void Deactivate()
        {
            Destroy(gameObject);
        }

        public Transform GetTransform()
        {
            return transform;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            GameManager.instance.OpenShop(this);
        }

        void Start()
        {

        }

        void Update()
        {

        }
    }

}