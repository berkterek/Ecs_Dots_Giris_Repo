using SpaceShipEcsDots.Components;
using Unity.Entities;

namespace SpaceShipEcsDots.Aspects
{
    public readonly partial struct ProjectileSelfDestroyAspect : IAspect
    {
        public readonly Entity Entity;
        readonly RefRW<ProjectileSelfDestroyData> _projectileSelfDestroyDataRW;

        public bool CanDestroy => _projectileSelfDestroyDataRW.ValueRO.CanDestroy;

        public void BackCountingProcess(float deltaTime)
        {
            _projectileSelfDestroyDataRW.ValueRW.CurrentLifeTime += deltaTime;

            if (_projectileSelfDestroyDataRW.ValueRO.CurrentLifeTime > _projectileSelfDestroyDataRW.ValueRO.MaxLifeTime)
            {
                _projectileSelfDestroyDataRW.ValueRW.CanDestroy = true;
            }
        }
    }
}