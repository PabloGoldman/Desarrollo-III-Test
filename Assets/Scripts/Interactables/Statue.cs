using UnityEngine;

public class Statue : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;

    [SerializeField] PlayerManager playerManager;

    bool isPlayerAbleToBuy = true;

    public string InteractionPrompt => prompt;

    private void Start()
    {
        if (!playerManager)
        {
            playerManager = FindObjectOfType<PlayerManager>();
        }
    }

    public bool Interact(Interactor interactor)
    {
        if (isPlayerAbleToBuy)
        {
            Debug.Log("Buying fragment");
            SetAsUnable();
            playerManager.BuyFragment();
        }
        else
        {
            Debug.Log("Already bought on this statue");
        }

        return true;
    }

    void SetAsUnable()
    {
        isPlayerAbleToBuy = false;
        GetComponent<SpriteRenderer>().color = Color.red;
    }
}
