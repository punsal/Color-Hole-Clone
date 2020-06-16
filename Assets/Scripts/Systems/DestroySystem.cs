using Components.Tags;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;

namespace Systems
{
    public class DestroySystem : JobComponentSystem
    {
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var entityCommandBuffer = new EntityCommandBuffer(Allocator.Temp);
            
            Entities.WithAll<DeleteTag>().ForEach((Entity entity) =>
            {
                // ReSharper disable once AccessToDisposedClosure
                entityCommandBuffer.DestroyEntity(entity);
            }).WithoutBurst().Run();
            
            entityCommandBuffer.Playback(EntityManager);
            entityCommandBuffer.Dispose();

            return default;
        }
    }
}
