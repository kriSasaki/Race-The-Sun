using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.Configs.Ships
{
    [CreateAssetMenu(fileName = "ShipConfigSheet", menuName = "Configs/ShipConfigSheet", order = 1)]
    public class ShipConfigSheet : ScriptableObject
    {
        [SerializeField] private List<ShipConfig> _shipConfigs;

        public IEnumerable<ShipConfig> Ships => _shipConfigs;

        public ShipConfig GetShipConfigById(string id)
        {
            return _shipConfigs.FirstOrDefault(ship => ship.ID == id);
        }
    }
}