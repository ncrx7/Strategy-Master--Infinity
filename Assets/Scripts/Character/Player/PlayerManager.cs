using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : CharacterManager
{
    [SerializeField] private PlayerStatManager _playerStatManager;

    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ICollectable>(out ICollectable collectableObject))
        {
            collectableObject.Collect(_playerStatManager);
            collectableObject.PlaySoundEffect();
        }
    }
}
