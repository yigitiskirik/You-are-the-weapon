using UnityEngine;

public class followPlayer : MonoBehaviour
{
    public Transform player;           
    public float followSpeed = 5f;     
    public float cameraRadius = 10f;   

    private Vector3 offset;            

    void Start()
    {
        offset = transform.position - player.position;
    }

    void Update()
    {
        Vector3 directionToPlayer = transform.position - player.position;
        float currentDistance = directionToPlayer.magnitude;
        if (currentDistance < cameraRadius)
        {
            Vector3 desiredPosition = player.position + offset;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
        }
        else
        {
            directionToPlayer.Normalize();
            Vector3 boundaryPosition = player.position + directionToPlayer * cameraRadius;
            transform.position = boundaryPosition;
        }
    }
}
