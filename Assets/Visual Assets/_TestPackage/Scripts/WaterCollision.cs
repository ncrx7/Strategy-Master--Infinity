using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "TargetObject":
                GameManager.Instance.sinkcounter++;
                GameManager.Instance.UpdateScore();
                Destroy(other.gameObject, 0.5f);
                break;
        
            case "Man":
                //Destroy(other.gameObject, 0.5f);
                break;
            default:
                break;
        }
    }
}
