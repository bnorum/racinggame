using UnityEngine;

public class SceneManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGameAsUnitedStates() {
        PersistentData.playerCarType = PersistentData.CarType.AMERICAN;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }

    public void StartGameAsJapan() {
        PersistentData.playerCarType = PersistentData.CarType.JAPANESE;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }

    public void StartGameAsGermany() {
        PersistentData.playerCarType = PersistentData.CarType.GERMAN;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }

    public void StartGameAsMexico() {
        PersistentData.playerCarType = PersistentData.CarType.MEXICAN;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }

    public void StartGameAsBritain() {
        PersistentData.playerCarType = PersistentData.CarType.BRITISH;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }
    public void StartGameAsPrehistoric() {
        PersistentData.playerCarType = PersistentData.CarType.PREHISTORIC;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }

    public void ReturnToTitle() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("StartScene");
    }
}
