using UnityEngine;

public class Statue : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;

    public string InteractionPrompt => prompt;

    public bool Interact(Interactor interactor)
    {
        Debug.Log("Interacting with Statue");
        return true;
    }
}
