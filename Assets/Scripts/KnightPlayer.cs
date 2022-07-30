using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class KnightPlayer : MonoBehaviour
{
    private static int HP = 100;
    public static bool isGameOver;
    public static bool isVictory;
    public TextMeshProUGUI playerHPText;
    public TextMeshProUGUI playerMessage;
    public TextMeshProUGUI playerWinMessage;
    public TextMeshProUGUI playerGameOverMessage;
    public TextMeshProUGUI mortarMessage;
    public Animator animator;
    public AudioSource[] sounds;
    public AudioSource gotHitSound;
    public AudioSource dyingSound;
    public AudioSource gameOverSound;
    public AudioSource victorySound;
    public GameObject bloodOverlay;
    Collider[] childColliders;
    private float gameOverTimer;
    private float messageTimer;
    private float victoryTimer;
    public int numOfDeadArchers;
    private int numOfArchers;
    public GameObject[] allArchers;
    private bool victorySoundPlayed;


    // Start is called before the first frame update
    void Start()
    {
        numOfDeadArchers = 0;
        findNumOfArchers();
        gameOverTimer = 0;
        messageTimer = 0;
        victoryTimer = 0;
        isGameOver = false;
        isVictory = false;
        victorySoundPlayed = false;
        sounds = GetComponents<AudioSource>();
        dyingSound = sounds[1];
        gotHitSound = sounds[0];
        gameOverSound = sounds[2];
        victorySound = sounds[3];
    }



    // Update is called once per frame
    void Update()
    {

        if (numOfDeadArchers == numOfArchers)
            isVictory = true;

        messageTimer += Time.deltaTime;
        if (messageTimer > 2.5)
            playerMessage.enabled = false;
        if (messageTimer > 5)
            mortarMessage.enabled = false;



        playerHPText.text = "" + HP;

        if (isGameOver)
        {
            gameOverTimer += Time.deltaTime;
            if (gameOverTimer > 1.5)
            {
                playerGameOverMessage.enabled = true;
                if (gameOverTimer > 3.5)
                    SceneManager.LoadScene("GameOver");
            }

        }

        if (isVictory)
        {
            victoryTimer += Time.deltaTime;
            if (victoryTimer > 1.3)
            {
                if (!victorySoundPlayed)
                {
                    victorySoundPlayed = true;
                    victorySound.PlayDelayed(0.001f);
                    playerWinMessage.enabled = true;
                }
                if (victoryTimer > 3.5)
                    SceneManager.LoadScene("Victory");

            }

        }


    }

    public IEnumerator takeDamage(int damage)
    {
        animator.SetTrigger("damage");
        HP -= damage;
        bloodOverlay.SetActive(true);
        if (HP <= 0)
        {
            animator.SetBool("isAlive", false);
            isGameOver = true;
            //animator.SetTrigger("die");

            GetComponent<Collider>().enabled = false;
            childColliders = this.GetComponentsInChildren<Collider>();
            foreach (Collider collider in childColliders)
            {
                collider.enabled = false;
                Destroy(collider);
            }
            gameObject.tag = "Untagged";
            dyingSound.PlayDelayed(0.1f);
            gameOverSound.PlayDelayed(0.1f);

        }

        else
        {
            gotHitSound.PlayDelayed(0.1f);
            
            animator.SetBool("isAlive", true);

        }

        yield return new WaitForSeconds(0.6f);
        bloodOverlay.SetActive(false);
    }

    public int getHP()
    {
        return HP;
    }


    private void findNumOfArchers()
    {
        allArchers = GameObject.FindGameObjectsWithTag("Archer");
        foreach (GameObject archer in allArchers)
            numOfArchers++;
    }

    public void increaseNumOfDeadsArchers()
    {
        numOfDeadArchers++;
    }

    

    
}
