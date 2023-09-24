using SpaceShipEcsDots.Components;
using Unity.Entities;
using UnityEngine;

namespace SpaceShipEcsDots.Authorings
{
    public class EnemyAuthoring : MonoBehaviour
    {
        public float MoveSpeed = 3f;
    }

    public class EnemyBaker : Baker<EnemyAuthoring>
    {
        public override void Bake(EnemyAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            
            AddComponent<EnemyMoveTargetData>(entity);
            AddComponent<EnemyTag>(entity);
            
            AddComponent(entity, new EnemyMoveData()
            {
                CanPassNextTarget = false,
                MoveSpeed = authoring.MoveSpeed
            });
        }
    }
}