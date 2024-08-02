using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDistanceManager : MonoBehaviour
{
    [SerializeField] private LayerMask _interactableLayer;
    [SerializeField] private UnitCharacterManager _unitCharacterManagerOwner;
    private UnitCharacterManager _unitCharacterManagerRay;
    public float FriendUnitDistance { get; private set; }
    public float OpposingUnitDistance { get; private set; }

    private void OnEnable()
    {
        Ticker.OnTickAction += HandleUnitCharacterBehaviourTicked;
    }

    private void OnDisable()
    {
        Ticker.OnTickAction -= HandleUnitCharacterBehaviourTicked;
    }

    private void Update()
    {
        //Debug.Log($"Friend Unit: {FriendUnitDistance}\n Opposing Unit: {OpposingUnitDistance}");
    }

    private void HandleUnitCharacterBehaviourTicked()
    {
        Debug.Log("ticking...");
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.green);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, _interactableLayer))
        {
            //BehaveUnitCharacter(hit);
            BehaveUnitCharacter(hit);
        }
        else
        {
            FriendUnitDistance = -1;
            OpposingUnitDistance = -1;
        }
    }

/*     private void BehaveUnitCharacter(RaycastHit hit)
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
                            //_unitCharacterManagerOwner.ChangeState(new UnitCharacterIdleState());
                        }
                        FriendUnitDistance = distanceBetweenTarget;
                    }
                    else if (_unitCharacterManagerOwner.characterOwnerType == CharacterOwnerType.ENEMY_UNIT)
                    {
                        if (distanceBetweenTarget < 2) //minimum distance between enemy-player unit, unit attack range
                        {
                            //SWTICH TO ATTACK STATE
                            //_unitCharacterManagerOwner.ChangeState(new UnitCharacterAttackState());
                        }
                        OpposingUnitDistance = distanceBetweenTarget;
                    }
                    break;
                case CharacterOwnerType.ENEMY_UNIT:
                    Debug.Log("The object in front of me is the enemy unit");
                    if (_unitCharacterManagerOwner.characterOwnerType == CharacterOwnerType.PLAYER_UNIT)
                    {
                        if (distanceBetweenTarget < 2) //minimum distance between player-enemy unit, unit attack range
                        {
                            //SWTICH TO ATTACK STATE
                            //_unitCharacterManagerOwner.ChangeState(new UnitCharacterAttackState());
                        }
                        OpposingUnitDistance = distanceBetweenTarget;
                    }
                    else if (_unitCharacterManagerOwner.characterOwnerType == CharacterOwnerType.ENEMY_UNIT)
                    {
                        if (distanceBetweenTarget < 3) // minimum distance between enemy-enemy friend unit
                        {
                            //SWITCH TO IDLE STATE
                            //_unitCharacterManagerOwner.ChangeState(new UnitCharacterIdleState());
                        }
                        FriendUnitDistance = distanceBetweenTarget;
                    }
                    break;
                default:
                    Debug.LogWarning("Unknows Unit Owner!!");
                    break;
            }
        }
    } */

    private void BehaveUnitCharacter(RaycastHit hit)
    {
        if (hit.collider.TryGetComponent<UnitCharacterManager>(out _unitCharacterManagerRay))
        {
            Debug.Log("test if");
            if (_unitCharacterManagerRay.characterOwnerType == _unitCharacterManagerOwner.characterOwnerType)
            {
                FriendUnitDistance = CalculateDistance(hit.collider.transform);
                OpposingUnitDistance = -1;
            }
            else if (_unitCharacterManagerRay.characterOwnerType != _unitCharacterManagerOwner.characterOwnerType)
            {
                OpposingUnitDistance = CalculateDistance(hit.collider.transform);
                FriendUnitDistance = -1;
            }
            else
            {
                Debug.Log("Unauthorized owner type!!!");
            }
        }
    }

    private float CalculateDistance(Transform target)
    {
        return (transform.position - target.transform.position).sqrMagnitude;
    }
}
