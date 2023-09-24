using SpaceShipEcsDots.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace SpaceShipEcsDots.Aspects
{
    public readonly partial struct ProjectileMovementAspect : IAspect
    {
        public readonly Entity Entity;
        readonly RefRW<LocalTransform> _localTransformRW;
        readonly RefRO<ProjectileMoveData> _projectileMoveRO;

        public void MoveProcess(float deltaTime)
        {
            float3 direction = new float3(0f, _projectileMoveRO.ValueRO.Direction, 0f);
            _localTransformRW.ValueRW.Position += deltaTime * _projectileMoveRO.ValueRO.MoveSpeed * direction;
        }
    }
}