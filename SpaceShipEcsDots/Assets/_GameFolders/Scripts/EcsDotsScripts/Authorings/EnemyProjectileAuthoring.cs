using SpaceShipEcsDots.Components;
using Unity.Entities;
using UnityEngine;

namespace SpaceShipEcsDots.Authorings
{
    public class EnemyProjectileAuthoring : MonoBehaviour
    {
        public float MoveSpeed;
        public float Direction;
        public float MaxLifeTime = 10f;
        public float MaxDamage = 10f;
        public float MinDamage = 2f;
    }
    
    public class EnemyProjectileBaker : Baker<EnemyProjectileAuthoring>
    {
        public override void Bake(EnemyProjectileAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            
            AddComponent<EnemyProjectileTag>(entity);
            AddComponent<DamageData>(entity);
            
            AddComponent(entity, new LaserSoundData()
            {
                IsPlayer = false
            });

            AddComponent(entity, new DamageRandomData()
            {
                MinDamage = authoring.MinDamage,
                MaxDamage = authoring.MaxDamage
            });
            
            AddComponent(entity, new ProjectileMoveData()
            {
                MoveSpeed = authoring.MoveSpeed,
                Direction = authoring.Direction
            });
            
            AddComponent(entity, new ProjectileSelfDestroyData()
            {
                MaxLifeTime = authoring.MaxLifeTime,
                CanDestroy = false,
                CurrentLifeTime = 0f
            });
        }
    }
}