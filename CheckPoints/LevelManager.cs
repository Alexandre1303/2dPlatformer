using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    private AudioSource finishSound;
    private void Start() {
        finishSound = GetComponent<AudioSource>();  
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name == "Player") { 
            finishSound.Play();
            Invoke(nameof(NextLevel), 2f);
        }
    }

    public void NextLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void PlayAgain() {
        SceneManager.LoadScene(1);
    }
   
}
