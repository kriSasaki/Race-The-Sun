namespace Project.Interfaces.Data
{
    public interface IAdvertismentData : IAdvertismentStatus, ISaveable
    {
        void RemoveAd();
    }
}