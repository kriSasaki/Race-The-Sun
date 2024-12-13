using UnityEngine;
using UnityEngine.UI;

namespace Project.Configs.UI
{
    [CreateAssetMenu(fileName = "ItemViewBackground", menuName = "Configs/UI/ItemViewBackground")]
    public class ItemViewBackgroundConfig : ScriptableObject
    {
        [field: SerializeField] public Sprite Background { get; private set; }
    }
}