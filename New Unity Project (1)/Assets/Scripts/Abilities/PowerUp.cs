using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    //public Rigidbody m_Shell;

    //public Transform m_FireTransform;

    //public float m_LaunchForce = 30f;


  //  private void Fire()
    //{
    //    Rigidbody shellInstance = Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;

     //   shellInstance.velocity = m_LaunchForce * m_FireTransform.forward;
   // }

   // private void Update()
  //  {
   //     if (Input.GetButtonUp("Fire1"))
   //     {
    //        Fire();
   //     }

  //  }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gameObject.SetActive(false);

        }
    }

    // The time in seconds before the shell is removed 
    public float m_MaxLifeTime = 2f;
    // The amount of damage done if the explosion is centred on a tank 
    public float m_MaxDamage = 34f;
    // The maximum distance away from the explosion tanks can be and are still affected 
    public float m_ExplosionRadius = 5;
    // The amount of force added to a tank at the centre of the explosion 
    public float m_ExplosionForce = 100f;

    // Reference to the particls that will play on explosion 
    public ParticleSystem m_ExplosionParticles;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, m_MaxLifeTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        // find the rigidbody of the collision object 
        Rigidbody targetRigidbody = other.gameObject.GetComponent<Rigidbody>();


        if (targetRigidbody != null)
        {

            targetRigidbody.AddExplosionForce(m_ExplosionForce, transform.position, m_ExplosionRadius);

            Tankhealth targetHealth = targetRigidbody.GetComponent<Tankhealth>();

            if (targetHealth != null)
            {
                float damage = CalculateDamage(targetRigidbody.position);

                targetHealth.TakeDamage(damage);
            }
        }

        m_ExplosionParticles.transform.parent = null;

        // Play the particle system 
        m_ExplosionParticles.Play();

        // Once the particles have finished, destroy the gameObject they are on 
        Destroy(m_ExplosionParticles.gameObject, m_ExplosionParticles.main.duration);

        // Destroy the shell 
        Destroy(gameObject);
    }

    private float CalculateDamage(Vector3 targetPosition)
    {
        Vector3 explosionToTarget = targetPosition - transform.position;

        float explosionDistance = explosionToTarget.magnitude;

        float relativeDistance = (m_ExplosionRadius - explosionDistance) / m_ExplosionRadius;

        float damage = relativeDistance * m_MaxDamage;

        damage = Mathf.Max(0f, damage);

        return damage;
    }


}
