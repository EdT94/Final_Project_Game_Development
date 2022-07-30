using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Archer : MonoBehaviour
{
    private int HP = 100;
    public Animator animator;
    public Slider healthBar;
    public AudioSource[] sounds;
    public AudioSource gotHitSound;
    public AudioSource dyingSound;
    public AudioSource swordSound;
    public GameObject arrowObject;
    public Transform arrowPoint;
    Collider[] childColliders;

    void Start()
    {
        sounds = GetComponents<AudioSource>();
        dyingSound = sounds[1];
        gotHitSound = sounds[0];
        swordSound = sounds[2];
    }

    void Update()
    {
        healthBar.value = HP;
    }

    public void takeDamage(int damage)
    {
        animator.SetTrigger("damage");
        HP -= damage;
        if (HP <= 0)
        {
            //Destroy(transform.GetComponent<Rigidbody>());
            gameObject.tag = "Untagged";
            animator.SetBool("isAlive", false);
            FindObjectOfType<KnightPlayer>().increaseNumOfDeadsArchers();
            //GetComponent<Collider>().enabled = false;
            childColliders = this.GetComponentsInChildren<Collider>();
            foreach (Collider collider in childColliders)
            {
                collider.enabled = false;
                Destroy(collider);
            }
            
            dyingSound.PlayDelayed(0.1f);
            
        }
        else
        {
            animator.SetBool("isAlive", true);

            gotHitSound.PlayDelayed(0.1f);

        }
    }

    public void Shoot()
    {
        Rigidbody rigidBody = Instantiate(arrowObject, arrowPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
        rigidBody.AddForce(transform.forward * 40f, ForceMode.Impulse);
    }

    public int getHP()
    {
        return HP;
    }
   
    public void playSwordDound()
    {
        swordSound.PlayDelayed(0.01f);

    }
}
