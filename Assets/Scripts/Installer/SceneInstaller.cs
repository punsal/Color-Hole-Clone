using Player.Data;
using UnityEngine;
using Zenject;

namespace Installer
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private PlayerData playerData;
    
        public override void InstallBindings()
        {
            Container.Bind<PlayerData>().FromInstance(playerData).AsSingle();
        }
    }
}