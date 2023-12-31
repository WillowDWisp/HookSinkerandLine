using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueHandler : MonoBehaviour
{
    //dialogue from NPCs
    List<string> IntroDialogue = new List<string>();
    List<string> NPCDialogue = new List<string>();
    List<string> BaseBetaDialogue = new List<string>();

    //dialogue to NPCs
    List<string> NPCDialogueOptions = new List<string>();
    List<string> BaseBetaDialogueOptions = new List<string>();

    List<List<string>> ListOfLists = new List<List<string>>();

    List<Button> buttons = new List<Button>();
    List<TextMeshProUGUI> buttonTexts = new List<TextMeshProUGUI>();

    public bool introDone;
    public bool npcDone;
    public bool baseReached;

    [SerializeField] PlayerCamera PCam;

    //objects
    [SerializeField] Button dialogueBox;
    [SerializeField] Button dialogueOption1;
    [SerializeField] Button dialogueOption2;
    [SerializeField] Button dialogueOption3;
    TextMeshProUGUI dialogueText;
    TextMeshProUGUI dialogueOptOneText;
    TextMeshProUGUI dialogueOptTwoText;
    TextMeshProUGUI dialogueOptThreeText;

    BaseBetaDialogue BBDobj;

    int opt1times = 0;
    int opt2times = 0;
    int opt3times = 0;

    int numOfAnswers;
    //what dialogue list is in use?
    int currentList = 0;
    //what list of dialogue options is in use?
    int currentOptionList = 0;
    //index of current dialogue option (npc side)
    int currIndex = 0;
    //where in the dictionary are we?
    int currDictPos = 0;

    bool inDialogue = false;
    public bool introDoneDone = false;

    Dictionary<string, int> dialoguePaths = new Dictionary<string, int>();

    // Start is called before the first frame update
    void Start()
    {

        InitializeDialogue();
        InitializeDict();

        buttons.Add(dialogueBox);
        buttons.Add(dialogueOption1);
        buttons.Add(dialogueOption2);
        buttons.Add(dialogueOption3);

        ListOfLists.Add(IntroDialogue);
        ListOfLists.Add(NPCDialogue);
        ListOfLists.Add(BaseBetaDialogue);
        ListOfLists.Add(NPCDialogueOptions);
        ListOfLists.Add(BaseBetaDialogueOptions);

        dialogueText = buttons[0].GetComponentInChildren<TextMeshProUGUI>();
        dialogueOptOneText = buttons[1].GetComponentInChildren<TextMeshProUGUI>();
        dialogueOptTwoText = buttons[2].GetComponentInChildren<TextMeshProUGUI>();
        dialogueOptThreeText = buttons[3].GetComponentInChildren<TextMeshProUGUI>();

        buttonTexts.Add(dialogueText);
        buttonTexts.Add(dialogueOptOneText);
        buttonTexts.Add(dialogueOptTwoText);
        buttonTexts.Add(dialogueOptThreeText);

        EndDialogue(4);
        BBDobj = FindObjectOfType<BaseBetaDialogue>();
        StartCoroutine("startSequence");


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            StartDialogue(2, 2);
        }


        if (inDialogue)
        {
            if (Input.GetKeyDown(KeyCode.Space) && (numOfAnswers == 0 || numOfAnswers == 1))
            {
                
                if(currIndex == ListOfLists[currentList].Count-1)
                {
                    LoadDialogue(0, 0, true);
                }
                else
                {
                    LoadDialogue(currentList, currIndex+1, false);
                }
                currIndex++;
            }

            if (Input.GetKeyDown(KeyCode.Alpha1) && numOfAnswers == 2)
            {
                LoadDialogue(1, 2, false, 3, currIndex, 2);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && numOfAnswers == 2)
            {
                LoadDialogue(1, 1, false, 3, currIndex, 2);
            }

            if (Input.GetKeyDown(KeyCode.Alpha1) && numOfAnswers == 3)
            {
                
                LoadDialogue(2, currentOptionList, false, 4, currIndex, 3);
                opt1times++;
                currentOptionList++;
                currIndex += 3;

            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && numOfAnswers == 3)
            {
                LoadDialogue(2, currentOptionList, false, 4, currIndex, 3);
                opt2times++;
                currentOptionList++;
                currIndex += 3;

            }
            if (Input.GetKeyDown(KeyCode.Alpha3) && numOfAnswers == 3)
            {
                LoadDialogue(2, currentOptionList, false, 4, currIndex, 3);
                opt3times++;
                currentOptionList++;
                currIndex += 3;

            }
        }

        
    }

    
    IEnumerator startSequence()
    {
        yield return new WaitForSeconds(1f);
        StartDialogue(0, 0);
    }

    public void EndDialogue(int numDialogue)
    {
        for (int i = 0; i < numDialogue; i++)
        {
            buttons[i].image.color = Color.clear;
            buttonTexts[i].color = Color.clear;
        }
        currIndex = 0;
        currentList++;
        inDialogue = false;
    }

    public void StartDialogue(int numDialogue, int index)
    {
        for(int i = 0; i < numDialogue; i++)
        {
            buttons[i].image.color = Color.blue;
            buttonTexts[i].color = Color.white;
        }
        inDialogue = true;
        LoadDialogue(index, 0, false);
    }

    public void StartDialogue(int numDialogue, int ListIndex, int DialogueIndex, bool dialogueEnd, int replyIndex, int AnswerIndex, int num)
    {
        for (int i = 0; i < numDialogue; i++)
        {
            buttons[i].image.color = Color.blue;
            buttonTexts[i].color = Color.white;
        }
        inDialogue = true;

        LoadDialogue(ListIndex, DialogueIndex, dialogueEnd, replyIndex, AnswerIndex, num);
    }

   public void StartFinalDialogue()
    {
        for (int i = 0; i < 4; i++)
        {
            buttons[i].image.color = Color.blue;
            buttonTexts[i].color = Color.white;
        }
        inDialogue = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        BBDobj.Section1();
        PCam.sensX = 10;
        PCam.sensY = 10;

    }

    //the following 3 overloads load dialogue with 0, 1, and 2/3 replies respectively
    public void LoadDialogue(int ListIndex, int DialogueIndex, bool dialogueEnd)
    {
        if (dialogueEnd)
        {
            EndDialogue(1);
        }
        else
        {
            currentList = ListIndex;


            List<string> L = ListOfLists[ListIndex];

            numOfAnswers = 0;
            dialogueText.text = L[DialogueIndex] + " (Press Space to Continue)";
        }
        
    }
 
    
    //this is for 2/3 reply dialogues
    void LoadDialogue(int ListIndex, int DialogueIndex, bool dialogueEnd, int replyIndex, int FirstAnswerIndex, int numAnswers)
    {
        if (dialogueEnd)
        {
            EndDialogue(1 + numAnswers);
        }
        else
        {
            List<string> L = ListOfLists[ListIndex];
            List<string> D = ListOfLists[replyIndex];
            
            dialogueText.text = L[DialogueIndex];
            dialogueOptOneText.text = "(Press 2) " + D[FirstAnswerIndex];
            dialogueOptTwoText.text = "(Press 1) " + D[FirstAnswerIndex + 1];
            if(numAnswers == 3)
            {
                dialogueOptThreeText.text = "(Press 3) " + D[FirstAnswerIndex + 2];
                numOfAnswers = 3;
            }
            else
            {
                numOfAnswers = 2;
            }
        }

    }

    //puts references to the positions of dialogue in the 
    void InitializeDict()
    {




    }

    //runs the dialogue initialization scripts
    void InitializeDialogue()
    {
        InitializeIntro();
        InitializeNPC();
        InitializeBaseBeta();
        InitializeBaseBetaDialogue();
        InitializeNPCDialogue();
    }

    void InitializeIntro()
    {
        List<string> L = new List<string>();
        L.Add("static ...an you hear me? Hello? Oh, Huron, I'm glad it's you.We have an emergency, I only have time to tell you this once. As you may know, twenty minutes ago tanks 27 - 81 all simultaneously broke.");
        L.Add("This is a Class Zero Containment breach.The only way of stopping this is to get reinforcements from the neighboring base, our storeroom has already been taken over and flooded completely.");
        L.Add("Our last remaining suit is by the exit there, we couldn't refill the tank so you have three minutes of air to get to Beta.Once you get there, just tell them-");
        L.Add("*A deafeningly loud bang comes from the intercom. There won't be any more communications. You need to leave.");
        L.Add("[Clicking the door will start the game; In future runs, you can just click the door to skip this section.]");
        IntroDialogue = L;
    }

    void InitializeNPC()
    {
        List<string> L = new List<string>();
        L.Add("Hey, I heard something happening over at Alpha, what's going on?");
        L.Add("...goodbye?");
        L.Add("Oh, I just came from there. Tell them Atlan says you can go through, they'll understand if you tell them it's urgent. You can rest once you get there, take some medicine for your cold. Good luck!");
        NPCDialogue = L;
    }

    void InitializeBaseBeta()
    {
        List<string> L = new List<string>();
        L.Add("State the purpose of your visit.");
        L.Add("How long ago did the breach happen?");
        L.Add("What's the passphrase?");
        L.Add("");
        L.Add("");
        BaseBetaDialogue = L;
    }

    void InitializeNPCDialogue()
    {
        List<string> L = new List<string>();
        L.Add("Something important, I have to leave.");
        L.Add("Containment breach, I'm going to Beta for help.");
        NPCDialogueOptions = L;
    }



    void InitializeBaseBetaDialogue()
    {
        List<string> L = new List<string>();
        L.Add("There was a containment breach at Station Alpha, and I'm here to get help.");
        L.Add("There was a containment breach at Station Alpha, and I'm here to take shelter.");
        L.Add("I'm out for a stroll. Oh also there was a breach at Station Alpha.");

        L.Add("About 20 Minutes ago.");
        L.Add("About 5 minutes ago.");
        L.Add("I don't remember.");

        L.Add("Not sure, the mic cut out before Dr. Michigan could say it");
        L.Add("No idea, Dr. Michigan died before he could tell me.");
        L.Add("I wasn't paying attention.");

        L.Add("");
        L.Add("");
        L.Add("");

        L.Add("");
        L.Add("");
        L.Add("");
        BaseBetaDialogueOptions = L;
    }



    //unused
    void LoadDialogue(int ListIndex, int DialogueIndex, bool dialogueEnd, int replyIndex, int AnswerIndex)
    {
        if (dialogueEnd)
        {
            EndDialogue(2);
        }
        else
        {
            currentList = ListIndex;


            List<string> L = ListOfLists[ListIndex];
            List<string> D = ListOfLists[replyIndex];

            dialogueText.text = L[DialogueIndex];
            numOfAnswers = 1;
            dialogueOptOneText.text = D[AnswerIndex] + " (Press Space to Continue)";
        }


    }
}
