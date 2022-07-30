using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMotion : MonoBehaviour
{
    private Animator animator;
    private AudioSource sound;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        animator.SetBool("doorIsOpen", true);
        sound.PlayDelayed(0.7f);
    }

    private void OnTriggerExit(Collider other)
    {
        animator.SetBool("doorIsOpen", false);
        sound.PlayDelayed(0.5f);

    }

    
}
