using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class UniTaskExample1 : MonoBehaviour
{
    [SerializeField] private Transform[] _targetsToMove;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            //DisplayLogs();
            MoveAllTarget();
            //MoveTarget(_targetsToMove[0]);
        }
    }
    //LOG EXAMPLE
    private async void DisplayLogs()
    {
        await DisplayText("merhaba");
        await DisplayText("Selam");
        await DisplayText("hi");
    }

    private async UniTask DisplayText(string text)
    {
        Debug.Log("Log is: " + text);
        await UniTask.Delay(2000);
    }
    //END LOG EXAMPLE

    //MOVE CUBE EXAMPLE
    private async void MoveAllTarget()
    {
       foreach (Transform transform in _targetsToMove)
       {
            await MoveTarget(transform);
       }
    }

    private async UniTask MoveTarget(Transform targetTransformToMove)
    {
        Vector3 targetPoint = new Vector3(targetTransformToMove.position.x -5 , targetTransformToMove.position.y, targetTransformToMove.position.z);

        while (targetTransformToMove.position != targetPoint)
        {
            targetTransformToMove.Translate(new Vector3(-1, 0, 0));
            //await UniTask.Yield();
            await UniTask.Delay(1000);
        }
    }
    //END MOVE CUBE EXAMPLE

}
