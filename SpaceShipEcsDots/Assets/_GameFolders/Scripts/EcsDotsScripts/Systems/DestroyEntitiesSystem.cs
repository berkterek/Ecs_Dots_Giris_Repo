using SpaceShipEcsDots.Components;
using Unity.Burst;
using Unity.Entities;

namespace SpaceShipEcsDots.Systems
{
    [BurstCompile]
    [UpdateInGroup(typeof(SimulationSystemGroup), OrderLast = true)]
    public partial struct DestroyEntitiesSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EndSimulationEntityCommandBufferSystem.Singleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var entityCommandBuffer = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();

            new DestroyEntitiesJob()
            {
                MyEntityCommandBuffer = entityCommandBuffer.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter()
            }.ScheduleParallel();
        }
    }

    [BurstCompile]
    public partial struct DestroyEntitiesJob : IJobEntity
    {
        public EntityCommandBuffer.ParallelWriter MyEntityCommandBuffer;

        [BurstCompile]
        private void Execute(Entity entity, in DestroyData destroyData, [ChunkIndexInQuery] int sortKey)
        {
            if (destroyData.IsDestroy)
            {
                MyEntityCommandBuffer.DestroyEntity(sortKey, entity);
            }
        }
    }
}