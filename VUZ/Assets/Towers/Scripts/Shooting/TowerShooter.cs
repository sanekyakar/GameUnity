using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerGame
{
    public class TowerShooter : MonoBehaviour
    {

        public float ShootDelay = 0.5f;
        public float ShootDelayRnd = 0.05f;
        public Transform ShootPos;
        public GameObject Bullet;

        List<Enemy> enemies = new List<Enemy>();
        Enemy currentEnemy;
        Transform target
        {
            get
            {
                if (currentEnemy == null) return null;
                return currentEnemy.transform;
            }
        }

        public void AddEnemy(Enemy e)
        {
            if (e && !enemies.Contains(e)) enemies.Add(e);
        }

        public void RemoveEnemy(Enemy e)
        {
            if (e && enemies.Contains(e))
            {
                enemies.Remove(e);
                if (currentEnemy == e) currentEnemy = null;
            }
        }

        float oldShoot;

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if (Time.time - oldShoot > ShootDelay)
            {
                if (currentEnemy)
                {
                    var b = Instantiate(Bullet, ShootPos.position - Vector3.forward * 0.2f, Quaternion.identity);
                    b.GetComponent<BombBullet>().target = target;
                    oldShoot = Time.time - Random.Range(0, ShootDelayRnd);
                    if (transform.position.x - target.position.x > 0) transform.localScale = Vector3.one;
                    else transform.localScale = new Vector3(-1, 1, 1);
                }
                else if(enemies.Count > 0)
                {
                    enemies.Sort();
                    while (currentEnemy == null && enemies.Count > 0)
                    {
                        currentEnemy = enemies[0];
                        if (currentEnemy == null) enemies.RemoveAt(0);
                    }
                }
            }
        }
    }
}