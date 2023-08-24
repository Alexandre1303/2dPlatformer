using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {
    
    
    private Animator animator;
    private Rigidbody2D player;
    private bool isColliding = false;
    [SerializeField] private AudioSource playerDeathSound;
    [SerializeField] private AudioSource playerHurt;
    [SerializeField] private float startingPlayerHP;
    [SerializeField] private float iDurantion;
    [SerializeField] private float numFlashes;
    private readonly object lck = new object();

    public float CurrentHP { get; private set; }
   
    private SpriteRenderer spriteRenderer;
    private void Awake() {
        animator = GetComponent<Animator>();
        player = GetComponent<Rigidbody2D>();
        CurrentHP = startingPlayerHP;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

   
    private void Die() {
        player.bodyType = RigidbodyType2D.Static;
        playerDeathSound.Play();
        animator.SetTrigger("death");
        Invoke(nameof(RestartLevel), 1.5f);
    }

    public void TakeDamage(float _damage) {
        CurrentHP = Mathf.Clamp(CurrentHP - _damage, 0, startingPlayerHP);
        
        if (CurrentHP > 0)
        {
            StartCoroutine(IFrames());
            animator.SetTrigger("damage");
            playerHurt.Play();
        }
        else { Die(); }
    }
            
     
    private IEnumerator IFrames() {

        int playerLayer = 6;
        int trapLayer = 7;
        Physics2D.IgnoreLayerCollision(playerLayer, trapLayer, true);
        for(int i = 0; i < numFlashes; i++) {
            // opaque white
            spriteRenderer.color = new Color(1, 0, 0, 0.5f);

            //to divide the number of flashes between the 2 seconds 
            yield return new WaitForSeconds(iDurantion / (numFlashes * 2));
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(iDurantion / (numFlashes * 2));

        }
        Physics2D.IgnoreLayerCollision(playerLayer, trapLayer, false);
    }

    private void RestartLevel() {
        Debug.Log("HEREEE");
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

}
