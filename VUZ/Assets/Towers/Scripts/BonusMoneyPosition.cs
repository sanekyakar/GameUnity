using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusMoneyPosition : MonoBehaviour {

    public float Speed = 50;
    Vector3 target;

    public void SetTargetPos(Vector3 toPos)
    {
        target = toPos;
    }
		
	void Update () {
        var dir = target - transform.position;
        transform.position += dir.normalized * Speed * Time.deltaTime;
        if (dir.magnitude < 10 || (dir.x < 0 && dir.y < 0)) Destroy(gameObject);
	}
}
