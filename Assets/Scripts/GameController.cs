using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    public GameObject rootPrefab;
    public GameObject[] trashPrefabs;


    public GameObject gameOverScreen;
    public GameObject winScreen;

    public bool gameOver;

    private int rottenRoots;
    public int RottenRoots
    {
        get {
            return rottenRoots;
        }
        set {
            rottenRoots = value;
            CheckRoots();
        }
    }

    public void Won()
    {
        gameOver = true;
        CancelInvoke();
        winScreen.SetActive(true);
        //Root[] roots = FindObjectsOfType<Root>();
        //foreach (Root root in roots)
        //{
        //    root.Stop();
        //}
    }


    private void Awake()
    {
        //Singleton setup
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
        InvokeRepeating(nameof(SpawnThrash), 10f, Random.Range(1f, 3f));
    }

    private void SpawnThrash()
    {
        Instantiate(trashPrefabs[Random.Range(0, trashPrefabs.Length)], FindObjectOfType<MoveErratically>().transform.position + Vector3.down * 2 + Vector3.left, Quaternion.identity);
    }

    private void CheckRoots()
    {
        if(rottenRoots > 5)
        {
            Time.timeScale = 0;
            gameOverScreen.SetActive(true);
            gameOver = true;
        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}