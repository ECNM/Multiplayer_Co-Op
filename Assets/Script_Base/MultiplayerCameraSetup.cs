using UnityEngine;

public class MultiplayerCameraSetup : MonoBehaviour
{
    public Camera player1Camera;
    public Camera player2Camera;

    void Start()
    {

        player1Camera.rect = new Rect(0, 0, 0.5f, 1);
        player2Camera.rect = new Rect(0.5f, 0, 0.5f, 1);


        player1Camera.gameObject.AddComponent<CameraSet>();
        player2Camera.gameObject.AddComponent<CameraSet>();
    }
}
