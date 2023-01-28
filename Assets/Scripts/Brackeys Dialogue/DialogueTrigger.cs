using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	public Dialogue dialogueNear;
	public Dialogue dialogueMedium;
	public Dialogue dialogueFar;
	public Dialogue dialogueQuestioning;
	public Dialogue lastDialogue;
	private int distanceNear = 0;
	private int distanceMedium = 1;
	private int distanceFar = 2;
	private DialogueManager dialogueManager;

    private void Start()
    {
		dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
	}

	public void TriggerQuestioningDialogue()
	{
		if (!dialogueManager.inConversation)
		{
			dialogueManager.StartDialogue(dialogueQuestioning, distanceNear, true, true);
		}
		else
		{
			dialogueManager.DisplayNextSentence(dialogueQuestioning, distanceNear, true, true);
		}
	}

	public void TriggerDialogueNear()
	{
		if (!dialogueManager.inConversation)
		{
			dialogueManager.StartDialogue(dialogueNear, distanceNear, false, false);
		}
		else if (lastDialogue != dialogueNear)
		{
			dialogueManager.InterruptDialogue(dialogueNear, distanceNear, false);
		}
		else
		{
			dialogueManager.DisplayNextSentence(dialogueNear, distanceNear, false, false);
		}
		lastDialogue = dialogueNear;
		
	}
	public void TriggerDialogueMedium()
	{
		if (!dialogueManager.inConversation)
		{
			dialogueManager.StartDialogue(dialogueMedium, distanceMedium, false, false);
		}
		else if (lastDialogue != dialogueMedium)
		{
			dialogueManager.InterruptDialogue(dialogueMedium, distanceMedium, false);
		}
		else
		{
			dialogueManager.DisplayNextSentence(dialogueMedium, distanceMedium, false, false);
		}
		lastDialogue = dialogueMedium;
		
	}
	public void TriggerDialogueFar()
	{
		if (!dialogueManager.inConversation)
		{
			dialogueManager.StartDialogue(dialogueFar, distanceFar, false, false);
		}
		else if (lastDialogue != dialogueFar)
		{
			dialogueManager.InterruptDialogue(dialogueFar, distanceFar, false);
		}
		else
		{
			dialogueManager.DisplayNextSentence(dialogueFar, distanceFar, false, false);
		}
		lastDialogue = dialogueFar;
	}

}
