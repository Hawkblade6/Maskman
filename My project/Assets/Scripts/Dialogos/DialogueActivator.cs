using UnityEngine;

public class DialogueActivator : MonoBehaviour, Interactable
{
    [SerializeField] private DialogueObject dialogueObject;
    [SerializeField] private PlayerController playerController;

    //public void Update()
    //{
    //    playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    //}

    public void UpdateDialogueObject(DialogueObject dialogueObject)
    {
        this.dialogueObject = dialogueObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent(out PlayerController player))
        {
            player.Interactable = this;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent(out PlayerController player))
        {
            if (player.Interactable is DialogueActivator dialogueActivator && dialogueActivator == this)
            {
                player.Interactable = null;
                player.Dialogue.CloseDialogueBox();
            }
        }
    }

    public void Interact(PlayerController player)
    {
        foreach (DialogueResponseEvents responseEvents in GetComponents<DialogueResponseEvents>())
        {
            if (responseEvents.DialogueObject == dialogueObject)
            {
                player.Dialogue.AddResponseEvents(responseEvents.Events);
                break;
            }
        }
        player.Dialogue.ShowDialogue(dialogueObject);
    }
}

