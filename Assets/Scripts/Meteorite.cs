using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Meteorite : MonoBehaviour, IDamageable
{
    [SerializeField] private float timeToEnd;
    [SerializeField] private float life;
    [SerializeField] private GameObject FinalScreen;
    [SerializeField] private ParticleSystem hurt;

    SpriteRenderer spriteRenderer;
    [SerializeField] Sprite idleSprite;
    [SerializeField] Sprite hurtSprite;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(float damage)
    {
        AkSoundEngine.PostEvent("Play_AsteroidHit", gameObject);
        life -= damage;

        hurt.Play();

        StartCoroutine(ChangeSpriteCoroutine());

        if (life <= 0)
        {
            AkSoundEngine.PostEvent("Play_Stinger_Win", gameObject); 
            AkSoundEngine.PostEvent("Stop_Musica", gameObject);
            AkSoundEngine.PostEvent("Stop_Ambiente_V2", gameObject);
           // FinalScreen.SetActive(true);
            Invoke("Credits", timeToEnd);
        }
    }

    private void Credits()
    {
        AkSoundEngine.PostEvent("Stop_Musica", gameObject);
        AkSoundEngine.PostEvent("Stop_Ambiente_V2", gameObject);
        SceneManager.LoadScene("Credits");
    }

    IEnumerator ChangeSpriteCoroutine()
    {
        spriteRenderer.sprite = hurtSprite;
        yield return new WaitForSeconds(0.3f);
        spriteRenderer.sprite = idleSprite;
    }
}
