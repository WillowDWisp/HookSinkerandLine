using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class BaseBetaDialogue : MonoBehaviour
{

    List<Button> buttons = new List<Button>();
    List<TextMeshProUGUI> buttonTexts = new List<TextMeshProUGUI>();

    TextMeshProUGUI dialogueText;
    TextMeshProUGUI dialogueOptOneText;
    TextMeshProUGUI dialogueOptTwoText;
    TextMeshProUGUI dialogueOptThreeText;
    [SerializeField] Button dialogueBox;
    [SerializeField] Button dialogueOption1;
    [SerializeField] Button dialogueOption2;
    [SerializeField] Button dialogueOption3;

    int currentSection = 1;

    Dictionary<string, string> dialogue = new Dictionary<string, string>();
    DialogueHandler handler;
    EndingHandler endingHandler;

    // Start is called before the first frame update
    void Start()
    {
        buttons.Add(dialogueBox);
        buttons.Add(dialogueOption1);
        buttons.Add(dialogueOption2);
        buttons.Add(dialogueOption3);

        endingHandler = FindObjectOfType<EndingHandler>();

        dialogueText = buttons[0].GetComponentInChildren<TextMeshProUGUI>();
        dialogueOptOneText = buttons[1].GetComponentInChildren<TextMeshProUGUI>();
        dialogueOptTwoText = buttons[2].GetComponentInChildren<TextMeshProUGUI>();
        dialogueOptThreeText = buttons[3].GetComponentInChildren<TextMeshProUGUI>();

        buttonTexts.Add(dialogueText);
        buttonTexts.Add(dialogueOptOneText);
        buttonTexts.Add(dialogueOptTwoText);
        buttonTexts.Add(dialogueOptThreeText);

        dialogue.Add("1-0", "*A voice comes on over the intercom* State your purpose.");
        dialogue.Add("1-1", "Breach at Lab Alpha, I'm here for reinforcements.");
        dialogue.Add("1-2", "There was a breach at Lab Alpha, I need to take shelter!");
        dialogue.Add("1-3", "My tank got damaged on the rocks while I was diving. My air's leaking, it runs out in thirty seconds, please -*cough*- let me in...");
        dialogue.Add("2-0", "When did this breach happen?");
        dialogue.Add("2-2", "20 minutes ago.");
        dialogue.Add("2-1", "5 minutes ago.");
        dialogue.Add("2-3", "I don't remember");
        dialogue.Add("3-0", "Did a supervisor at Alpha give you a passphrase?");
        dialogue.Add("3-2", "Something happened before they could tell me.");
        dialogue.Add("3-1", "Dr. Michigan was attacked before he could tell me.");
        dialogue.Add("3-3", "No, I don't think so...");
        dialogue.Add("4-0", "What? Where did you come from?");
        dialogue.Add("4-2", "I'm from Base Alpha...");
        dialogue.Add("4-1", "The surface..?");
        dialogue.Add("4-3", "I work here...");
        dialogue.Add("4-4", "...please forgive me.");
        dialogue.Add("5-0", "What's your name?");
        dialogue.Add("5-2", "Jason.");
        dialogue.Add("5-1", "Dr. Michigan.");
        dialogue.Add("5-3", "Atlan.");
        dialogue.Add("5-4", "Nobody works here with that name.");
        dialogue.Add("6-0", "...we'll open the door, just get in. I know it's your job, but you have to stop going out like this. You already sound like you're oxygen deprived. *The outer airlock door opens. You step into the room, and see the workers on the other side of a window.*");
        dialogue.Add("7-0", "*You've finally made it.*");
        dialogue.Add("7-1", "BREAK THE WINDOW");
        dialogue.Add("7-2", "BREAK THE WINDOW");
        dialogue.Add("7-3", "BREAK THE WINDOW");
        dialogue.Add("...", "...");
        dialogue.Add("SpecialSilence", ". . .");

    }

    private void Awake()
    {
        dialogueOption1.onClick.AddListener(Section2);
        dialogueOption2.onClick.AddListener(Section2);
        dialogueOption3.onClick.AddListener(Section4);
    }


    public void Section1()
    {
        dialogueText.text = dialogue["1-0"];
        dialogueOptOneText.text = dialogue["1-1"];
        dialogueOptTwoText.text = dialogue["1-2"];
        dialogueOptThreeText.text = dialogue["1-3"];
        currentSection = 1;


    }
    public void Section2()
    {
        dialogueText.text = dialogue["2-0"];
        dialogueOptOneText.text = dialogue["2-1"];
        dialogueOptTwoText.text = dialogue["2-2"];
        dialogueOptThreeText.text = dialogue["2-3"];
        currentSection = 2;

        dialogueOption1.onClick.RemoveAllListeners();
        dialogueOption2.onClick.RemoveAllListeners();
        dialogueOption3.onClick.RemoveAllListeners();
        dialogueOption1.onClick.AddListener(Section3);
        dialogueOption2.onClick.AddListener(Section3);
        dialogueOption3.onClick.AddListener(StraightToBad);
    }
    public void Section3()
    {
        dialogueText.text = dialogue["3-0"];
        dialogueOptOneText.text = dialogue["3-1"];
        dialogueOptTwoText.text = dialogue["3-2"];
        dialogueOptThreeText.text = dialogue["3-3"];
        currentSection = 3;

        dialogueOption1.onClick.RemoveAllListeners();
        dialogueOption2.onClick.RemoveAllListeners();
        dialogueOption3.onClick.RemoveAllListeners();
        dialogueOption1.onClick.AddListener(TheGoodOne);
        dialogueOption2.onClick.AddListener(TheGoodOne);
        dialogueOption3.onClick.AddListener(StraightToBad);
        
    }
    public void Section4()
    {
        dialogueText.text = dialogue["4-0"];
        dialogueOptOneText.text = dialogue["4-1"];
        dialogueOptTwoText.text = dialogue["4-2"];
        dialogueOptThreeText.text = dialogue["4-3"];
        currentSection = 4;

        dialogueOption1.onClick.RemoveAllListeners();
        dialogueOption2.onClick.RemoveAllListeners();
        dialogueOption3.onClick.RemoveAllListeners();
        dialogueOption1.onClick.AddListener(ForgiveMe);
        dialogueOption2.onClick.AddListener(Section3);
        dialogueOption3.onClick.AddListener(Section5);
    }
    public void Section5()
    {
        dialogueText.text = dialogue["5-0"];
        dialogueOptOneText.text = dialogue["5-1"];
        dialogueOptTwoText.text = dialogue["5-2"];
        dialogueOptThreeText.text = dialogue["5-3"];
        currentSection = 5;

        dialogueOption1.onClick.RemoveAllListeners();
        dialogueOption2.onClick.RemoveAllListeners();
        dialogueOption3.onClick.RemoveAllListeners();
        dialogueOption1.onClick.AddListener(NoName);
        dialogueOption2.onClick.AddListener(NoName);
        dialogueOption3.onClick.AddListener(Section6);
    }
    public void Section6()
    {
        dialogueText.text = dialogue["6-0"];
        dialogueOptOneText.text = dialogue["SpecialSilence"];
        dialogueOptTwoText.text = dialogue["SpecialSilence"];
        dialogueOptThreeText.text = dialogue["SpecialSilence"];
        currentSection = 6;

        dialogueOption1.onClick.RemoveAllListeners();
        dialogueOption2.onClick.RemoveAllListeners();
        dialogueOption3.onClick.RemoveAllListeners();
        dialogueOption1.onClick.AddListener(Section7);
        dialogueOption2.onClick.AddListener(Section7);
        dialogueOption3.onClick.AddListener(Section7);
    }
    public void Section7()
    {
        dialogueText.text = dialogue["7-0"];
        dialogueOptOneText.text = dialogue["7-1"];
        dialogueOptTwoText.text = dialogue["7-2"];
        dialogueOptThreeText.text = dialogue["7-3"];
        currentSection = 7;

        dialogueOption1.image.color = Color.black;
        dialogueOption2.image.color = Color.black;
        dialogueOption3.image.color = Color.black;
        dialogueOptOneText.color = Color.red;
        dialogueOptTwoText.color = Color.red;
        dialogueOptThreeText.color = Color.red;

        dialogueOption1.onClick.RemoveAllListeners();
        dialogueOption2.onClick.RemoveAllListeners();
        dialogueOption3.onClick.RemoveAllListeners();
        dialogueOption1.onClick.AddListener(TheTruth);
        dialogueOption2.onClick.AddListener(TheTruth);
        dialogueOption3.onClick.AddListener(TheTruth);
    }

    public void NoName()
    {
        dialogueText.text = dialogue["5-4"];
        dialogueOptOneText.text = dialogue["..."];
        dialogueOptTwoText.text = dialogue["..."];
        dialogueOptThreeText.text = dialogue["..."];
        currentSection = 8;

        dialogueOption1.onClick.RemoveAllListeners();
        dialogueOption2.onClick.RemoveAllListeners();
        dialogueOption3.onClick.RemoveAllListeners();
        dialogueOption1.onClick.AddListener(StraightToBad);
        dialogueOption2.onClick.AddListener(StraightToBad);
        dialogueOption3.onClick.AddListener(StraightToBad);
    }

    public void ForgiveMe()
    {
        dialogueText.text = dialogue["4-4"];
        dialogueOptOneText.text = dialogue["..."];
        dialogueOptTwoText.text = dialogue["..."];
        dialogueOptThreeText.text = dialogue["..."];
        currentSection = 8;

        dialogueOption1.onClick.RemoveAllListeners();
        dialogueOption2.onClick.RemoveAllListeners();
        dialogueOption3.onClick.RemoveAllListeners();
        dialogueOption1.onClick.AddListener(StraightToBad);
        dialogueOption2.onClick.AddListener(StraightToBad);
        dialogueOption3.onClick.AddListener(StraightToBad);

    }

    public void StraightToBad()
    {
        endingHandler.TriggerEnding(5);
    }
    public void TheGoodOne()
    {
        endingHandler.TriggerEnding(3);
    }
    public void TheTruth()
    {
        endingHandler.TriggerEnding(4);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
