using UnityEngine;

public class BackgroundLooper : MonoBehaviour
{
    [Range(-1f,1f)]
    public float speedX = 0.5f;
    [Range(-1f, 1f)]
    public float speedY = 0f;
    public float endX = 10000000f;
    public float endY = 10000000f;
    private float offsetX = 0f;
    private float offsetY = 0f;
    Material material;
    bool stop = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (stop) return;
        Debug.Log("loop X " + offsetX + " Y "+ offsetY);
        offsetX += Time.deltaTime * speedX / 10f;
        offsetY += Time.deltaTime * speedY / 10f;
        material.SetTextureOffset("_MainTex", new Vector2(offsetX, offsetY));
        if (offsetX >= endX ||offsetY >= endY)
        {
            Stop();
        }
    }

    public void Stop()
    {
        stop = true;
    }
}
