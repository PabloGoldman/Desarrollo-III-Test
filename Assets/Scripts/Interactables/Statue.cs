using UnityEngine;

public class Statue : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;

    PlayerManager playerManager;

    bool isStatueAvailable = true;

    //Aca podria estar el costo de la estatua

    public string InteractionPrompt => prompt;

    private void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
    }

    public void Interact(Interactor interactor)
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
    }

    void SetAsUnable()
    {
        isStatueAvailable = false;
        prompt = "Already purchased!";
        GetComponent<SpriteRenderer>().color = Color.red;
    }
}
