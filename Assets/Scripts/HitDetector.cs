using UnityEngine;

public class HitDetector : MonoBehaviour
{
    public System.Action<Transform, Vector3> onHitted;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 direction = transform.position - collision.transform.position;
        onHitted?.Invoke(collision.transform, direction);
    }
}
