using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;

    [SerializeField] private string promptAfterInteraction;

    public string InteractionPrompt => prompt;

    PlayerManager playerManager;

    private void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
    }

    public void Interact(Interactor interactor)
    {
        if (playerManager.CanUnlockEndDoor())
        {
            SetAsUnable();
        }
    }

    void SetAsUnable()
    {

        GetComponent<SpriteRenderer>().color = Color.clear;
    }

}
