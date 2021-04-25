using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
	Transform mTrans;

	enum MoveDirection{Down, Left, Right, Up, None};
	MoveDirection CurrentDir = MoveDirection.None;
	enum tileTypes{Air, Dirt, Grass, Rock, BG, Gem};
	//tileTypes ttype;
	public Tilemap tilemap;
	bool canMove = false;
	public Tile replace;

    void Start()
    {
		mTrans = this.transform;
		mTrans.position = new Vector2(Mathf.Round(mTrans.position.x) + 0.5f , Mathf.Round(mTrans.position.y) + 0.5f);
    }
	

    void Update()
    {
		if (Input.GetButtonDown("Right"))
		{
			CurrentDir = MoveDirection.Right;
		}
		else if (Input.GetButtonDown("Left"))
		{
			CurrentDir = MoveDirection.Left;
		}
		else if (Input.GetButtonDown("Down"))
		{
			CurrentDir = MoveDirection.Down;
		}
		else if (Input.GetButtonDown("Up"))
		{
			CurrentDir = MoveDirection.Up;
		}
		if(CurrentDir != MoveDirection.None)
			Move(CurrentDir);

		//if (tilemap.GetTile(Vector3Int.FloorToInt(transform.position + Vector3.down)))
		//	mTrans.position += Vector3.down;
    }

	void Move(MoveDirection dir)
	{
		switch (dir)
		{
			case MoveDirection.Down: checkDirection(Vector2.down);
			break;
			case MoveDirection.Left: checkDirection(Vector2.left);
			break;
			case MoveDirection.Right: checkDirection(Vector2.right);
			break;
			case MoveDirection.Up:if(transform.position.y < 0){ checkDirection(Vector2.up); }
			break;
			default:
			break;
		}
		CurrentDir = MoveDirection.None;
	}

	void checkDirection(Vector2 offset)
	{
		Vector3Int v3 = Vector3Int.FloorToInt(mTrans.position + (Vector3)offset); 
		TileBase t = tilemap.GetTile(v3);

		switch (ttype(t.name))
		{
			case tileTypes.Air:
			mTrans.position += (Vector3)offset;
			break;
			case tileTypes.Dirt:
			tilemap.SetTile(v3 , replace);
			mTrans.position += (Vector3)offset;
			break;
			case tileTypes.Grass:
			tilemap.SetTile(v3 , replace);
			mTrans.position += (Vector3)offset;
			break;
			case tileTypes.Rock:
			canMove = false;
			break;
			case tileTypes.BG:
			mTrans.position += (Vector3)offset;
			break;
			case tileTypes.Gem:
			GameManager.instance.addcoins();
			tilemap.SetTile(v3 , replace);
			mTrans.position += (Vector3)offset;
			break;
			default:
			break;
		}
		if (ttype(t.name) == tileTypes.Air)
		{
			canMove = true;
		}
	}

	tileTypes ttype(string name)
	{
		switch (name)
		{
			case "air": return tileTypes.Air;
			case "dirt": return tileTypes.Dirt;
			case "rock": return tileTypes.Rock;
			case "grass": return tileTypes.Grass;
			case "dirtbg": return tileTypes.BG;
			case "gem": return tileTypes.Gem;
			default: return tileTypes.Air;
		}
	}
}
