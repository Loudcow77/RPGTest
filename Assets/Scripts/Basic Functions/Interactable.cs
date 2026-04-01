using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f; // The radius around the object
    public Transform interactionTransform; // The object being transformed

    // What we check if our item is the focus, defult its not
    // This is so we dont just start going after the item right away
    bool isFoucs = false;
    // Checks if we interacted with the item, defult its not
    bool hasInteracted = false;

    Transform player;
    // What happens when the object is interacted with
    public virtual void Interact()
    {
        Debug.Log("You Interacted with " + transform.name);
    }
    
    void Update()
    {
        // checks if the item is our foucs and if we havent interacted with it
        if (isFoucs && !hasInteracted)
        {
            // Makes the player go towards the object
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            // Check if we are inside the radius if we are then we interact with it
            if (distance <= radius)
            {
                Interact();
                hasInteracted = true;
            }
        }
    }
    // Called when object is focused
    public void onFocused(Transform playerTransform)
    {
        isFoucs = true;
        player = playerTransform;
        hasInteracted = false;
    }
    // Called when object is no longer in focus
    public void onDeFocused()
    {
        isFoucs = false;
        player = null;
        hasInteracted = false;
    }
    // Draw radius in the editor
    void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
            interactionTransform = transform;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
