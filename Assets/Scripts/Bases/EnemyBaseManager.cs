using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseManager : BaseManager
{
    [SerializeField] private PlayerStatManager _playerStatManager;
    [SerializeField] private Collider _collider;

    public override void Start()
    {
        base.Start();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamage>(out IDamage bullet))
        {
            bullet.DealDamage(ref _currentBaseHealth, (int)_playerStatManager.GetPlayerFixedStatValue(StatType.PF));

            UpdateBaseHealthText(_currentBaseHealth);
            Debug.Log("enemy base ddamage dealed");

            if (CheckBaseHealth())
            {
                //OnBoxHealtRunnedOut();
                _collider.enabled = false;
            }
        }
    }
}
