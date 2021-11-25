using UnityEngine;

public class MagnetBool : MonoBehaviour
{
    [SerializeField] private GameObject magnetSprite;
    [SerializeField] private MagnetBehaviour _magnetBehaviour;
    public bool _hasMagnet;
    private void Awake()
    {
        showMagnet(_hasMagnet, _magnetBehaviour.frequency);
        if (_hasMagnet) Destroy(this);
    }

    private void showMagnet(bool HasMagnet, Frequency frequency)
    {
        _magnetBehaviour.gameObject.SetActive(HasMagnet);
        magnetSprite.SetActive(HasMagnet);
        _magnetBehaviour.frequency = frequency;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out StaticMagnet magnet))
        {
            showMagnet(true, magnet.frequency);
            other.gameObject.SetActive(false);
            Destroy(this);
        }
    }
}
