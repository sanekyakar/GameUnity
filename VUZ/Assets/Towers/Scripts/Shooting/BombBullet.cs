using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerGame
{
    public class BombBullet : MonoBehaviour
    {
        public Transform target;
        public float speed = 1;
        public float speedUpOverTime = 0.1f;
        public float ballisticOffset = 2;
        public float hitDistance = 2;
        public float damage = 0.1f;
        public float exploseDistance = 1f;
        public int maxCount = 5;
        public GameObject explosePrefab;
        public bool freezeRotation;

        float counter;
        Vector2 originPoint;
        Vector2 myVirtualPosition;
        Vector2 myPreviousPosition;
        Vector2 aimPoint;

        // Use this for initialization
        void Start()
        {
            originPoint = myVirtualPosition = myPreviousPosition = transform.position;
            aimPoint = target.position;
        }

        // Update is called once per frame
        void Update()
        {
            counter += Time.deltaTime;
            speed += Time.deltaTime * speedUpOverTime;
            if (target != null)
            {
                aimPoint = target.position;
            }
            Vector2 originDistance = aimPoint - originPoint;
            Vector2 distanceToAim = aimPoint - (Vector2)myVirtualPosition;
            myVirtualPosition = Vector2.Lerp(originPoint, aimPoint, counter * speed / originDistance.magnitude);
            transform.position = AddBallisticOffset(originDistance.magnitude, distanceToAim.magnitude);
            //LookAtDirection2D((Vector2)transform.position - myPreviousPosition);
            LookAtDirection2D((Vector2)transform.position - myPreviousPosition);
            myPreviousPosition = transform.position;
            if (distanceToAim.magnitude <= hitDistance)
            {
                MakeDamage();       
                // Destroy bullet
                //Destroy(gameObject);
                //enabled = false;
            }

        }

        void MakeDamage()
        {
            var enemies = Physics.SphereCastAll(transform.position, exploseDistance, Vector3.up);
            int maxCount = this.maxCount;
            foreach (var e in enemies)
            {
                if (maxCount <= 0) break;
                var h = e.collider.GetComponent<EnemyHealth>();
                if (h != null)
                {
                    h.TakeDamage(damage);
                    maxCount--;
                }
            }
            Instantiate(explosePrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        private void LookAtDirection2D(Vector2 direction)
        {
            if (freezeRotation == false)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }

        private Vector2 AddBallisticOffset(float originDistance, float distanceToAim)
        {
            if (ballisticOffset > 0f)
            {
                // Calculate sinus offset
                float offset = Mathf.Sin(Mathf.PI * ((originDistance - distanceToAim) / originDistance));
                offset *= originDistance;
                // Add offset to trajectory
                return (Vector2)myVirtualPosition + (ballisticOffset * offset * Vector2.up);
            }
            else
            {
                return myVirtualPosition;
            }
        }
    }
}
