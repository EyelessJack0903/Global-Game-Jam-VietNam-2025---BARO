using UnityEngine;

public class BallClickHandler : MonoBehaviour
{
    private void OnMouseDown()
    {
        Debug.Log(gameObject.name + " was clicked!");

        ExplodeBall();
    }

    private void ExplodeBall()
    {
        Destroy(gameObject);
    }
}
