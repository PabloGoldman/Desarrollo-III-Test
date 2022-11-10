using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalMeteorite : MonoBehaviour, IInteractable
{
    [SerializeField] private string initialPrompt;

    public string InteractionPrompt => initialPrompt;

    public void Interact(Interactor interactor)
    {
        Debug.Log("meteorite interaction");
    }
}
