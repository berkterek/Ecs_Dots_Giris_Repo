using SpaceShipEcsDots.Components;
using Unity.Entities;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace SpaceShipEcsDots.Authorings
{
    public class PlayerAuthoring : MonoBehaviour
    {
        public float MoveSpeed = 5f;
    }
    
    public class PlayerBaker : Baker<PlayerAuthoring>
    {
        public override void Bake(PlayerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new MoveData()
            {
                MoveSpeed = authoring.MoveSpeed
            });

            AddComponent<InputData>(entity);
            AddComponent<PlayerTag>(entity);
        }
    }
}