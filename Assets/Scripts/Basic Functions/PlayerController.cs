using UnityEngine.EventSystems;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    public Interactable focus;

    Camera cam;
    PlayerMotor motor;
    public LayerMask movementMask;

    void Start()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
    }

    void Update()
    {
        // Check if we are clicking on a gameObject if we are stop
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        // If we left click do this
        if (Input.GetMouseButtonDown(0))
        {
            // Creates a ray from the camera to where our mouse is
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            // check if we hit the floor or something we can walk on
            RaycastHit hit;
            // If we hit a walkable space move our player there
            if (Physics.Raycast(ray, out hit, 100, movementMask))
            {
                // Moves the player to the point we clicked
                motor.MoveToPoint(hit.point);
                // Calls removeFocus so we stop going after an object if we were
                removeFocus();
            }
        }
        // If we right click do this
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100)) 
            {
                // Checks if what we hit was an interactable
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                
                // If it was an interactable then set it as the focus
                if (interactable != null)
                {
                    setFocus(interactable); 
                }

            }
        }
    }
    // Makes it so we can interact with the focus
    void setFocus(Interactable newFocus)
    {
        // Checks if we arent going after the same object
        if (newFocus != focus)
        {
            if (focus != null)
                focus.onDeFocused();

            focus = newFocus;
            // Moves us to the new focus object
            motor.followTarget(newFocus);
        }
        
        newFocus.onFocused(transform);
    }
    // The opposite of setFoucs
    void removeFocus()
    {
        if (focus != null)
            focus.onDeFocused();
        
        focus = null;
        motor.stopFollowingTarget();
    }
}
