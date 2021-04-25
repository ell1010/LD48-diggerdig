﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public GameObject player;
	
    void Update()
    {
		Vector3 screnpos = Camera.main.WorldToScreenPoint(player.transform.position);
		if (screnpos.y < (Screen.height / 2))
		{
			this.transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
		}
    }
}
