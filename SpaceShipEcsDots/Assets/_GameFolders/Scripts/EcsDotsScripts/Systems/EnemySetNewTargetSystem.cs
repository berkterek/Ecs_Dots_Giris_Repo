using SpaceShipEcsDots.Components;
using Unity.Burst;
using Unity.Entities;

namespace SpaceShipEcsDots.Systems
{
    [BurstCompile]
    [UpdateAfter(typeof(EnemyMoveSystem))]
    public partial struct EnemySetNewTargetSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            new EnemySetNewTargetJob()
            {

            }.ScheduleParallel();
        }
    }

    [BurstCompile]
    public partial struct EnemySetNewTargetJob : IJobEntity
    {
        [BurstCompile]
        private void Execute(Entity entity, ref EnemyMoveData enemyMoveData, ref EnemyMoveTargetData enemyMoveTargetData, in EnemyPathData enemyPathData)
        {
            if (!enemyMoveData.CanPassNextTarget) return;
            
            enemyMoveTargetData.NextTargetIndex++;
            
            if (enemyMoveTargetData.NextTargetIndex >= enemyMoveTargetData.MaxTargetIndex)
            {
                //Destroy
            }
            else
            {
                enemyMoveTargetData.Target = enemyPathData.BlobValueReference.Value.Values[enemyMoveTargetData.NextTargetIndex];
                enemyMoveData.CanPassNextTarget = false;
            }
        }
    }
}