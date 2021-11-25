using System;
using UnityEngine;

public class EndZone : MonoBehaviour
{
    public SpriteRenderer greenLight, pinkLight;
    public Color greenColor, pinkColor;
    private bool p1, p2;

    private void Awake()
    {
        p1 = false;
        p2 = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.name)
        {
            case "Player":
                greenLight.color = greenColor;
                p1 = true;
                break;
            case "Player2":
                pinkLight.color = pinkColor;
                p2 = true;
                break;
        }
        if (p1 && p2) SceneLoader.instance.LoadNextScene();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        switch (other.name)
        {
            case "Player":
                greenLight.color = Color.white;
                p1 = false;
                break;
            case "Player2":
                pinkLight.color = Color.white;
                p2 = false;
                break;
        }
    }
}
