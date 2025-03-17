using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    void Awake()
    {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }

    }

    public AudioSource ScoreCalcRev;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayScoreCalcRev() {

        ScoreCalcRev.pitch += 0.1f;

        ScoreCalcRev.time = 0;
        ScoreCalcRev.Play();
    }

    public void PlayScoreCalcRevNegative() {

        ScoreCalcRev.pitch -= 0.05f;

        ScoreCalcRev.time = 0;
        ScoreCalcRev.Play();
    }

    public void ResetScoreCalcRev() {
        ScoreCalcRev.pitch = 1f;
    }
}
