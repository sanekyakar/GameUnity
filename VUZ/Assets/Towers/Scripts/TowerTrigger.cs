using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerGame
{
    public class TowerTrigger : MonoBehaviour
    {

        void Start()
        {
            var dist = GetComponentInParent<Tower>().MaxDistance;
            GetComponent<SphereCollider>().radius = dist;
        }

        void OnTriggerEnter(Collider collision)
        {
            var e = collision.GetComponent<Enemy>();
            if(e) GetComponentInParent<TowerShooter>().AddEnemy(e);
        }

        void OnTriggerExit(Collider collision)
        {
            var e = collision.GetComponent<Enemy>();
            if (e) GetComponentInParent<TowerShooter>().RemoveEnemy(e);
        }

    }
}