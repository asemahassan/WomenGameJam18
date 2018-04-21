﻿using System;
using CreatureModule;
using UnityEngine;

public class RaptorGame : CreatureGameAgent
{
	private bool is_moving = false;
	private bool is_facing_left = false;
	public bool use_ai = false;
	private int ai_cnt = 600;
	private int ai_state_x = 0;
	private int ai_state_y = 0;
	
	public RaptorGame ()
		: base()
	{
	}
	
	private void runAI()
	{
		var creature_renderer = game_controller.creature_renderer;
		var parent_obj = creature_renderer.gameObject;
		Rigidbody2D parent_rbd = parent_obj.GetComponent<Rigidbody2D> ();
		parent_rbd.gravityScale = 0;
		
		
		if (ai_cnt > UnityEngine.Random.Range (200, 500)) {
			int roll_val = UnityEngine.Random.Range (0, 150);
			if (roll_val > 100) {
				ai_state_x = 1;
			}
			else if (roll_val < 100 && roll_val > 50) {
				ai_state_x = 2;
			}
			else {
				ai_state_x = 0;
			}
			
			roll_val = UnityEngine.Random.Range (0, 150);
			if (roll_val > 100) {
				ai_state_y = 1;
			}
			else if (roll_val < 100 && roll_val > 50) {
				ai_state_y = 2;
			}
			else {
				ai_state_y = 0;
			}
			
			if(ai_state_x == 0)
			{
				ai_state_y = 0;
			}
			
			ai_cnt = 0;
		}
		
		
		if(parent_rbd.position.x < -71)
		{
			ai_state_x = 1;
		}
		else if(parent_rbd.position.x > 71)
		{
			ai_state_x = 2;
		}
		
		if(parent_rbd.position.y < -39)
		{
			ai_state_y = 1;
		}
		else if(parent_rbd.position.y > 29)
		{
			ai_state_y = 2;
		}
		
		
		float move_vel_x = 0;
		float move_vel_y = 0;
		
		if (ai_state_x == 0) {
		} 
		else if (ai_state_x == 1) {
			move_vel_x = 5;
		} 
		else if (ai_state_x == 2) {
			move_vel_x = -5;
		}

		/*
		if (ai_state_y == 1) {
			move_vel_y = 2;
		} 
		else if (ai_state_y == 2) {
			move_vel_y = -2;
		}
		*/

		ai_cnt++;
		
		// now do actual movement
		if (is_moving) {
			if(is_moving)
			{
				parent_rbd.velocity = new UnityEngine.Vector2(move_vel_x, move_vel_y);
				
				if(is_facing_left)
				{
					creature_renderer.BlendToAnimation("slow");
				}
				else {
					creature_renderer.BlendToAnimation("fast");
				}
			}
			
			if(ai_state_x == 0) {
				is_moving = false;
				var cur_vel = parent_rbd.velocity;
				cur_vel.x = 0;
				parent_rbd.velocity = cur_vel;
				creature_renderer.BlendToAnimation("default");
			}
		} 
		else {
			if (ai_state_x == 1) {
				is_moving = true;
				is_facing_left = true;
			} 
			else if (ai_state_x == 2) {
				is_moving = true;
				is_facing_left = false;
			}
		}
		
		
		if (!is_moving) {
			if(creature_renderer.active_animation_name != "default")
			{
				creature_renderer.BlendToAnimation("default");
			}
		} 
	}
	
	public override void updateStep()
	{
		base.updateStep ();
		
		var creature_renderer = game_controller.creature_renderer;
		
		bool left_down = Input.GetKeyDown (KeyCode.A);
		bool right_down = Input.GetKeyDown (KeyCode.D);
		bool left_up = Input.GetKeyUp (KeyCode.A);
		bool right_up = Input.GetKeyUp (KeyCode.D);
		//bool attack_down = Input.GetKeyDown (KeyCode.R);
		//bool attack_up = Input.GetKeyUp (KeyCode.R);
		var parent_obj = creature_renderer.gameObject;
		Rigidbody2D parent_rbd = parent_obj.GetComponent<Rigidbody2D> ();
		parent_rbd.gravityScale = 0;
		
		if (use_ai) {
			runAI ();
			return;
		}
		
		if (is_moving) {
			if(is_moving)
			{
				float move_vel_x = 0;
				if(is_facing_left)
				{
					move_vel_x = -10;
				}
				else {
					move_vel_x = 10;
				}
				parent_rbd.velocity = new UnityEngine.Vector2(move_vel_x, 0);
				
				if(is_facing_left)
				{
					creature_renderer.BlendToAnimation("fast");
					//creature_renderer.SetActiveAnimation ("forward", true);
				}
				else {
					creature_renderer.BlendToAnimation("slow");
					//creature_renderer.SetActiveAnimation ("reverse", true);
				}
			}
			
			if(left_up || right_up) {
				is_moving = false;
				var cur_vel = parent_rbd.velocity;
				cur_vel.x = 0;
				parent_rbd.velocity = cur_vel;
				creature_renderer.BlendToAnimation("default");
				//creature_renderer.SetActiveAnimation ("default");
			}
		} 
		else {
			if (left_down) {
				is_moving = true;
				is_facing_left = true;
			} 
			else if (right_down) {
				is_moving = true;
				is_facing_left = false;
			}
			
			/*
			float facing_angle = 0;
			if(is_facing_left) {
				facing_angle = 180;
			}
			*/
			//parent_obj.transform.rotation = UnityEngine.Quaternion.AngleAxis(facing_angle, new UnityEngine.Vector3(0, 1, 0));
		}
		
		
		if (!is_moving) {
			if(creature_renderer.active_animation_name != "default")
			{
				creature_renderer.BlendToAnimation("default");
				//creature_renderer.SetActiveAnimation ("default");
			}
		} 
		
	}
}


