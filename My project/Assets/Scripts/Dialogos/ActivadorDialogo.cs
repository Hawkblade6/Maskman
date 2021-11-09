using UnityEngine;

public class ActivadorDialogo : MonoBehaviour, Interaccion
{
    [SerializeField] private DialogueObject dialogueObject;

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
        if (TryGetComponent(out DialogueResponseEvents responseEvents))
        {
            jugador.Dialogue.AddResponseEvents(responseEvents.Events);
        }
        jugador.Dialogue.ShowDialogue(dialogueObject);
    }
}
