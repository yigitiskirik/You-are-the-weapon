using UnityEngine;

public class Door : MonoBehaviour
{
    public GameManager gameManager;
    public enum Orientation {
        Left,
        Right,
        Top,
        Bottom
    }

    public Orientation orientation;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player"))
        {
            switch (orientation)
            {
                case Orientation.Bottom:
                    gameManager.EnterBottomDoor();
                    break;
                case Orientation.Top:
                    gameManager.EnterTopDoor();
                    break;
                case Orientation.Left:
                    gameManager.EnterLeftDoor();
                    break;
                default:
                    gameManager.EnterRightDoor();
                    break;
            }
        }
    }
}
