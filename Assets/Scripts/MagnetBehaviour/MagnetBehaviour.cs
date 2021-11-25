using UnityEngine;

public class MagnetBehaviour : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    private Color newColor = Color.white;
    [SerializeField] private Color NorthColor = Color.red;
    [SerializeField] private Color SouthColor = Color.blue;
    [SerializeField] private Color NeutralColor = Color.white;
    
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] protected Frequency _frequency;
    public Frequency frequency
    {
        get { return _frequency; }
        set
        {
            _frequency = value; 
            newColor = _frequency == Frequency.North ? NorthColor : _frequency == Frequency.South ? SouthColor : NeutralColor;
            ChangeSpriteColor();
            ChangeParticleColor();
        }
    }
    public float pullForce;

    private void OnValidate()
    {
        newColor = _frequency == Frequency.North ? NorthColor : _frequency == Frequency.South ? SouthColor : NeutralColor;
        ChangeSpriteColor();
    }

    private void Start()
    {
        newColor = _frequency == Frequency.North ? NorthColor : _frequency == Frequency.South ? SouthColor : NeutralColor;
        ChangeSpriteColor();
        ChangeParticleColor();
    }

    private void ChangeSpriteColor()
    {
        _spriteRenderer.color = newColor;
    }

    private void ChangeParticleColor()
    {
        ParticleSystem.MainModule ma = _particleSystem.main;
        ma.startColor = new Color(newColor.r, newColor.g, newColor.b, .25f);
    }
}