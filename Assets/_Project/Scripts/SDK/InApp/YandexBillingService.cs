using System;
using Project.Interfaces.SDK;
using YG;
using YG.Utils.Pay;

namespace Project.SDK.InApp
{
    public class YandexBillingService : IBillingService
    {
        public void LoadProductCatalog(Action<Purchase[]> onLoadCallback)
        {
            onLoadCallback(YandexGame.purchases);
        }
    }
}