using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInteract : MonoBehaviour
{
    public bool isInRange;
    public bool withinBorders;
    public bool canInteract = true;
    public UnityEvent interactAction;
    [SerializeField] private Sprite noiseSprite;
    [SerializeField] private SpriteRenderer spriteRenderer;

    //private BoxCollider2D interactor;
    private DialogueManager dialogueManager;
    private PlayerMovement player;

    // Start is called before the first frame update
    void Start()
    {
        //interactor = GetComponent<BoxCollider2D>();
        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        //spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange)
        {
            if (gameObject.tag != "Questioning")
            {
                spriteRenderer.sprite = noiseSprite;
            }
            else if (player.disguised)
            {
                spriteRenderer.sprite = noiseSprite;
            }
            if (Input.GetKeyDown(KeyCode.Space) && canInteract && player.canMove && !player.disguised && !(gameObject.tag == "Questioning"))
            {
                interactAction.Invoke();
            }
            else if (Input.GetKeyDown(KeyCode.Space) && gameObject.tag == "Questioning" && canInteract && player.disguised)
            {
                interactAction.Invoke();
            }
        }
        else if (!withinBorders && (gameObject.tag == "FarDistance"))
        {
            spriteRenderer.sprite = null;
        }

        if (gameObject.tag == "FarDistance" && (dialogueManager.mediumDistance || dialogueManager.nearDistance))
        {
            isInRange = false;
        }
        else if (gameObject.tag == "FarDistance" && !(dialogueManager.mediumDistance || dialogueManager.nearDistance) && withinBorders)
        {
            isInRange = true;
        }

        if (gameObject.tag == "MediumDistance" && dialogueManager.nearDistance)
        {
            isInRange = false;
        }
        else if (gameObject.tag == "MediumDistance" && !dialogueManager.nearDistance && withinBorders)
        {
            isInRange = true;
        }
        else if (gameObject.tag == "Questioning" && !withinBorders)
        {
            spriteRenderer.sprite = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag == "NearDistance")
        {
            dialogueManager.nearDistance = true;
        }
        else if (gameObject.tag == "MediumDistance")
        {
            dialogueManager.mediumDistance = true;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = true;
            withinBorders = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (gameObject.tag == "NearDistance")
        {
            dialogueManager.nearDistance = false;
        }
        else if (gameObject.tag == "MediumDistance")
        {
            dialogueManager.mediumDistance = false;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            withinBorders = false;
        }
    }
}
