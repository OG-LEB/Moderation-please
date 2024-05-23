using UnityEngine;

public class MouthTrigger : MonoBehaviour
{
    [SerializeField] private GameController gameController;
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("CheeseBall")) 
        {
            gameController.HitTarget();
        }
    }
}
