using SpaceShipEcsDots.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace SpaceShipEcsDots.Systems
{
    [BurstCompile]
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateBefore(typeof(TransformSystemGroup))]
    public partial struct PlayerMoveSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;
            new PlayerMoveJob()
            {
                DeltaTime = deltaTime
            }.ScheduleParallel();
        }
    }

    [BurstCompile]
    public partial struct PlayerMoveJob : IJobEntity
    {
        public float DeltaTime;
        
        [BurstCompile]
        private void Execute(Entity entity, ref LocalTransform localTransform, in MoveData moveData, in InputData input)
        {
            float3 direction = input.Direction;
            localTransform.Position += DeltaTime * moveData.MoveSpeed * direction;
        }
    }
}