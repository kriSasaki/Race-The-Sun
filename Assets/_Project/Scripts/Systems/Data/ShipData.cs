[System.Serializable]
public class ShipData
{
    public string ID;
    public string Name;
    public string ModelPrefabPath;

    public ShipData(string id, string name, string modelPrefabPath)
    {
        ID = id;
        Name = name;
        ModelPrefabPath = modelPrefabPath;
    }
}