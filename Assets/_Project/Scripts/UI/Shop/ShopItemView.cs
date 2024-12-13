using Project.Configs.UI;
using Project.Systems.Shop.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI.Shop
{
    public class ShopItemView : MonoBehaviour
    {
        [SerializeField] private Image _itemImage;
        [SerializeField] private TMP_Text _itemAmount;
        [SerializeField] private Image _priceImage;
        [SerializeField] private TMP_Text _priceAmount;

        [SerializeField] private Sprite _background;
        [SerializeField] private Image _cover;

        public void Set(ShopItem item, ItemViewBackgroundConfig config = null)
        {
            if (config != null)
            {
                _background = config.Background;
            }

            _itemImage.sprite = item.Sprite;
            _itemAmount.text = item.AmountText;

            _priceAmount.text = item.PriceAmountText;

            _priceImage.sprite = item.PriceSprite;
            _priceImage.gameObject.SetActive(item.PriceSprite != null);
        }
    }
}