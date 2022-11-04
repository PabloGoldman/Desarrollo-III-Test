using System;
using UnityEngine;

public class HowToPlay : MonoBehaviour
{
   [SerializeField] private GameObject howToPlay;


   private void Awake()
   {
       howToPlay.SetActive(false);
   }


   private void OnTriggerEnter2D(Collider2D c)
   {
       if (!c.gameObject.CompareTag("Player")) return;
       howToPlay.SetActive(true);
   }

   private void OnTriggerExit2D(Collider2D c)
   {
       if (!c.gameObject.CompareTag("Player")) return;
       howToPlay.SetActive(false);
   }
}
