using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sendAlert : MonoBehaviour
{
    public DialogueHandler handler;
    GameObject obj;

    private void Start()
    {
        obj = GetComponent<GameObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(handler.introDone == false)
        {
            handler.StartDialogue(1, 0);
            handler.introDone = true;
        }
        else if (handler.npcDone == false && handler.introDoneDone)
        {
            handler.StartDialogue(3, 1, 0, false, 3, 0, 2);
            handler.npcDone = true;
        }
        else if(this.name == "BaseBetaPlatform" && handler.introDoneDone && handler.npcDone)
        {
            handler.StartFinalDialogue();
            
        }
        
        
    }

    private void OnTriggerExit(Collider other)
    {
        if(this.name != "SpawnRoom" && this.name != "BaseBetaPlatform")
        {
        handler.EndDialogue(4);
        }

    }
}
