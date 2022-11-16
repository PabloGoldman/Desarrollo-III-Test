using UnityEngine;
using UnityEngine.SceneManagement;

public class Meteorite : MonoBehaviour, IDamageable
{
    [SerializeField]private float timeToEnd;
    [SerializeField] private float life;

   public void TakeDamage(float damage)
   {
       life -= damage;
       if(life<=0) Invoke("Credits", timeToEnd);
   }

    private void Credits()
    {
        AkSoundEngine.PostEvent("Stop_Musica", gameObject);
        AkSoundEngine.PostEvent("Stop_Ambiente_V2", gameObject);
        SceneManager.LoadScene("Credits");
    }
}
