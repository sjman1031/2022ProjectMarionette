﻿using UnityEngine;
using System.Collections;

public class take_star : MonoBehaviour {

	game_uGUI my_game_uGUI;

	// Use this for initialization
	void Start () {

		this.gameObject.SetActive(true);
		my_game_uGUI = GameObject.FindGameObjectWithTag("_gui_").GetComponent<game_uGUI>();
        my_game_uGUI.total_collectable_stars_in_this_stage++;

    }
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag("Player"))//if the player collide with this star
			{
			if (my_game_uGUI.star_number < my_game_uGUI.total_collectable_stars_in_this_stage)
				{
				my_game_uGUI.Add_stars(1);
				}
			
			this.gameObject.SetActive(false);
			}
	}
}
