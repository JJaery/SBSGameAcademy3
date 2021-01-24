using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Animator animator;
    public float HP = 100;
    public float MaxHP = 100;
    public float moveSpeed = 10;
    private float Damage = 5;

    protected void Hit(Character target)
    {
        Debug.Log($"{gameObject.name} Attack! -> {target.gameObject.name}");
        target.Hitted(this);
    }

    protected void Hitted(Character hitter)
    {
        Debug.Log($"{gameObject.name} Hitted by {hitter.gameObject.name}");
        float curHp = HP;

        //계산
        this.HP -= hitter.Damage;
        //

        float realDmg = curHp - this.HP;

        animator.SetTrigger("onHitted");
        UIManager.Instance.MakeDamageUI(this.transform, realDmg);
    }
}
