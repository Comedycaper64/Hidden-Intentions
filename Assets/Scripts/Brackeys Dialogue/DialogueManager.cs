using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	public Text nameText;
	public Text dialogueText;
	public bool inConversation = false;
	public bool mediumDistance = false;
	public bool nearDistance = false;
	public bool farDistance = false;
	private SuspicionScript suspicion;
	private int lastDistance = -1;
	[SerializeField] private int dialogueCounter = 0;

	//public Animator animator;

	private Queue<string> sentences;

	// Use this for initialization
	void Start () {
		sentences = new Queue<string>();
		suspicion = GameObject.FindGameObjectWithTag("SusSlider").GetComponent<SuspicionScript>();
	}

    private void Update()
    {

    }

	public void SetNameText(Dialogue dialogue)
	{
		nameText.text = dialogue.name;
	}

    public void StartDialogue (Dialogue dialogue, int distance, bool isQuestioning, bool noSuspicion)
	{
		//animator.SetBool("IsOpen", true);
		inConversation = true;
		sentences.Clear();
		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence(dialogue, distance, isQuestioning, noSuspicion);
	}

	public void InterruptDialogue(Dialogue dialogue, int distance, bool isQuestioning)
	{
		sentences.Clear();
		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}

		for (int i = 0; i < dialogueCounter; i++)
		{
			sentences.Dequeue();
		}
		DisplayNextSentence(dialogue, distance, isQuestioning, false);
	}

	public void DisplayNextSentence (Dialogue dialogue, int distance, bool isQuestioning, bool noSuspicion)
	{
		dialogueCounter++;

		lastDistance = distance;
		if (sentences.Count == 0)
		{
			EndDialogue(isQuestioning);
			return;
		}

		if (!noSuspicion)
		{
			if (distance == 0)
			{
				suspicion.AddSuspicion(10);
			}
			else if (distance == 1)
			{
				suspicion.AddSuspicion(3);
			}
		}
				
		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence (string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
	}

	public void EndDialogue(bool isQuestioning)
	{
		dialogueCounter = 0;
		//if is questioning start question or something
		lastDistance = -1;
		StartCoroutine(TypeSentence(""));
		inConversation = false;
		if (isQuestioning)
        {
			QuestioningEvent questioning = GameObject.FindGameObjectWithTag("Questioning").GetComponent<QuestioningEvent>();
			questioning.BeginQuestioning();
		}
		//animator.SetBool("IsOpen", false);
	}

}
