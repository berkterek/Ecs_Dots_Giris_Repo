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
        public void OnCreate(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;
            foreach (var (localTransformRW, moveDataRO) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<MoveData>>())
            {
                var direction = new float3(0f, 1f, 0f);
                localTransformRW.ValueRW.Position += deltaTime * moveDataRO.ValueRO.MoveSpeed * direction;
            }
        }
    }
}