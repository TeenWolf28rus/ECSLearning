using CustomEcsBase.Components.Pool;

namespace CustomEcsBase.Components.Interfaces
{
    public interface IEcsComponentsManager
    {
        EcsComponentsPool<T> GetPool<T>() where T : IEcsComponent;
        bool HasPool<T>() where T : IEcsComponent;
        void Dispose();
    }
}