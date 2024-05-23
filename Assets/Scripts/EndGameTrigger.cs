using UnityEngine;

public class EndGameTrigger : MonoBehaviour
{
    [SerializeField] private GameController gameController;
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("CheeseBall")) 
        {
            gameController.GameOver();
            col.gameObject.SetActive(false);
        }
    }
}
