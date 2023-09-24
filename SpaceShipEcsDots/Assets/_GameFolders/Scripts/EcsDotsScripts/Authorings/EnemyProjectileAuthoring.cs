﻿using SpaceShipEcsDots.Components;
using Unity.Entities;
using UnityEngine;

namespace SpaceShipEcsDots.Authorings
{
    public class EnemyProjectileAuthoring : MonoBehaviour
    {
        public float MoveSpeed;
        public float Direction;
    }
    
    public class EnemyProjectileBaker : Baker<EnemyProjectileAuthoring>
    {
        public override void Bake(EnemyProjectileAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            
            AddComponent<EnemyProjectileTag>(entity);
            
            AddComponent(entity, new ProjectileMoveData()
            {
                MoveSpeed = authoring.MoveSpeed,
                Direction = authoring.Direction
            });
        }
    }
}