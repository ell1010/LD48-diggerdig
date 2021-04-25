using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum tileTypes { Air, Dirt, Grass, Rock };

[CreateAssetMenu]
public class basetile : Tile
{
	public tileTypes type;
}
