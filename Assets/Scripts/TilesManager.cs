using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using UnityEngine.WSA;

public class TilesManager : MonoBehaviour
{
    [SerializeField]
    private Tilemap landTileMap;

    [SerializeField]
    private Tilemap waterTileMap;

    [SerializeField]
    private Tilemap objectTileMap;

    [SerializeField]
    private GameObject tilePrefab;

    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private float distanceToShow = 0.5f;

    [SerializeField]
    private Dust dust;

    private List<BastionTile> landTiles = new List<BastionTile>();
    private List<BastionTile> waterTiles = new List<BastionTile>();
    private List<BastionTile> objectTiles = new List<BastionTile>();

    // Start is called before the first frame update
    void Start()
    {
        //GameObject waterTilesParent = GameObject.Find("Water Tiles");
        GameObject landTilesParent = GameObject.Find("Land Tiles");
        GameObject objectTilesParent = GameObject.Find("Object Tiles");

        //foreach (Transform waterChild in waterTilesParent.transform)
        //{
        //    var tile = waterChild.GetComponent<BastionTile>();
        //    tile.setupShow();
        //    waterTiles.Add(tile);
        //}

        foreach (Transform landChid in landTilesParent.transform)
        {
            var bastionTile = landChid.GetComponent<BastionTile>();
            landTiles.Add(bastionTile);

            var distance = Vector2.Distance(playerTransform.position, landChid.position);

            if (distance < distanceToShow * 1.5f)
            {
                bastionTile.setupShow();
            }
        }

        foreach (Transform objectChild in objectTilesParent.transform)
        {
            var bastionTile = objectChild.GetComponent<BastionTile>();
            objectTiles.Add(bastionTile);

            var distance = Vector2.Distance(playerTransform.position, objectChild.position);

            if (distance < distanceToShow)
            {
                bastionTile.setupShow();
            }
        }
    }

    private void Update()
    {
        foreach (var tile in landTiles)
        {
            var distance = Vector2.Distance(playerTransform.position, tile.transform.position);

            if (distance < distanceToShow * 1.5f)
            {
                tile.Show(-1, 1.25f, Ease.OutSine, true);
            }
        }

        foreach (var tile in objectTiles)
        {
            var distance = Vector2.Distance(playerTransform.position, tile.transform.position);

            if (distance < distanceToShow)
            {
                tile.Show(1, 0.25f, Ease.OutBounce, false, () => OnTileAnimDone(tile.transform.position));
            }
        }
    }

    private void OnTileAnimDone(Vector3 position)
    {
        Instantiate(dust, position, Quaternion.identity);
    }

    public void GenerateAllTile()
    {
        GameObject tilesParentGameObject = new GameObject();
        tilesParentGameObject.name = "Tiles";

        //var waterTilesParent = Instantiate(tilesParentGameObject, new Vector3(0, 0, 0), Quaternion.identity);
        //waterTilesParent.name = "Water Tiles";
        //waterTilesParent.transform.parent = tilesParent.transform;

        GameObject landTilesParent = new GameObject();
        landTilesParent.name = "Land Tiles";
        landTilesParent.transform.parent = tilesParentGameObject.transform;

        GameObject objectTilesParent = new GameObject();
        objectTilesParent.name = "Object Tiles";
        objectTilesParent.transform.parent = tilesParentGameObject.transform;

        //GenerateTile(waterTileMap, waterTilesParent, -1, true);
        GenerateTile(landTileMap, landTilesParent, 0, false);
        GenerateTile(objectTileMap, objectTilesParent, 1, false);
    }    

    public void GenerateTile(Tilemap tilemap, GameObject parent, int sortingOrder, bool isShow)
    {
        int index = 0;
        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
            if (tilemap.HasTile(localPlace))
            {
                var sprite = tilemap.GetSprite(localPlace);
                Vector3 place = tilemap.CellToWorld(localPlace);
                place.z = place.y;
                var tile = Instantiate(tilePrefab, place, Quaternion.identity);
                tile.name = "Tile " + sprite.name + $" ({index})";
                tile.transform.parent = parent.transform;

                var renderer = tile.GetComponent<SpriteRenderer>();
                renderer.sprite = sprite;
                renderer.sortingOrder = sortingOrder;
                index++;
            }
        }
    }
}
