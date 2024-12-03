using System;
using YG.Utils.Pay;

namespace Project.Interfaces.SDK
{
    public interface IBillingService
    {
        void LoadProductCatalog(Action<Purchase[]> onLoadCallback);
    }
}