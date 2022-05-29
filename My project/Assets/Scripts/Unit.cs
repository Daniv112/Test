using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [SerializeField] protected int hp;
    [SerializeField] protected int damage;
    [SerializeField] private GameObject iconSelect;
    [SpineEvent] public string footstepEventName = "footstep";
    protected SkeletonAnimation animation;
    protected Vector3 position;


    public virtual void Start()
    {
        animation = GetComponent<SkeletonAnimation>();
        animation.AnimationState.Complete += delegate{ Idle(); };
        position = transform.position;
    }

    public void Attack(Unit unit)
    {
        animation.AnimationName = "Miner_1";
        unit.TakeDamage(damage);
    }

    public void TakeDamage(int damage)
    {
        animation.AnimationName = "Damage";

        if (hp - damage > 0)
            hp -= damage;
        else
        {
            hp = 0;
            Dead();
        }

    }

    void Idle() 
    {
        if (animation.AnimationName != "Idle") 
        {
            animation.AnimationName = "Idle";
        }  
    }

    public void Deactivate() 
    {
        transform.position = position;
        iconSelect.SetActive(false);
    }

    public void Select()
    {
        iconSelect.SetActive(true);
    }

    private void Dead()
    {

    }

}
