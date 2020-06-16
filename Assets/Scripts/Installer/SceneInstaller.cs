using Player.Data;
using UnityEngine;
using Zenject;

namespace Installer
{
    public class SceneInstaller : MonoInstaller
    {
        #pragma warning disable 649
        [SerializeField] private PlayerData playerData;
        #pragma warning restore 649
        
        public override void InstallBindings()
        {
            Container.Bind<PlayerData>().FromInstance(playerData).AsSingle();
        }
    }
}