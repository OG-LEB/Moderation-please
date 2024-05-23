using UnityEngine;

public class ModeratorMovement : MonoBehaviour
{

    [SerializeField] private float Speed;
    private bool movement;
    private bool GoLeft = true;
    private void Start()
    {
        StopMovement();
    }
    public void StartMovement()
    {
        movement = true;
    }
    public void StopMovement()
    {
        movement = false;
    }
    private void FixedUpdate()
    {
        if (movement)
        {
            Movement();
        }
    }
    private void Movement()
    {
        if (GoLeft)
        {
            transform.Translate(new Vector3(-1, 0, 0) * Time.deltaTime * Speed);
            if (transform.position.x <= -6)
            {
                GoLeft = false;
            }
        }
        else
        {
            transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * Speed);
            if (transform.position.x >= 6)
            {
                GoLeft = true;
            }
        }
    }
    public void ResetPosition()
    {
        transform.position = new Vector3(0, transform.position.y, transform.position.z);
    }
    public void Restart()
    {
        //Reset position
        int ver = Random.Range(0, 100);
        if (ver <= 50)
        {
            transform.position = new Vector3(-6, transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(6, transform.position.y, transform.position.z);
        }
        //Reset speed
        Speed = 1;
    }
    public void SpeedUp()
    {
        Speed += 0.1f;
    }
}
