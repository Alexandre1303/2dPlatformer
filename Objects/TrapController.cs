using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    [SerializeField] private float damage;


    private void OnTriggerEnter2D(Collider2D collision) {
       
        collision.GetComponent<PlayerHealth>().TakeDamage(damage);
    }

}
