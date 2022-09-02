namespace CustomEcsBase.Components.Interfaces
{
    public interface IEcsComponent
    {
        int EntityId { get; }
        void Init(int entityId);
        void Reset();
    }
}