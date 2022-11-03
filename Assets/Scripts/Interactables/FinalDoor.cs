using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoor : MonoBehaviour, IInteractable
{
    string prompt;

    [SerializeField] private string initialPrompt;

    [SerializeField] private string notAvailablePrompt;

    [SerializeField] private string promptAfterInteraction;

    public string InteractionPrompt => prompt;

    bool isAvailable = true;

    PlayerManager playerManager;

    [SerializeField] Color finalDoorColor;

    private void Start()
    {
        prompt = initialPrompt;
        playerManager = FindObjectOfType<PlayerManager>();
    }

    public void Interact(Interactor interactor)
    {
        if (isAvailable)
        {
            if (playerManager.CanUnlockEndDoor())
            {
                Debug.Log("Buying fragment");
                SetAsUnable();
            }
            else
            {
                SetAsNotAvailable();
                Invoke(nameof(SetAsAble), 0.1f);
                Debug.Log("You don't have enough soul fragments!");
            }
        }
    }

    void SetAsAble()
    {
        prompt = initialPrompt;
    }

    void SetAsUnable()
    {
        isAvailable = false;
        prompt = promptAfterInteraction;

        SpriteRenderer[] renderer = GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer aux in renderer)
        {
            aux.color = finalDoorColor;
        }

        Destroy(GetComponent<BoxCollider2D>());
    }

    void SetAsNotAvailable()
    {
        prompt = notAvailablePrompt;
    }

}
