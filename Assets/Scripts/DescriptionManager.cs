using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionManager : MonoBehaviour
{
    public List<GameObject> buttons;
    public List<string> descriptions;

    public static DescriptionManager Instance { get; private set; }
    void Awake()
    {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }

    public TextMeshProUGUI descriptionText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject button in buttons) {
            if (IsPointerOverUIElement(button.GetComponent<RectTransform>())) {
                int index = buttons.IndexOf(button);
                descriptionText.text = descriptions[index];
            }
        }
    }



    private bool IsPointerOverUIElement(RectTransform rectTransform)
    {
        // Get the current mouse or touch position
        Vector2 screenPosition = Input.mousePosition; // For mouse
        if (Input.touchCount > 0)
        {
            screenPosition = Input.GetTouch(0).position; // For touch
        }

        // Check if the screen position is within the RectTransform
        return RectTransformUtility.RectangleContainsScreenPoint(rectTransform, screenPosition, null);
    }
}
