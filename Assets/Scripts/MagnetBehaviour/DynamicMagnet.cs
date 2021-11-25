using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class DynamicMagnet : MagnetBehaviour
{
    private Rigidbody2D _rb;
    private CircleCollider2D _cCol;
    private void OnValidate()
    {
        GetComponent<CircleCollider2D>().isTrigger = true;
    }
    private void Awake()
    {
        _rb = GetComponentInParent<Rigidbody2D>();
        _cCol = GetComponent<CircleCollider2D>();
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        GetPushed(other.gameObject, other.GetComponent<MagnetBehaviour>());
    }

    private void GetPushed(GameObject other, MagnetBehaviour otherMagnet)
    {
        Vector2 pos = transform.position;
        Vector2 otherPos = other.transform.position;
        Vector2 dir = (otherPos - pos).normalized * (otherMagnet.frequency == frequency ? -1 : 1);

        float force = _cCol.radius/Vector2.Distance(pos, otherPos) * otherMagnet.pullForce;
        if (_rb.velocity.x > -10 && _rb.velocity.x < 10 && _rb.velocity.y > -10 && _rb.velocity.y < 10)
            _rb.AddForce(dir * force);
    }
}