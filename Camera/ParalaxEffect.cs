using UnityEngine;

public class ParalaxEffect : MonoBehaviour {

    [SerializeField] Vector2 parallaxSpeed;
    [SerializeField] bool infiniteX;
    [SerializeField] bool infiniteY;

    private Transform cameraPosition;
    private Vector3 lastCameraPos;
    private float textureUnitSizeX;
    private float textureUnitSizeY;

    private void Start () {
        cameraPosition = Camera.main.transform;
        lastCameraPos = cameraPosition.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
        textureUnitSizeY = texture.height/ sprite.pixelsPerUnit;
    }

    private void LateUpdate () {
        Vector3 travelledDistance = cameraPosition.position - lastCameraPos;
        Vector3 distanceToTravel =  
            new(travelledDistance.x * parallaxSpeed.x, travelledDistance.y * parallaxSpeed.y);
        transform.position += distanceToTravel;
        lastCameraPos = cameraPosition.position;
        CalculateReposition();
    }

    private void CalculateReposition() {
        if (infiniteX &&
            Mathf.Abs(cameraPosition.position.x - transform.position.x) >= textureUnitSizeX) {
            float offsetX = (cameraPosition.position.x - transform.position.x) % textureUnitSizeX;
            transform.position = new Vector3(cameraPosition.position.x + offsetX, transform.position.y);
        }

        if (infiniteY &&
            Mathf.Abs(cameraPosition.position.y - transform.position.y) >= textureUnitSizeY) {
            float offsetY = (cameraPosition.position.y - transform.position.y) % textureUnitSizeY;
            transform.position = new Vector3(transform.position.x, cameraPosition.position.y + + offsetY);
        }
    }
}
