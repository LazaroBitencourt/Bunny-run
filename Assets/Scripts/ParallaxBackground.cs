using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ParallaxBackground : MonoBehaviour
{
    public float speedMultiplier = 0.5f;
    public bool fitToCamera = true;
    public float yOffset = 0f;
    public int sortingOrder = -10;

    private Material material;
    private float tileWidth;
    private float offsetX;

    private void Awake()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.sortingOrder = sortingOrder;
        material = meshRenderer.material;
    }

    private void Start()
    {
        Texture tex = material.mainTexture;
        if (tex == null)
        {
            enabled = false;
            return;
        }

        tex.wrapMode = TextureWrapMode.Repeat;

        Camera cam = Camera.main;
        if (fitToCamera && cam != null && cam.orthographic)
        {
            float height = cam.orthographicSize * 2f;
            float width = height * cam.aspect;
            transform.localScale = new Vector3(width, height, 1f);
            transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y + yOffset, transform.position.z);
        }

        tileWidth = transform.localScale.y * ((float)tex.width / tex.height);
        material.mainTextureScale = new Vector2(transform.localScale.x / tileWidth, 1f);
    }

    private void Update()
    {
        if (GameManager.Instance == null) return;

        float speed = GameManager.Instance.gameSpeed * speedMultiplier;
        offsetX = Mathf.Repeat(offsetX + speed / tileWidth * Time.deltaTime, 1f);
        material.mainTextureOffset = new Vector2(offsetX, 0f);
    }
}