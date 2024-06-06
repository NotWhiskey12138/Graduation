using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CombatTestDummy : MonoBehaviour,IDamageable
{
    [SerializeField] private GameObject hitParticles;
    
    private Animator anim;
    
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Damage(float amount)
    {
        Debug.Log(amount + " Damage taken!!!");

        Instantiate(hitParticles, transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360f)));
        anim.SetTrigger("damage");
    }
}
