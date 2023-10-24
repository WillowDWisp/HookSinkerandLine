using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class EndingHandler : MonoBehaviour
{
    [SerializeField] AudioSource goodEnding;
    [SerializeField] AudioSource badEnding;
    [SerializeField] private TextMeshProUGUI endingText;
    [SerializeField] private RawImage blackScreen;

    [SerializeField] private List<string> endingMessages;

    OxygenTimer timer;
    DialogueHandler dialogueHandler;
    [SerializeField] Button restartButton;

    // Start is called before the first frame update
    void Start()
    {
        restartButton.image.color = Color.clear;
        restartButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.clear;
        restartButton.GetComponentInChildren<Text>().color = Color.clear;

        endingText.text = "Due to unforseen circumstances, you are apparently dead. If you're seeing this, do let Devon know. Thanks!";

        endingText.faceColor = Color.clear;
        blackScreen.color = Color.clear;

        timer = FindObjectOfType<OxygenTimer>();
        dialogueHandler = FindObjectOfType<DialogueHandler>();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //0 is error message, 1 is falling off edge, 2 is managing to run out of 02, 3 is good ending, 4 is bad ending, 5 is bad ending

    public void TriggerEnding(int endMessageIndex)
    {
        blackScreen.color = Color.black;
        endingText.faceColor = Color.white;
        dialogueHandler.EndDialogue(4);
        timer.StopTimer();
        restartButton.image.color = Color.white;
        restartButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
        restartButton.GetComponentInChildren<Text>().color = Color.white;

        if (endMessageIndex == 3)
        {
            goodEnding.Play();
        }
        if (endMessageIndex == 5)
        {
            badEnding.Play();
        }
        if (endMessageIndex == 4)
        {
            timer.alarm.mute = false;
            timer.alarm.volume = 0.5f;
        }

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
