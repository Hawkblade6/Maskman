using UnityEngine;

public class ActivadorDialogo : MonoBehaviour, Interaccion
{
    [SerializeField] private DialogueObject dialogueObject;

    public void UpdateDialogueObject(DialogueObject dialogueObject)
    {
        this.dialogueObject = dialogueObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent(out Jugador jugador))
        {
            jugador.Interaccion = this;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent(out Jugador jugador))
        {
            if(jugador.Interaccion is ActivadorDialogo activadorDialogo && activadorDialogo == this)
            {
                jugador.Interaccion = null;
            }
        }
    }

    public void Interact(Jugador jugador) 
    {
        foreach (DialogueResponseEvents responseEvents in GetComponents<DialogueResponseEvents>())
        {
            if (responseEvents.DialogueObject == dialogueObject)
            {
                jugador.Dialogue.AddResponseEvents(responseEvents.Events);
                break;
            }
        }
        jugador.Dialogue.ShowDialogue(dialogueObject);
    }
}
