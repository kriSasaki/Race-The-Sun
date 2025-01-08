using Project.Configs.Stats;
using Project.Players.View;
using UnityEngine;

namespace Project.Configs.Ships
{
    [CreateAssetMenu(fileName = "ShipConfig", menuName = "Configs/ShipConfig", order = 1)]
    public class ShipConfig : ScriptableObject
    {
        [field: SerializeField] public string ID { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public int Cost { get; private set; }
        [field: SerializeField] public PlayerView PlayerView { get; private set; }
        [field: SerializeField] public StatsSheet  StatsConfig { get; private set; }
        [field: SerializeField] public bool IsPurchased { get; private set; } 
    }
}