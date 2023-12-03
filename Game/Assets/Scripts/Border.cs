using UnityEngine;

public class Border : MonoBehaviour
{
    [SerializeField] private SpringJoint2D _joint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int[] liquidLayers = { 6, 7, 8 };

        for (int i = 0; i < liquidLayers.Length; i++)
        {
            if (collision.gameObject.layer == liquidLayers[i])
            {
                Destroy(collision.gameObject);
            }
        }

        if (collision.name == "Glass")
        {
            collision.transform.position = Vector2.zero;
            collision.transform.rotation = Quaternion.identity;
            collision.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            collision.GetComponent<Rigidbody2D>().angularVelocity = 0;
            _joint.enabled = false;
        }
    }
}
