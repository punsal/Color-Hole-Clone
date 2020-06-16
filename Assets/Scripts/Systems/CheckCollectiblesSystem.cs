using Components.Tags;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;

namespace Systems
{
    public class CheckCollectiblesSystem : JobComponentSystem
    {
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var entityCommandBuffer = new EntityCommandBuffer(Allocator.Temp);
            
            Entities.WithAll<CollectibleTag>().ForEach((ref Entity entity, in Translation position) =>
            {
                if (position.Value.y <= -2f)
                {
                    // ReSharper disable once AccessToDisposedClosure
                    entityCommandBuffer.AddComponent(entity, new DeleteTag());
                }
            }).Run();
            
            entityCommandBuffer.Playback(EntityManager);
            entityCommandBuffer.Dispose();

            return default;
        }
    }
}
