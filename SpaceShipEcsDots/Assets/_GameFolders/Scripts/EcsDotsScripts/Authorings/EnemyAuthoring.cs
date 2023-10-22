using SpaceShipEcsDots.Components;
using Unity.Entities;
using UnityEngine;

namespace SpaceShipEcsDots.Authorings
{
    public class EnemyAuthoring : MonoBehaviour
    {
        public GameObject ProjectilePrefab;
        public float MoveSpeed = 3f;
        public float MaxFireRandomTime = 4f;
        public float MinFireRandomTime = 1f;
    }

    public class EnemyBaker : Baker<EnemyAuthoring>
    {
        public override void Bake(EnemyAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            
            AddComponent<EnemyMoveTargetData>(entity);
            AddComponent<EnemyTag>(entity);

            AddComponent(entity, new AttackData()
            {
                MaxFireTime = Random.Range(authoring.MinFireRandomTime,authoring.MaxFireRandomTime),
                Projectile = GetEntity(authoring.ProjectilePrefab,TransformUsageFlags.Dynamic),
                CurrentFireTime = 0f
            });
            
            AddComponent(entity, new AttackRandomTimeData()
            {
                MaxFireRandomTime = authoring.MaxFireRandomTime,
                MinFireRandomTime = authoring.MinFireRandomTime
            });
            
            AddComponent(entity, new EnemyMoveData()
            {
                CanPassNextTarget = false,
                MoveSpeed = authoring.MoveSpeed
            });
        }
    }
}