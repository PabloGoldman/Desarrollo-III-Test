using System;
using UnityEngine;

public class Statue : MonoBehaviour, IInteractable
{
    string prompt;

    [SerializeField] private string initialPrompt;
    [SerializeField] private string notAvailablePrompt;
    [SerializeField] private string promptAfterInteraction;
    [SerializeField] private int id;

    public int ID
    {
        get => id;
        set => id = value;
    }

    PlayerManager playerManager;
    bool isAvailable = true;
    public static event Action<int> SendID;
    public string InteractionPrompt => prompt;

    private void Start()
    {
        prompt = initialPrompt;
        playerManager = FindObjectOfType<PlayerManager>();
    }

    public void Interact(Interactor interactor)
    {
        if (gameObject.name != "MeteoriteTriggerPrompt")
        {
            if (isAvailable)
            {
                if (playerManager.BuyFragment())
                {
                    Debug.Log("Buying fragment");
                    SetAsUnable();
                    AkSoundEngine.PostEvent("Play_Totem_Compra", gameObject);
                    AkSoundEngine.PostEvent("Play_VO_Entrega", gameObject);
                    SendID?.Invoke(id);
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
