using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TowerGame
{

    public interface ITowerPlace
    {
        void Deactivate();
        Transform GetTransform();
    }

    public class TowerPlace : MonoBehaviour, IPointerClickHandler, ITowerPlace
    {
        
        public void OnPointerClick(PointerEventData eventData)
        {
            GameManager.instance.OpenShop(this);
            //Deactivate();
        }

        public void Deactivate()
        {
            GetComponent<Collider>().enabled = false;
            //gameObject.SetActive(false);
        }

        public Transform GetTransform()
        {
            return transform;
        }
    }
}
