using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyPlatform : MonoBehaviour {
    

    private void OnTriggerEnter2D(Collider2D collision) {
        ApplyPos(collision, transform);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        ApplyPos(collision, null);
    }

    #nullable enable
    private void ApplyPos(Collider2D collision, Transform? transform) { 
        if (collision.gameObject.name == "Player") {
            collision.gameObject.transform.SetParent(transform);
        }
    }

}
