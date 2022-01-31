using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerGame
{
    public class EnemyHealth : MonoBehaviour
    {

        public float HP = 1;
        public Transform HpPivot;
        public AudioClip DeathSound;

        float startHP;
        Vector3 startPivotScale;

        void Start()
        {
            startHP = HP;
            startPivotScale = HpPivot.localScale;
            UpdateBar();
        }

        public void TakeDamage(float d)
        {
            HP -= d;
            UpdateBar();
            if(HP <= 0)
            {
                SpawnAudioSrc();
                Destroy(gameObject);
            }
        }

        void SpawnAudioSrc()
        {
            if(DeathSound != null)
            {
                GameObject obj = new GameObject("Sound");
                obj.AddComponent<TimeDestroer>().Timer = DeathSound.length;
                var src = obj.AddComponent<AudioSource>();
                src.clip = DeathSound;
                src.Play();
            }
        }

        void UpdateBar()
        {
            HpPivot.localScale = new Vector3(startPivotScale.x * (HP / startHP) + 0.1f, startPivotScale.y, startPivotScale.z);
        }
        
    }
}
