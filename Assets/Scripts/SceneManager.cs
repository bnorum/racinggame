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

    public void StartGame() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }

    public void ReturnToTitle() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("StartScene");
    }
}
