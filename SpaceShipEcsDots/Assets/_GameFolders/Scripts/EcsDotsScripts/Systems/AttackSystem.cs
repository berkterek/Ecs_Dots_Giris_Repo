using SpaceShipEcsDots.Components;
using Unity.Burst;
using Unity.Entities;

namespace SpaceShipEcsDots.Systems
{
    [BurstCompile]
    public partial struct AttackSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginSimulationEntityCommandBufferSystem.Singleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var entityCommandBuffer = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            var deltaTime = SystemAPI.Time.DeltaTime;

            new AttackJob()
            {
                DeltaTime = deltaTime,
                MyEntityCommandBuffer = entityCommandBuffer.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter()
            }.ScheduleParallel();
        }
    }

    [BurstCompile]
    public partial struct AttackJob : IJobEntity
    {
        public float DeltaTime;
        public EntityCommandBuffer.ParallelWriter MyEntityCommandBuffer;

        [BurstCompile]
        private void Execute(Entity entity, ref AttackData attackData, [ChunkIndexInQuery]int sortKey)
        {
            attackData.CurrentFireTime += DeltaTime;
            if (attackData.CurrentFireTime > attackData.MaxFireTime)
            {
                attackData.CurrentFireTime = 0f;
                var projectileEntity = MyEntityCommandBuffer.Instantiate(sortKey, attackData.Projectile);
            }
        }
    }
}