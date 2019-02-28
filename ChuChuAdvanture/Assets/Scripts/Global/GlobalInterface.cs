namespace ChuChu
{
    public interface IManageableResource
    {
        EObjectType ObjectTag { get; }
        void Deploy();
    }
}

