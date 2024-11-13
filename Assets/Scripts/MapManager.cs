using System;
using System.Collections;
using System.Collections.Generic;
using InventorySystem;
using UnityEngine;

public class MapManager : Singleton<MapManager>
{
    #region TileThings

    [System.Serializable]
    public class TileRewardData
    {
        public int amount;
        public string type;
    }

    [System.Serializable]
    public class Tile
    {
        public int id;
        public TileRewardData reward;
    }

    [System.Serializable]
    public class GameMap
    {
        public int totalTiles;
        public Tile[] tiles;
    }

    public TextAsset mapJson;

    public List<Tile> tilesList;

    #endregion

    public GameObject tilePrefab;

    private void Start()
    {
        LoadMap();
    }

    [NaughtyAttributes.Button()]
    void LoadMap()
    {
        GameMap map = JsonUtility.FromJson<GameMap>(mapJson.ToString());

        tilesList = new List<Tile>(map.tiles);
        Vector3 currentPosition = Vector3.zero;

        for (int i = 0; i < map.totalTiles; i++)
        {
            var tileUnit = Instantiate(tilePrefab, currentPosition, Quaternion.identity).transform;
            var typeSprite = InventoryManager.Instance.GetTypesSprite(tilesList[i].reward.type);
            tileUnit.GetComponent<TileUnit>().rewardValue.text = tilesList[i].reward.amount.ToString();
            tileUnit.GetComponent<TileUnit>().stepCount.text = tilesList[i].id.ToString();

            if (typeSprite != null)
            {
                tileUnit.GetComponent<TileUnit>().rewardImage.sprite = typeSprite;
            }
            else
            {
                tileUnit.GetComponent<TileUnit>().rewardImage.gameObject.SetActive(false);
                tileUnit.GetComponent<TileUnit>().rewardValue.gameObject.SetActive(false);
            }


            currentPosition.x += 1;
        }

        PlayerMovement.Instance.maxTileNumber = tilesList.Count;
    }

    public void DetectReward(int id)
    {
        var itemType = tilesList[id - 1].reward.type;
        if (Enum.TryParse(itemType, true, out InventoryManager.ItemType itemEnum))
        {
            InventoryManager.Instance.AddItem(itemEnum,tilesList[id - 1].reward.amount);
            PlayerMovement.Instance.lootParticle.Play();
        }

        GameManager.Instance.bCanRoll = true;
    }
}