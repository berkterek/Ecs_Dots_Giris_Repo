using Unity.Entities;
using Unity.Mathematics;

namespace SpaceShipEcsDots.Components
{
    public struct EnemySpawnPositionsReference : IComponentData
    {
        public BlobAssetReference<EnemySpawnPositionBlob> BlobValueReference;
    }

    public struct EnemySpawnPositionBlob
    {
        public BlobArray<float3> Values;
    }
}