using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TowerGame
{
    public class WaypointsPath : MonoBehaviour
    {
        public static WaypointsPath instance;
        static Vector3[][] Paths;

        public static Vector3[] GetPath(int line)
        {
            return Paths[line];
        }


        void Awake()
        {
            instance = this;
            int lines = transform.GetChild(0).childCount;
            print(lines);
            Paths = new Vector3[lines][];
            for(int i = 0; i < lines; i++)
            {
                Paths[i] = new Vector3[transform.childCount];
            }

            int currPath = 0;
            foreach (Transform t in transform)
            {
                if (t.childCount >= lines)
                {
                    for (int i = 0; i < lines; i++)
                    {
                        Paths[i][currPath] = t.GetChild(i).position;
                    }
                }
                currPath++;
            }
        }

        void Update()
        {

        }

        public static Vector3 GetRandomStartPos()
        {
            int line = Random.Range(0, Paths.Length);
            return Paths[line][0];
        }


        void OnDrawGizmos()
        {
            Vector3 v1 = Vector3.zero,
                v2 = Vector3.zero, 
                v3 = Vector3.zero;
            foreach(Transform t in transform)
            {
                if(t.childCount >= 3)
                {
                    Transform t1 = t.GetChild(0);
                    Transform t2 = t.GetChild(1);
                    Transform t3 = t.GetChild(2);
                    if(v1 != Vector3.zero)
                    {
                        Gizmos.color = Color.blue;
                        Gizmos.DrawLine(v1, t1.position);
                        Gizmos.DrawLine(v2, t2.position);
                        Gizmos.DrawLine(v3, t3.position);
                        Gizmos.color = Color.white;
                    }

                    v1 = t1.position;
                    v2 = t2.position;
                    v3 = t3.position;
                }
            }
        }
    }
}
