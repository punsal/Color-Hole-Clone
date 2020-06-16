using Components;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Systems
{
    public class MovementSystem : JobComponentSystem
    {
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var direction = new float2(
                Input.GetAxis("Horizontal"),
                Input.GetAxis("Vertical"));
            
            Entities.WithAll<PlayerMovementComponent>().ForEach((ref PlayerMovementComponent playerMovementComponent) =>
            {
                playerMovementComponent.direction = direction;
            }).Run();
            
            return default;
        }
    }
}
