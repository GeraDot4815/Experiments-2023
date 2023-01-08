using UnityEngine;
using UnityEngine.Tilemaps;
[CreateAssetMenu(fileName = "NewTileHolder", menuName = "Custom/Tiles/TileHolder")]
public class TileHolder : ScriptableObject
{
    [field : SerializeField] public TileBase floorTile { get; private set; }
    [field: SerializeField] public TileBase topWall { get; private set; }
}
