
using System;
using UnityEngine;
using UnityEngine.AI;

public class test : MonoBehaviour
{
   [SerializeField] private Transform target;
   private NavMeshAgent NavMeshAgent;

   private void Awake()
   {
      NavMeshAgent = GetComponent<NavMeshAgent>();
      NavMeshAgent.updateRotation = false;
      NavMeshAgent.updateUpAxis = false;
   }

   private void Update()
   {
      NavMeshAgent.SetDestination(target.position);
   }
}
