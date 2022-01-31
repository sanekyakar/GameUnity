using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerGame
{
    public class Enemy : MonoBehaviour, System.IComparable<Enemy>
    {

        public int MoneyBonus = 100;
        Animator anim;
        EnemyWalker walker;

        // Use this for initialization
        void Start()
        {
            GameManager.Enemies.Add(this);
            anim = GetComponent<Animator>();
            walker = GetComponent<EnemyWalker>();
        }

        public void SetAttackMode()
        {
            if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                anim.Play("Attack");
        }

        public void OnDestroy()
        {
            GameManager.Enemies.Remove(this);
            GameManager.instance.AddBonusMoney(MoneyBonus, transform.position);
        }

        public int CompareTo(Enemy obj)
        {
            if (obj != null)
            {
                return walker.CompareTo(obj.walker);
            }
            return 1;
        }
    }
}