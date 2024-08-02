using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDistanceManager : MonoBehaviour
{
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private UnitCharacterManager _unitCharacterManagerOwner;
    private UnitCharacterManager _unitCharacterManagerRay;
    private float _distanceHolder = 0f;

    private void OnEnable()
    {
        Ticker.OnTickAction += HandleUnitCharacterBehaviourTicked;
    }

    private void OnDisable()
    {
        Ticker.OnTickAction -= HandleUnitCharacterBehaviourTicked;
    }

    private void HandleUnitCharacterBehaviourTicked()
    {
        Debug.Log("ticking...");
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.green);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, interactableLayer))
        {
            BehaveUnitCharacter(hit);
        }
    }

    private void BehaveUnitCharacter(RaycastHit hit)
    {
        float distanceBetweenTarget = CalculateDistance(hit.collider.transform);
        Debug.Log("raycast inside if : " + hit.collider.gameObject.name);

        if (hit.collider.TryGetComponent<UnitCharacterManager>(out _unitCharacterManagerRay))
        {
            switch (_unitCharacterManagerRay.characterOwnerType)
            {
                case CharacterOwnerType.PLAYER_UNIT:
                    if (_unitCharacterManagerOwner.characterOwnerType == CharacterOwnerType.PLAYER_UNIT)
                    {
                        if (distanceBetweenTarget < 3) // minimum distance between player-player friend unit 
                        {
                            //SWITCH TO IDLE STATE
                            _unitCharacterManagerOwner.ChangeState(new UnitCharacterIdleState());
                        }
                    }
                    else if (_unitCharacterManagerOwner.characterOwnerType == CharacterOwnerType.ENEMY_UNIT)
                    {
                        if (distanceBetweenTarget < 2) //minimum distance between enemy-player unit, unit attack range
                        {
                            //SWTICH TO ATTACK STATE
                            _unitCharacterManagerOwner.ChangeState(new UnitCharacterAttackState());
                        }
                    }
                    break;
                case CharacterOwnerType.ENEMY_UNIT:
                    Debug.Log("The object in front of me is the enemy unit");
                    if (_unitCharacterManagerOwner.characterOwnerType == CharacterOwnerType.PLAYER_UNIT)
                    {
                        if (distanceBetweenTarget < 2) //minimum distance between player-enemy unit, unit attack range
                        {
                            //SWTICH TO ATTACK STATE
                            _unitCharacterManagerOwner.ChangeState(new UnitCharacterAttackState());
                        }
                    }
                    else if (_unitCharacterManagerOwner.characterOwnerType == CharacterOwnerType.ENEMY_UNIT)
                    {
                        if (distanceBetweenTarget < 3) // minimum distance between enemy-enemy friend unit
                        {
                            //SWITCH TO IDLE STATE
                            _unitCharacterManagerOwner.ChangeState(new UnitCharacterIdleState());
                        }
                    }
                    break;
                default:
                    Debug.LogWarning("Unknows Unit Owner!!");
                    break;
            }
        }
    }

    private float CalculateDistance(Transform target)
    {
        return (transform.position - target.transform.position).sqrMagnitude;
    }
}
