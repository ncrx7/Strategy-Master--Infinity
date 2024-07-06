using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavmeshManager : MonoBehaviour
{
    [SerializeField] NavMeshAgent _navmeshAgent;

    public NavMeshAgent GetNavMeshAgent()
    {
        return _navmeshAgent;
    }

    public void ActivateNavmeshAgent()
    {
        _navmeshAgent.enabled = true;
    }

    public void DisableNavmeshAgent()
    {
        _navmeshAgent.enabled = false;
    }
}
