using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusScript : MonoBehaviour
{
    [SerializeField] private GameObject voiceRange;
    private DialogueManager dialogueManager;
    private DialogueTrigger dialogueTrigger;
    private PlayerMovement player;
    private bool playerHasDisguised = false;
    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
        dialogueTrigger = GetComponent<DialogueTrigger>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.disguised && !playerHasDisguised)
        {
            dialogueManager.EndDialogue(false);
            var otherRanges = GameObject.FindGameObjectsWithTag("VoiceRange");
            foreach (var range in otherRanges)
            {
                range.SetActive(false);
            }
            playerHasDisguised = true;
        }
    }

    private void OnMouseDown()
    {
        if (!player.disguised)
        {
            dialogueManager.SetNameText(dialogueTrigger.dialogueNear);
            dialogueManager.EndDialogue(false);
            var otherRanges = GameObject.FindGameObjectsWithTag("VoiceRange");
            foreach (var range in otherRanges)
            {
                range.SetActive(false);
            }
            voiceRange.SetActive(true);
        }
    }
}
