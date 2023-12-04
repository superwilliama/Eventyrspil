using UnityEngine;

public class Table : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        int[] liquidLayers = { 6, 7, 8 };

        for (int i = 0; i < liquidLayers.Length; i++)
        {
            if (collision.gameObject.layer == liquidLayers[i])
            {
                Destroy(collision.gameObject, Random.Range(0.5f, 3.5f));
            }
        }
    }
}
