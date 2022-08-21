namespace CustomEcsBase.Components.Interfaces
{
    public interface ICEcsComponent
    {
        int EntityId { get; }
        void Init(int entityId);
        void Reset();
    }
}