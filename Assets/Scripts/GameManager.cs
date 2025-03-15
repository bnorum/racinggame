using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public CanvasScaler canvasScaler;

    void Awake()
    {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        DecideResolution();
    }
    public void DecideResolution() {
        if (Screen.width > Screen.height) {
            //landscape
            canvasScaler.matchWidthOrHeight = 0f;
        } else {
            //portrait
            canvasScaler.matchWidthOrHeight = 1f;
        }
    }


}
