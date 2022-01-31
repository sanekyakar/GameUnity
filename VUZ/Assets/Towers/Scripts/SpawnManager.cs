using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerGame
{

    public class SpawnManager : MonoBehaviour {

        [System.Serializable]
        public class Wave
        {
            public float Delay = 5;
            public int Count = 5;
            public int MaxEnemyLevel = 10;
        }

        public Wave[] Waves; 
        public Enemy[] enemies;
        public Vector2 RandomPosAmpl = new Vector2(0.5f,0.5f);

        int currentWave;

        void Start()
        {
            StartCoroutine(SpawnWaves());
        }

        IEnumerator SpawnWaves()
        {
            for (int _w =0;_w<Waves.Length;_w++)
            {
                Wave w = Waves[_w];
                yield return new WaitForSeconds(w.Delay);
                for(int i = 0; i < w.Count; i++)
                {
                    int e = Random.Range(0, Mathf.Min(enemies.Length, w.MaxEnemyLevel));
                    float x = Random.Range(-RandomPosAmpl.x, RandomPosAmpl.x);
                    float y = Random.Range(-RandomPosAmpl.y, RandomPosAmpl.y);
                    Instantiate(enemies[e], WaypointsPath.GetRandomStartPos() + new Vector3(x,y), Quaternion.identity);
                }
            }

            while(GameManager.Enemies.Count > 0)
            {
                yield return new WaitForSeconds(1);
            }

            GameManager.instance.GameWon();
        }

    }

}