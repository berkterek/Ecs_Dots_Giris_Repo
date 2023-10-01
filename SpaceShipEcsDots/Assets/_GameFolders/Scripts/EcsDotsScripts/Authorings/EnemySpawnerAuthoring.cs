using SpaceShipEcsDots.Components;
using Unity.Entities;
using UnityEngine;

namespace SpaceShipEcsDots.Authorings
{
    public class EnemySpawnerAuthoring : MonoBehaviour
    {
        public GameObject Prefab;
        public float MinTime = 1f;
        public float MaxTime = 5f;
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
        }
    }
}


