using UnityEngine;

namespace Project.Systems.Data
{
    [System.Serializable]
    public class LevelData
    {
        [SerializeField] private string _levelName;

        public LevelData(string levelName)
        {
            _levelName = levelName;
        }

        public string LevelName => _levelName;
    }
}