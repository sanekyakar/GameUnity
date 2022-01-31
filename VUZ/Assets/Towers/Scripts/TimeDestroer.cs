using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TowerGame
{
    public class TimeDestroer : MonoBehaviour
    {
        public float Timer=1;
        float startTime;
        // Use this for initialization
        void Start()
        {
            startTime = Time.time;
        }

        // Update is called once per frame
        void Update()
        {
            if (Time.time - startTime > Timer)
            {
                Destroy(gameObject);
            }
        }
    }
}