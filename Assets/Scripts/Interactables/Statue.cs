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
            if (playerManager.BuyFragment())
            {
                Debug.Log("Buying fragment");
                SetAsUnable();
            }
            else
            {
                Debug.Log("You don't have enough soul fragments!");
            }
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
