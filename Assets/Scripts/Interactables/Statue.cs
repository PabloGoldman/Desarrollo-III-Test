using UnityEngine;

public class Statue : MonoBehaviour, IInteractable
{
    string prompt;

    [SerializeField] private string initialPrompt;

    [SerializeField] private string notAvailablePrompt;

    [SerializeField] private string promptAfterInteraction;

    PlayerManager playerManager;

    bool isAvailable = true;

    //Aca podria estar el costo de la estatua

    public string InteractionPrompt => prompt;

    private void Start()
    {
        prompt = initialPrompt;
        playerManager = FindObjectOfType<PlayerManager>();
    }

    public void Interact(Interactor interactor)
    {
        if (isAvailable)
        {
            if (playerManager.BuyFragment())
            {
                Debug.Log("Buying fragment");
                SetAsUnable();
                AkSoundEngine.PostEvent("Play_Totem_Compra", gameObject);
                AkSoundEngine.PostEvent("Play_VO_Entrega", gameObject);

            }
            else
            {
                SetAsNotAvailable();
                Invoke(nameof(SetAsAble), 0.1f);
                Debug.Log("You don't have enough soul fragments!");
                AkSoundEngine.PostEvent("Play_Totem_NO", gameObject);

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
        GetComponent<SpriteRenderer>().color = Color.gray;
    }

    void SetAsNotAvailable()
    {
        prompt = notAvailablePrompt;
    }
}
