using UnityEngine;
using UnityEngine.SceneManagement;

public class Meteorite : MonoBehaviour, IDamageable
{
    [SerializeField]private float timeToEnd;
    [SerializeField]private float life;
    [SerializeField]private GameObject FinalScreen;
    [SerializeField]private  ParticleSystem hurt;

    public void TakeDamage(float damage)
   {
       life -= damage;
       hurt.Play();
       if (life <= 0)
       {
           AkSoundEngine.PostEvent("Stop_Musica", gameObject);
           AkSoundEngine.PostEvent("Stop_Ambiente_V2", gameObject);
           FinalScreen.SetActive(true);
           Invoke("Credits", timeToEnd);
       }
   }

    private void Credits()
    {
        AkSoundEngine.PostEvent("Stop_Musica", gameObject);
        AkSoundEngine.PostEvent("Stop_Ambiente_V2", gameObject);
        SceneManager.LoadScene("Credits");
    }
}
