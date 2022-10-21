using UnityEngine;

public class Statue : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;

    PlayerManager playerManager;

    bool isStatueAvailable = true;

    public string InteractionPrompt => prompt;

    private void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
    }

    public bool Interact(Interactor interactor)
    {
        if (isStatueAvailable)
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
        isStatueAvailable = false;
        GetComponent<SpriteRenderer>().color = Color.red;
    }
}
