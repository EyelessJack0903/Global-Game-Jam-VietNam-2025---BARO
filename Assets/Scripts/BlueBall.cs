using UnityEngine;

public class BlueBall : MonoBehaviour
{
    public float speed = 2f; 

    private void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DeathZone")) 
        {
            Destroy(gameObject);
            Debug.Log("Blue ball destroyed!");
        }
    }
}
