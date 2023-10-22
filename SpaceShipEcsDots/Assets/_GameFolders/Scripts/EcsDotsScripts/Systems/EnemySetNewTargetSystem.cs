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
            foreach (var (spawnDataRO, spawnPositionDataRO) in SystemAPI.Query<RefRO<EnemySpawnData>, RefRO<EnemySpawnPositionsReference>>())
            {
                foreach (var (enemyMoveDataRW, enemyMoveTargetDataRW) in SystemAPI.Query<RefRW<EnemyMoveData>, RefRW<EnemyMoveTargetData>>())
                {
                    if(!enemyMoveDataRW.ValueRO.CanPassNextTarget) continue;

                    enemyMoveDataRW.ValueRW.CanPassNextTarget = false;
                    enemyMoveTargetDataRW.ValueRW.NextTargetIndex++;

                    if (enemyMoveTargetDataRW.ValueRO.NextTargetIndex >= enemyMoveTargetDataRW.ValueRO.MaxTargetIndex)
                    {
                        //Destroy
                    }
                    else
                    {
                        enemyMoveTargetDataRW.ValueRW.Target =
                            spawnPositionDataRO.ValueRO.BlobValueReference.Value.Values[
                                enemyMoveTargetDataRW.ValueRW.NextTargetIndex];
                    }
                }
            }
        }
    }
}