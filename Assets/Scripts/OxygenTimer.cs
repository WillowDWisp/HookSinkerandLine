using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OxygenTimer : MonoBehaviour
{
    public bool isTimerRunning = false;

    [SerializeField] EndingHandler handler;

    int timeLeft = 180;
    int minutesLeft = 5;
    int secondsLeft = 0;

    DialogueHandler handlerD;

    [SerializeField] TextMeshProUGUI timer;
    [SerializeField] GameObject player;
    [SerializeField] GameObject teleportSpot;

    // Start is called before the first frame update
    void Start()
    {
        handlerD = FindObjectOfType<DialogueHandler>();
        timer.color = Color.clear;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimerRunning)
        {

            if (timeLeft < 0)
            {
                StopCoroutine("RunTimer");
                isTimerRunning = false;
                handler.TriggerEnding(2);
                return;
            }
            
            minutesLeft = Mathf.FloorToInt(timeLeft / 60);
            secondsLeft = Mathf.FloorToInt(timeLeft % 60);
            
            if(secondsLeft != 0)
            {
                timer.SetText("Time left: " + minutesLeft + ":" + secondsLeft);
            }
            
            else
            {
                timer.SetText("Time left: " + minutesLeft + ":00");
            }
            
        }

        if (!isTimerRunning)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                StartTimer();
            }
        }
        
    }
    
    void StartTimer()
    {
        handlerD.introDoneDone = true;
        handlerD.EndDialogue(4);

        isTimerRunning = true;
        timer.color = Color.white;
        player.transform.position = teleportSpot.transform.position;
        player.transform.rotation = new Quaternion(0, 180, 0, player.transform.rotation.w);
        StartCoroutine("RunTimer");
    }

    void StopTimer()
    {
        isTimerRunning = false;
        StopAllCoroutines();
    }

    IEnumerator RunTimer()
    {
        yield return StartCoroutine("DecreaseTime");
        timeLeft--;
        StartCoroutine("RunTimer");
    }

    IEnumerator DecreaseTime()
    {

        yield return new WaitForSecondsRealtime(1);
        
        
        
    }
}
