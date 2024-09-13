using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerWins : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private GameObject gameManager;
    [SerializeField] private GameObject portalExit;

    private bool isColliding = false;
    private bool buttonPressed = false;

    private Collider2D currentCollision;

    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PortalExit"))
        {
            isColliding = true;
            currentCollision = collision;
            TryWin();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PortalExit"))
        {
            isColliding = false;
            currentCollision = null;
        }
    }

    public void InteractButtonPressed()
    {
        buttonPressed = true;
        TryWin();
    }

    public void InteractButtonReleased()
    {
        buttonPressed = false;
    }

    private void TryWin()
    {
        if (isColliding && buttonPressed)
        {
            Win();
        }
    }

    private void Win()
    {
        GetComponent<PlayerMovement2>().enabled = false;
        UsePortal();
        animator.SetTrigger("playerOut");
    }

    private void UsePortal()
    {
        portalExit.GetComponent<ActivatePortal>().Activate();
    }

    private void NextLevel()
    {
        gameManager.GetComponent<GameManager>().LoadNextLevel();
    }

}
