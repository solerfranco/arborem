using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    private Animator anim;
    public Animator growAnim;
    private float timeCounter;
    private AudioSource audioSource;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (GameController.Instance.gameOver) return;
        timeCounter += Time.deltaTime;
        if(timeCounter > 6)
        {
            timeCounter = 0;
            anim.SetTrigger("Grow");
            growAnim.SetTrigger("Grow");
            audioSource.Play();
        }
        print(Time.timeSinceLevelLoad);
        if(Time.timeSinceLevelLoad > 60)
        {
            GameController.Instance.Won();
        }
    }
}
