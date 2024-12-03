﻿using Project.Configs.ShopItems;
using Project.SDK.Advertisment;
using YG.Utils.Pay;

namespace Project.Systems.Shop.Items
{
    public class AddRemovalItem : InAppItem
    {
        private readonly AdvertismentController _controller;

        public AddRemovalItem(AdvertismentController controller, AddRemovalConfig config, Purchase itemData)
            : base(config, itemData)
        {
            _controller = controller;
        }

        public override bool IsAvaliable => _controller.IsAddHided == false;

        public override string AmountText => string.Empty;

        public override void Get()
        {
            _controller.RemoveAd();
        }
    }
}