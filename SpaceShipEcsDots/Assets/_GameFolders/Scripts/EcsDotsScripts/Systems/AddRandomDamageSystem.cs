using SpaceShipEcsDots.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;

namespace SpaceShipEcsDots.Systems
{
    [BurstCompile]
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    public partial struct AddRandomDamageSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginSimulationEntityCommandBufferSystem.Singleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var elapsedTime = SystemAPI.Time.ElapsedTime;
            var entityCommandBuffer = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            
            new AddRandomDamageJob()
            {
                ElapsedTime = elapsedTime,
                MyEntityCommandBuffer = entityCommandBuffer.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter()
            }.ScheduleParallel();
        }
    }

    [BurstCompile]
    public partial struct AddRandomDamageJob : IJobEntity
    {
        public EntityCommandBuffer.ParallelWriter MyEntityCommandBuffer;
        public double ElapsedTime;
        
        [BurstCompile]
        private void Execute(Entity entity, in DamageRandomData damageRandomData, ref DamageData damageData, [ChunkIndexInQuery] int sortKey)
        {
            uint seed = (uint)ElapsedTime * (uint)entity.Index;
            var randomDamageData = Random.CreateFromIndex(seed).NextFloat(damageRandomData.MinDamage, damageRandomData.MaxDamage);
            damageData.Damage = randomDamageData;
            
            MyEntityCommandBuffer.SetComponentEnabled<DamageRandomData>(sortKey, entity, false);
        }
    }
}