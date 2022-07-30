using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Arrow : MonoBehaviour
{
    private AudioSource sound;
    public int damageAmount = 10;


    public void Start()
    {
        sound = GetComponent<AudioSource>();
        sound.PlayDelayed(0.1f);
        //Destroy(gameObject, 5);
    }


    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Knight")
        {
            
           
            
            if (SceneManager.GetActiveScene().name == "DefendScene")
            {
                Destroy(gameObject);
                Destroy(transform.GetComponent<BoxCollider>());
                if (other.GetComponent<Knight>())
                    other.GetComponent<Knight>().takeDamage(damageAmount);
               
            }
            else
            {
                Destroy(transform.GetComponent<BoxCollider>());
                if (other.GetComponent<Knight>())
                    other.GetComponent<Knight>().takeDamage(damageAmount);
                else if (other.GetComponent<KnightPlayer>())
                    StartCoroutine(FindObjectOfType<KnightPlayer>().takeDamage(damageAmount));
            }
            

            //else if (other.GetComponent<KnightPlayer>())
            //     StartCoroutine(FindObjectOfType<KnightPlayer>().takeDamage(damageAmount));
            //other.GetComponent<Knight>().takeDamage(damageAmount);
            //transform.parent = other.transform;

        }
    }


    
}
