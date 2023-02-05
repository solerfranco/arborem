using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    public int maxHealth = 1;
    private int currentHealth;
    public AudioClip[] sounds;
    public AudioClip[] soundsDeath;
    public AudioSource audioSource;

    private void Awake()
    {
        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();
    }

    public void OnMouseDown()
    {
        if (CheckHealth())
        {
            audioSource.PlayOneShot(sounds[Random.Range(0, sounds.Length)]);
            currentHealth--;
            LeanTween.moveLocalX(gameObject, transform.position.x + 0.1f, 0.10f).setEaseInOutCubic().setLoopPingPong(2).setOnComplete(() => 
            {
                if (!CheckHealth())
                {
                    audioSource.PlayOneShot(soundsDeath[Random.Range(0, soundsDeath.Length)]);
                    Invoke(nameof(DestroyTrash), 0.25f);
                }
            });
        }
    }

    private void DestroyTrash()
    {
        Destroy(gameObject);
    }

    private bool CheckHealth()
    {
        return currentHealth > 0;
    }
}
