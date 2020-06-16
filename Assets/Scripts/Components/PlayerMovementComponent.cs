using Unity.Entities;
using Unity.Mathematics;

namespace Components
{
    [GenerateAuthoringComponent]
    public struct PlayerMovementComponent : IComponentData
    {
        public float2 direction;
    }
}
