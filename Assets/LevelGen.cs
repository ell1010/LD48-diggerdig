using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelGen : MonoBehaviour
{
	[SerializeField]
	groundTiles[] tiles = new groundTiles[4];
	[SerializeField]
	List<tileRow> currentRow = new List<tileRow>();
	int weightedsum = 0;
	public Transform player;
	public Tilemap tmap;
	int lowestplayerpoint;
	[SerializeField]
	Tile air;
	void Start()
	{
		foreach (groundTiles tile in tiles)
		{
			weightedsum += tile.weight;
		}
		generateAir();
		generateTiles();
	}
	
	void Update()
	{
		if (player.position.y < lowestplayerpoint)
		{
			lowestplayerpoint = (int)player.position.y;
			generateTiles();
		}
	}

	void generateAir()
	{
		for (int o = -3; o < 3; o++)
		{
			if (!tmap.HasTile(new Vector3Int(o , 0 , 0)))
				tmap.SetTile(new Vector3Int(o , 0 , 0) , air);
		}
	}

	void generateTiles()
	{
		for (int i = -1; i >= player.transform.position.y - 6; i--)
		{
			int rocks = 0;
			currentRow = new List<tileRow>();
			for (int o = -7; o < 7; o++)
			{
				if (!tmap.HasTile(new Vector3Int(o , i , 0)))
				{
					if (o < -3 || o > 3)
					{
						tmap.SetTile(new Vector3Int(o , i , 0) , tiles[3].tile);
					}
					else
					{
						Tile t = tiles[arrayvalue()].tile;
						tmap.SetTile(new Vector3Int(o , i , 0) , t);

						currentRow.Add(new tileRow { tile = t , x = o , y = i });
						if (t.name == "rock")
						{
							rocks++;
						}
					}
				}
			}
			if (rocks > 3)
			{
				foreach (tileRow tile in currentRow)
				{
					if (tile.tile.name == "rock")
					{
						Tile t = tiles[arrayvalue()].tile;
						tmap.SetTile(new Vector3Int(tile.x , tile.y , 0) , t);
						print("changerocks");
					}
				}
			}
		}
	}

	int arrayvalue()
	{
		int x = Random.Range(0 , weightedsum);
		int addedweight = 0;
		for (int i = 0; i < tiles.Length; i++)
		{
			addedweight += tiles[i].weight;
			if (x < addedweight)
				return i;
		}
		return 0;
	}
}

[System.Serializable]
public class groundTiles
{
	public int weight;
	public Tile tile;
}
[System.Serializable]
public class tileRow
{
	public Tile tile;
	public int x;
	public int y;
}
