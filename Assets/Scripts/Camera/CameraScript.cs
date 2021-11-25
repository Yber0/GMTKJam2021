using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform P1, P2;
    Vector3 screensize;
    public Transform rightWall, leftWall;
    public float hMin, hMax;
    void Start()
    {
        screensize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        hMin += screensize.x;
        hMax -= screensize.x;
    }
    void FixedUpdate()
    {
        screensize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, -10));
        rightWall.position = new Vector2(screensize.x + 0.5f, 0);
        leftWall.position = new Vector2(screensize.x - 0.5f - Camera.main.orthographicSize * Camera.main.aspect * 2f, 0);
    }
    private void LateUpdate()
    {
        Vector3 pos = transform.position;
        float xPos = (P1.position.x + P2.position.x) / 2;
        xPos = Mathf.Clamp(xPos, hMin, hMax);
        transform.position = new Vector3(xPos, pos.y, pos.z);
    }
}
