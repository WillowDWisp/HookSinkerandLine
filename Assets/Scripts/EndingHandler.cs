using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndingHandler : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI endingText;
    [SerializeField] private RawImage blackScreen;

    [SerializeField] private List<string> endingMessages;

    // Start is called before the first frame update
    void Start()
    {
        endingText.text = "Due to unforseen circumstances, you are apparently dead. If you're seeing this, do let Devon know. Thanks!";

        endingText.faceColor = Color.clear;
        blackScreen.color = Color.clear;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TriggerEnding(int endMessageIndex)
    {
        blackScreen.color = Color.black;
        endingText.faceColor = Color.white;
        try
        {
            endingText.text = endingMessages[endMessageIndex];
        }
        catch
        {
            endingText.text = endingMessages[0];
        }
    }

}
