using UnityEngine;

public class PressAction : MonoBehaviour
{
    private SpriteRenderer spr;
    private int count=0;

    [SerializeField] private Sprite idle_button; 
    [SerializeField] private Sprite press_button;

    [SerializeField] private ExecuteAction[] _executeActions;
    
    private void Start()
    {
        spr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("ActionObject"))
        {
            count++;
            spr.sprite = press_button;

            foreach (ExecuteAction _object in _executeActions)
            {
                _object.Execute();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("ActionObject"))
        {
            count--;

            if (count == 0)
            {
                spr.sprite = idle_button;
                
                foreach (ExecuteAction _object in _executeActions)
                {
                    _object.Execute();
                }
            }
        }
    }
}
