using UnityEngine;

public class Balloon : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private BalloonManager manager;
    public float speed = 1f; 

    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer not found in children of " + gameObject.name);
        }
    }

    public void SetManager(BalloonManager newManager)
    {
        manager = newManager;
    }

    public void SetBalloonType(string type)
    {
        if (spriteRenderer == null) return; 

        if (type == "red")
        {
            spriteRenderer.color = Color.red; 
        }
        else
        {
            spriteRenderer.color = Color.green; 
        }
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
        Debug.Log("Balloon speed set to: " + speed);
    }

    void Explode()
    {
        manager.BalloonExploded();
        manager.LoseLife();
    }
}
