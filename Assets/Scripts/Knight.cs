using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Knight : MonoBehaviour
{
    private int HP = 100;
    private int swordDamage = 20;
    public Animator animator;
    public Slider healthBar;
    public AudioSource[] sounds;
    public AudioSource gotHitSound;
    public AudioSource dyingSound;
    public AudioSource swordSound;
    Transform archer;
    Collider[] childColliders;

    void Start()
    {  
        sounds = GetComponents<AudioSource>();
        dyingSound = sounds[0];
        gotHitSound = sounds[1];
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
        if(HP <= 0)
        {
            //Destroy(transform.GetComponent<Rigidbody>());
            animator.SetBool("isAlive", false);
            FindObjectOfType<ArcherPlayer>().increaseNumOfDeadsKnights();
            GetComponent<Collider>().enabled = false;
            childColliders = this.GetComponentsInChildren<Collider>();
             foreach (Collider collider in childColliders)
             {
                 collider.enabled = false;
                 Destroy(collider);
             }
            gameObject.tag = "Untagged";
            dyingSound.PlayDelayed(0.1f);
            
        }
        else
        {
            animator.SetBool("isAlive", true);

            gotHitSound.PlayDelayed(0.1f);

        }
    }



    public void attack()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Archer");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - animator.transform.position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        if (closest)
        {
            archer = closest.transform;
            if (distance < 7)
            {
                swordSound.PlayDelayed(0.1f);
                animator.transform.LookAt(archer);
                if (archer.GetComponent<Archer>())
                        archer.GetComponent<Archer>().takeDamage(swordDamage);
                else if (archer.GetComponent<ArcherPlayer>())
                    StartCoroutine(FindObjectOfType<ArcherPlayer>().takeDamage(swordDamage)); 
                distance = Vector3.Distance(archer.position, animator.transform.position);
                
            }
            else
                animator.SetBool("isAttacking", false);

        }
        else
            animator.SetBool("isAttacking", false);


    }
    

    public int getHP()
    {
        return HP;
    }


    


}
