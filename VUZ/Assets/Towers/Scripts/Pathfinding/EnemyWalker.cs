using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerGame
{
    public class EnemyWalker : MonoBehaviour, System.IComparable<EnemyWalker>
    {

        public float Speed = 10;
        public float RandomAmplitude = 0.3f;

        public float DistanceToEnd
        {
            get
            {
                return endDistance;
            }
        }

        float endDistance;
        bool walking = true;
        bool addedToList;
        int current;
        float currRnd;
        Vector3[] path;
        Vector3 startScale;

        Vector3 currentPath
        {
            get
            {
                if (path == null || path.Length == 0 || path.Length <= current) return Vector3.zero;
                return path[current];
            }
        }
        float currentDistance
        {
            get
            {
                return Vector3.Distance(transform.position, currentPath);
            }
        }

        void Start()
        {
            path = WaypointsPath.GetPath(Random.Range(0, 3));
            currRnd = Random.Range(0, RandomAmplitude);
            CalcEndDist();
            startScale = transform.localScale;
        }

        float oldEndDistTime;
        void Update()
        {
            if(walking && path != null && path.Length > 0)
            {
                if(currentDistance < 0.2f)
                {
                    currRnd = Random.Range(-RandomAmplitude, RandomAmplitude);
                    current++;
                    if (current == path.Length)
                    {
                        walking = false;
                    }
                }
                else
                {
                    var dir = currentPath - transform.position;
                    transform.position += dir.normalized * (Speed + currRnd) * Time.deltaTime;
                    var scale = startScale;
                    if (dir.x < 0) {
                        scale.x = -startScale.x;
                    }
                    transform.localScale = scale;
                }
                if (Time.time - oldEndDistTime > 0.1f)
                {
                    CalcEndDist();
                    oldEndDistTime = Time.time;
                }
            }
            else if (!walking && !addedToList)
            {
                endDistance = 0;
                addedToList = true;
                GameManager.instance.AddCastleEnemy(GetComponent<Enemy>());
            }
        }

        void CalcEndDist()
        {
            float dst = currentDistance;
            Vector3 oldpos = currentPath;
            for(int i = current+1;i<path.Length;i++) {
                dst += Vector3.Distance(oldpos, path[i]);
                oldpos = path[i];
            }
            endDistance = dst;
        }

        public int CompareTo(EnemyWalker obj)
        {
            if (obj != null)
            {
                return DistanceToEnd.CompareTo(obj.DistanceToEnd);
            }
            return 1;
        }
    }

}