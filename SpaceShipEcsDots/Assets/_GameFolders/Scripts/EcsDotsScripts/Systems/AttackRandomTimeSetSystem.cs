using SpaceShipEcsDots.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;

namespace SpaceShipEcsDots.Systems
{
    [BurstCompile]
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateAfter(typeof(AttackSystem))]
    public partial struct AttackRandomTimeSetSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var elapsedTime = SystemAPI.Time.ElapsedTime;
            
            new AttackRandomTimeSetJob()
            {
                
            }.ScheduleParallel();
        }
    }

    [BurstCompile]
    public partial struct AttackRandomTimeSetJob : IJobEntity
    {
        public float ElapsedTime;

        [BurstCompile]
        private void Execute(Entity entity, ref AttackData attackData, in AttackRandomTimeData attackRandomTimeData, [ChunkIndexInQuery]int sortKey)
        {
            if (attackData.CanChange)
            {
                attackData.CanChange = false;
                uint seedNumber = (uint)math.abs(ElapsedTime);
                attackData.MaxFireTime = Random.CreateFromIndex(seedNumber).NextFloat(attackRandomTimeData.MinFireRandomTime,
                    attackRandomTimeData.MaxFireRandomTime);
            }
        }
    }
}