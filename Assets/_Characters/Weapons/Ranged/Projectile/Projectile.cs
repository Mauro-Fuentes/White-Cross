using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Characters
{
    public class Projectile : MonoBehaviour
    {

        [SerializeField] float projectileSpeed;

        [SerializeField] GameObject shooter; // quién disparo?
        
        const float DESTROY_DELAY = 0.01f;

        float damageCaused;

        public void SetShooter(GameObject shooter)
        {
            this.shooter = shooter;
        }

        public void SetDamage(float damage)
        {
            damageCaused = damage;

        }
        
        public float GetDefaultLaunchSpeed()
        {
            return projectileSpeed;
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer != gameObject.layer)
            {
                //DamageIfDamageable(collision);
            }

        }

        // TODO:

        // private void DamageIfDamageable(Collision collision)
        // {
        //     /// DEALDAMAGE IF ITS DAMAGEABLE
        //     //Debug.Log ("Projectyle hit " + other.name);
        //     Component damageableComponent = collision.gameObject.GetComponent(typeof(IDamageable));

        //     if (damageableComponent)
        //     {
        //         // Debug.Log ("I hit " + damageableComponent );
        //         // CAST Como Player hereda de dos Espacios diferentes le tenemos que decir de cual: (damageableComponent as IDamageable)
        //         (damageableComponent as IDamageable).TakeDamage(damageCaused);
        //     }
        //     Destroy(gameObject, DESTROY_DELAY);
        // }

        // void OnTriggerEnter(Collider other)
        // {
        // 	//Debug.Log ("Projectyle hit " + other.name);
        // 	Component damageableComponent = other.GetComponent<Collider>().GetComponent (typeof (IDamageable));

        // 	if (damageableComponent)
        // 	{
        // 		// Debug.Log ("I hit " + damageableComponent );
        // 		// CAST Como Player hereda de dos Espacios diferentes le tenemos que decir de cual: (damageableComponent as IDamageable)
        // 		( damageableComponent as IDamageable ).TakeDamage(damageCaused);
        // 	}
        // }
    }
}