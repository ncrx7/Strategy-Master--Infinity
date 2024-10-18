using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SceneReferenceInstallers : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<UnitCharacterSpawnOnSceneManager>().FromComponentInHierarchy().AsSingle();

        //Container.BindFactory<UnitCharacterSpawnOnSceneManager, UnitCharacterSpawnOnSceneManagerFactory>().FromComponentInHierarchy();
    }
}
