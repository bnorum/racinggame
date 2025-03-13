using TMPro;
using UnityEngine;

public class BonusText : MonoBehaviour
{
    public float timer = 1f;
    TextMeshProUGUI text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0) {
            Destroy(gameObject);
        }
        transform.position += Vector3.up * Time.deltaTime * 10f;
        //text.color = Color.Lerp(text.color, Color.clear, Time.deltaTime);
    }
}
