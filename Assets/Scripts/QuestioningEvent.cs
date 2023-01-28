using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestioningEvent : MonoBehaviour
{
    private DialogueManager dialogueManager;
    [SerializeField] private RuntimeAnimatorController[] possibleDisguises;
    [SerializeField] private Dialogue[] Question1;
    [SerializeField] private Dialogue[] Question2;
    [SerializeField] private Dialogue[] Question3;
    private Dialogue[] QuestionToSend;
    private string questionToAsk;
    private string answer1;
    private string answer2;
    private string answer3;
    private string answer4;
    private string acceptedAnswer;
    [SerializeField] private Text answerBox;
    [SerializeField] private Sprite openDoor;
    [SerializeField] private GameObject door;
    [SerializeField] private int disguiseNumber;
    public Dialogue correctDialogue;
    public Dialogue incorrectDialogue;
    private SuspicionScript susScript;
    private PlayerMovement player;
    private bool acceptInput = false;

    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
        susScript = GameObject.FindGameObjectWithTag("SusSlider").GetComponent<SuspicionScript>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (acceptInput)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (answer1 == acceptedAnswer)
                {
                    CorrectAnswer();
                }
                else
                {
                    IncorrectAnswer();
                }
                answerBox.gameObject.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (answer2 == acceptedAnswer)
                {
                    CorrectAnswer();
                }
                else
                {
                    IncorrectAnswer();
                }
                answerBox.gameObject.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (answer3 == acceptedAnswer)
                {
                    CorrectAnswer();
                }
                else
                {
                    IncorrectAnswer();
                }
                answerBox.gameObject.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                if (answer4 == acceptedAnswer)
                {
                    CorrectAnswer();
                }
                else
                {
                    IncorrectAnswer();
                }
                answerBox.gameObject.SetActive(false);
            }

        }
    }

    public void BeginQuestioning()
    {
        for (int i = 0; i < possibleDisguises.Length; i++)
        {
            if (player.animator.runtimeAnimatorController == possibleDisguises[i])
            {
                disguiseNumber = i;
            }
        }
        var tempRandom = Random.Range(0, 3);
        if (tempRandom == 0)
        {
            QuestionToSend = Question1;
        }
        else if (tempRandom == 1)
        {
            QuestionToSend = Question2;
        }
        else
        {
            QuestionToSend = Question3;
        }
            
        CheckDisguises(disguiseNumber, QuestionToSend);

        answerBox.gameObject.SetActive(true);
        answerBox.text = (questionToAsk + "\n\n1. " + answer1 + "\n2. " + answer2 + "\n3. " + answer3 + "\n4. " + answer4 + "\n (Use 1,2,3,4 to answer)");
        acceptInput = true;
    }

    //0 = Suited Man 1 
    //1 = Pink Woman 2
    //2 = Casual Male 3
    //3 = Blue Haired Woman 4
    //4 = Bald Man 5
    //5 = Captain 6
    //6 = Clown 7
    //7 = Glasses Guy 8
    //8 = Rich Man 9
    //9 = Turban Guy 10

    private void CheckDisguises(int disguise, Dialogue[] question)
    {
        questionToAsk = question[disguise].sentences[0];
        answer1 = question[disguise].sentences[1];
        answer2 = question[disguise].sentences[2];
        answer3 = question[disguise].sentences[3];
        answer4 = question[disguise].sentences[4];
        if (disguise % 4 == 0)
        {
            acceptedAnswer = answer1;
        }
        else if (disguise % 4 == 1)
        {
            acceptedAnswer = answer2;
        }
        else if (disguise % 4 == 2)
        {
            acceptedAnswer = answer3;
        }
        else if (disguise % 4 == 3)
        {
            acceptedAnswer = answer4;
        }

    }

    private void CorrectAnswer()
    {
        acceptInput = false;
        door.SetActive(false);
        dialogueManager.StartDialogue(correctDialogue, 0, false, true);
        GetComponent<PlayerInteract>().canInteract = false;
    }

    private void IncorrectAnswer()
    {
        acceptInput = false;
        dialogueManager.StartDialogue(incorrectDialogue, 0, false, true);
        susScript.AddSuspicion(50);
    }
}
