using SpaceShipEcsDots.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace SpaceShipEcsDots.Systems
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateBefore(typeof(TransformSystemGroup))]
    [UpdateAfter(typeof(PlayerMoveSystem))]
    public partial struct ProjectileMovementSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;

            new ProjectileMovementJob()
            {
                DeltaTime = deltaTime
            }.ScheduleParallel();
        }
    }

    public partial struct ProjectileMovementJob : IJobEntity
    {
        public float DeltaTime;

        private void Execute(Entity entity, ref LocalTransform localTransform, in ProjectileMoveData projectileMoveData)
        {
            float3 direction = new float3(0f, projectileMoveData.Direction, 0f);
            localTransform.Position += DeltaTime * projectileMoveData.MoveSpeed * direction;
        }
    }
}