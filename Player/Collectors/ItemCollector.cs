using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour {

    private int fruitCounter = 0;
    [SerializeField] private Text fruitCounterText;
    [SerializeField] private AudioSource collectSoundEffect;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Collectable")) {
            collectSoundEffect.Play();
            GameObject fruit = collision.gameObject;
            Destroy(fruit);
            fruitCounter++;
            fruitCounterText.text = "Fruits: " + fruitCounter;
        }
    }
}
