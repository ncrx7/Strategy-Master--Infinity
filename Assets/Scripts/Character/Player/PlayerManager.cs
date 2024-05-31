using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : CharacterManager
{
    

    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("" + other.gameObject.name);
    }
}
