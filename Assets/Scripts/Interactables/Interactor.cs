using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private float interactionPointRadius = 0.5f;
    [SerializeField] private LayerMask interactableMask;

    private Collider2D[] colliders = new Collider2D[3];
    private int numsFound;
    [SerializeField] private InteractionPromptUI interactionPromptUI;

    private IInteractable interactable;

    // Update is called once per frame
    void Update()
    {
        numsFound = Physics2D.OverlapCircleNonAlloc(interactionPoint.position, interactionPointRadius, colliders, interactableMask);

        if (numsFound > 0)
        {
            interactable = colliders[0].GetComponent<IInteractable>();

            if (interactable != null)
            {
                if (!interactionPromptUI.isDisplayed)
                {
                    interactionPromptUI.SetUp(interactable.InteractionPrompt);
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.Interact(this);
                    interactionPromptUI.Close();
                    interactionPromptUI.SetUp(interactable.InteractionPrompt);
                }
            }
        }

        else if (interactable != null)
        {
            interactable = null;
            interactionPromptUI.Close();
        }
    }
}
