using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableManager : MonoBehaviour
{
    [SerializeField] private AssetReferenceGameObject _assetReferenceGameObject;
    [SerializeField] private AssetLabelReference _assetLabelReference;
    private GameObject _gameObjectReference;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            LoadAssets();
        }
        else if(Input.GetKeyDown(KeyCode.Y))
        {
            RelaseAssets();
            Debug.Log("pressed y");
        }
    }

    //ASSET LOAD METHOD 4 - DIRECTLY INSTANTIATE
      private void LoadAssets()
    {
        _assetReferenceGameObject.InstantiateAsync().Completed +=
        (asyncOperationHandler) =>
        {
            _gameObjectReference = asyncOperationHandler.Result;
        };
    }  

         //ASSET LOAD METHOD 3 - WITH LABEL
/*          private void LoadAssets()
        {
            Addressables.LoadAssetAsync<GameObject>(_assetLabelReference).Completed +=
            (asyncOperationHandler) =>
            {
                if (asyncOperationHandler.Status == AsyncOperationStatus.Succeeded)
                {
                    Instantiate(asyncOperationHandler.Result);
                }
                else
                {
                    Debug.Log("FAILED TO LOAD ASSETS!!");
                }
            };
        }   */

/*              //ASSET LOAD METHOD 2 - WITH ASSET REFERENCE
            private void LoadAssets()
            {
                _assetReferenceGameObject.LoadAssetAsync<GameObject>().Completed +=
                (asyncOperationHandler) =>
                {
                    if (asyncOperationHandler.Status == AsyncOperationStatus.Succeeded)
                    {
                        Instantiate(asyncOperationHandler.Result);
                    }
                    else
                    {
                        Debug.Log("FAILED TO LOAD ASSETS!!");
                    }
                };
            }  */

    //ASSET LOAD METHOD 1 - WITH PATH
    /*     private void LoadAssets()
        {
            Addressables.LoadAssetAsync<GameObject>("Assets/Scripts/Tutorial/Addressables/Prefabs/TestObj.prefab").Completed +=
            (asyncOperationHandler) =>
            {
                if (asyncOperationHandler.Status == AsyncOperationStatus.Succeeded)
                {
                    Instantiate(asyncOperationHandler.Result);
                }
                else
                {
                    Debug.Log("FAILED TO LOAD ASSETS!!");
                }
            };
        } */


    private void RelaseAssets()
    {
        //_assetReferenceGameObject.ReleaseInstance(_gameObjectReference);
        _assetReferenceGameObject.ReleaseAsset();
    }
}
