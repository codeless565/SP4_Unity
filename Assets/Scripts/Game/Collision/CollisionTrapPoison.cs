using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTrapPoison : MonoBehaviour
{
    private bool isDestroying;
    private float elapseTimer;
    public float DestroyDelayTime = 3;

    void Start()
    {
        isDestroying = false;
        elapseTimer = DestroyDelayTime;
    }

    void Update()
    {
        if (isDestroying)
        {
            elapseTimer -= Time.deltaTime;
            if (elapseTimer <= 0)
                Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isDestroying)
            return;

        if (other.GetComponent<Player2D_StatsHolder>() == null) 
            return;

        PoisonTrapEffect tempTrap = other.gameObject.AddComponent<PoisonTrapEffect>();
        Debug.Log(tempTrap);
        tempTrap.SetDuration(10);

        isDestroying = true;
    }
}
