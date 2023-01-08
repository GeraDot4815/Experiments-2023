using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Level : MonoBehaviour
{
    public static Level Instance;
    [field: SerializeField] public ElementTypes.Elements biom { get; private set; }
    [SerializeField] private List<Tilemap> tileMaps;
    [SerializeField] private TileHolder defaultTiles;
    [SerializeField] private TileHolder greenTiles;
    [SerializeField] private TileHolder fireTiles;
    [SerializeField] private TileHolder iceTiles;
    [SerializeField] private TileHolder spaceTiles;
    private void Awake()
    {
        Instance = this;
        ChangeTiles();
    }
    private void ChangeTiles()
    {
        switch (biom)
        {
            case ElementTypes.Elements.GreenGrass:
                MySwipeTiles(greenTiles);
                break;
            case ElementTypes.Elements.Fire:
                MySwipeTiles(fireTiles);
                break;
            case ElementTypes.Elements.Ice:
                MySwipeTiles(iceTiles);
                break;
            case ElementTypes.Elements.Space:
                MySwipeTiles(spaceTiles);
                break;
        }
    }
    private void MySwipeTiles(TileHolder holder)
    {
        foreach (Tilemap tmap in tileMaps)
        {
            tmap.SwapTile(defaultTiles.floorTile, holder.floorTile);
            tmap.SwapTile(defaultTiles.topWall, holder.topWall);
        }
    }
}
