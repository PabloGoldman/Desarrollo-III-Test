using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private float interactionPointRadius = 0.5f;
    [SerializeField] private LayerMask interactableMask;

    private Collider2D[] colliders = new Collider2D[3];
    [SerializeField] private int numsFound;

    // Update is called once per frame
    void Update()
    {
        numsFound = Physics2D.OverlapCircleNonAlloc(interactionPoint.position, interactionPointRadius, colliders, interactableMask);

        if (numsFound > 0)
        {
            var interactable = colliders[0].GetComponent<IInteractable>();

            if (interactable != null && Input.GetKeyDown(KeyCode.E))
            {
                interactable.Interact(this);
            }
        }
    }
}
