﻿using SpaceShipEcsDots.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace SpaceShipEcsDots.Systems
{
    [BurstCompile]
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateBefore(typeof(TransformSystemGroup))]
    public partial struct EnemyMoveSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;

            new EnemyMoveJob()
            {
                DeltaTime = deltaTime
            }.ScheduleParallel();
        }
    }

    [BurstCompile]
    public partial struct EnemyMoveJob : IJobEntity
    {
        public float DeltaTime;

        [BurstCompile]
        private void Execute(Entity entity, ref LocalTransform localTransform, ref EnemyMoveData enemyMoveData,
            in EnemyMoveTargetData enemyMoveTargetData)
        {
            if (enemyMoveData.CanPassNextTarget) return;
            
            if (math.distance(enemyMoveTargetData.Target, localTransform.Position) < 0.1f)
            {
                enemyMoveData.CanPassNextTarget = true;
                return;
            }

            var direction = math.normalize(enemyMoveTargetData.Target - localTransform.Position);

            localTransform.Position += DeltaTime * enemyMoveData.MoveSpeed * direction;
        }
    }
}