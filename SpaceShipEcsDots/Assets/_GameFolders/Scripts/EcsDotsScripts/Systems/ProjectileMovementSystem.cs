﻿using SpaceShipEcsDots.Aspects;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace SpaceShipEcsDots.Systems
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateBefore(typeof(TransformSystemGroup))]
    [UpdateAfter(typeof(PlayerMoveSystem))]
    [BurstCompile]
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

    [BurstCompile]
    public partial struct ProjectileMovementJob : IJobEntity
    {
        public float DeltaTime;

        [BurstCompile]
        private void Execute(ProjectileMovementAspect projectileMovementAspect)
        {
           projectileMovementAspect.MoveProcess(DeltaTime);
        }
    }
}