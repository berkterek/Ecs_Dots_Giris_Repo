using SpaceShipEcsDots.Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SpaceShipEcsDots.Authorings
{
    public class EnemySpawnerAuthoring : MonoBehaviour
    {
        public GameObject Prefab;
        public float MinTime = 1f;
        public float MaxTime = 5f;
        public Transform[] Points;
    }
    
    public class EnemySpawnerBaker : Baker<EnemySpawnerAuthoring>
    {
        public override void Bake(EnemySpawnerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            
            AddComponent(entity, new EnemySpawnData()
            {
                Entity = GetEntity(authoring.Prefab,TransformUsageFlags.Dynamic),
                MinTime = authoring.MinTime,
                MaxTime = authoring.MaxTime,
                CurrentTime = 0f,
                RandomTime = Random.Range(authoring.MinTime,authoring.MaxTime)
            });

            var blobBuilder = new BlobBuilder(Allocator.Temp);
            ref var root = ref blobBuilder.ConstructRoot<EnemySpawnPositionBlob>();
            var array = blobBuilder.Allocate(ref root.Values, authoring.Points.Length);

            for (int i = 0; i < array.Length; i++)
            {
                var position = (float3)authoring.Points[i].position;
                array[i] = position;
            }

            BlobAssetReference<EnemySpawnPositionBlob> blobAssetReference =
                blobBuilder.CreateBlobAssetReference<EnemySpawnPositionBlob>(Allocator.Persistent);
            
            AddComponent(entity, new EnemySpawnPositionsReference()
            {
                BlobValueReference = blobAssetReference
            });
            
            blobBuilder.Dispose();
        }
    }
}


