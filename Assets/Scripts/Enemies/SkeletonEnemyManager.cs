using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles Behaviour of Skeleton Enemy
public class SkeletonEnemyManager : MonoBehaviour
{
    enum EnemySkeletonState
    {
        IDLE,
        WALK,
        ATTACK,
        DIE,
    }

    EnemySkeletonState skeletonState;
    public Animation anim;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void AnimationUpdate()
    {
        switch(skeletonState)
        {
            case EnemySkeletonState.IDLE:
                anim.Play("SkeletonIdle");
                break;

            case EnemySkeletonState.WALK:
                anim.Play("SkeletonWalk");
                break;

            case EnemySkeletonState.ATTACK:
                anim.Play("SkeletonAttack");
                break;
        }
    }
}
