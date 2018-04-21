##Creature Platformer

![Logo](http://www.kestrelmoon.com/creaturedocs/img/unity_platformer.png)

This is a 2D Side-scrolling Platformer Sample with characters authored using the Creature Animation Tool ( http://creature.kestrelmoon.com/ ) It uses the provided Creature Animation Plugins for Unity to make the characters move, jump etc. in the world. Full source is provided along with video tutorials on how this sample was implemented. Characters are deformed in realtime using both mesh and bone skinning algorithms run in the provided plugin. 

This platformer demo allows you to control the Fox main character and move around the scene. The Fox can move left, right and jump.

###Controls
Controls for the main Fox character:

**A** - Moves Left 

**D** - Moves Right 

**Space** - Jump

###Video Tutorials
Video Tutorials on how the Sample was constructed and coded up on C#:

**Part 1**, Animation Authoring: https://youtu.be/BtEZD61Y7oc

**Part 2**, Implementing the basic mechanics for Movement, Jumping etc. in C# and Unity: https://youtu.be/_Ccqd9lZ6UA

###How it works
The **Creature Platformer** runs off the **Creature Unity Plugin**. The relevant characters using the plugin are grouped under the following objects: **Spirit**, **Fox**, **Yeti**, **Bats** and **Horsemen**.

#### Where the Files are Located
All Creature Animation data resources are located under the **Resources** folder. The actual plugin lives under the **Distro** and **Editor** folders.


###Creature Plugin for Unity
The **Creature Unity Plugin** has the following components:

- **CreatureAsset** - This contains the Creature Animation Data for the current character.

- **CreatureRenderer** - This is the actual renderer object that displays the character.

- **CreatureGameController** - This is an optional object used for gameplay mechanics of Creature characters.

- **CreatureAgent** - The actual object that contains Gameplay/AI logic to control + move the Creautre character.


###How a Creature Character is imported into Unity
1) Click on **Creature->CreatureAsset** to add a new **CreatureAsset**. Connect up the **Flat Data** slot pointing to the exported binary flat data of the character from Creature.

2) Click on **Creature->CreatureRenderer** to add a new **CreatureRenderer**. Connect up the **CreatureAsset** slot to point to the asset created from 1)

3) Drag in the character's texture image into the **CreatureRenderer**, select an appropriate shader ( Sprite should work )

At this point the character should be displayed in Unity.

###Gameplay Mechanics
Gameplay mechanics and control of Creature characters are done through the **CreatureGameController** and **CreatureAgent** objects.

Read the full documentation online to learn how to use these 2 components. If you want to extend a play around with the current gameplay/control logic, examine any of the objects like **BatAgent** or **FoxAgent**.

In this writeup we will take a look at **FoxAgent.cs** located under the **Resources** folder.

####Fox Agent

Below is the code of **Fox Agent**:

    using UnityEngine;
    using System.Collections;

    public class FoxAgent : CreatureGameAgent
    {
        int moveState = 0;
        bool facingRight = true;

        const int MOVE_STATE_IDLE = 0;
        const int MOVE_STATE_LEFT = -1;
        const int MOVE_STATE_RIGHT = 1;
        const int MOVE_STATE_JUMP = 2;
        const int MOVE_STATE_POST_JUMP = 3;

        public FoxAgent()
            : base()
        {
            moveState = MOVE_STATE_IDLE;
        }

        public override void updateStep()
        {
            base.updateStep();

            bool left_down = Input.GetKeyDown(KeyCode.A);
            bool right_down = Input.GetKeyDown(KeyCode.D);
            bool left_up = Input.GetKeyUp(KeyCode.A);
            bool right_up = Input.GetKeyUp(KeyCode.D);
            bool space_down = Input.GetKeyDown(KeyCode.Space);

            if (left_down)
            {
                facingRight = false;
                moveState = MOVE_STATE_LEFT;
            }
            else if (right_down)
            {
                facingRight = true;
                moveState = MOVE_STATE_RIGHT;
            }
            else if (left_up || right_up)
            {
                moveState = MOVE_STATE_IDLE;
            }
            else if (space_down)
            {
                moveState = MOVE_STATE_JUMP;
            }

            setMovement();
            setAnimation();
        }

        private void setMovement()
        {
            var creature_renderer = game_controller.creature_renderer;
            var parent_obj = creature_renderer.gameObject;
            Rigidbody2D parent_rbd = parent_obj.GetComponent<Rigidbody2D>();
            var speed = 25.0f;
            var curVel = parent_rbd.velocity;

            if (moveState == MOVE_STATE_LEFT)
            {
                parent_rbd.velocity = new Vector3(-speed, curVel.y, 0);
            }
            else if (moveState == MOVE_STATE_RIGHT)
            {
                parent_rbd.velocity = new Vector3(speed, curVel.y, 0);
            }
            else if (moveState == MOVE_STATE_IDLE)
            {
                parent_rbd.velocity = new Vector3(0, curVel.y, 0);
            }
            else if (moveState == MOVE_STATE_JUMP)
            {
                var groundObjs = GameObject.FindGameObjectsWithTag("ground_tag");
                foreach (var ground in groundObjs)
                {
                    var ground_collider = ground.GetComponent<Collider2D>();
                    if (parent_rbd.IsTouching(ground_collider))
                    {
                        var jumpForceX = 30.0f;
                        if (!facingRight)
                        {
                            jumpForceX = -jumpForceX;
                        }
                        parent_rbd.AddForce(new Vector3(jumpForceX, 150, 0), ForceMode2D.Impulse);

                        break;
                    }

                }
                moveState = MOVE_STATE_POST_JUMP;
            }
            else if (moveState == MOVE_STATE_POST_JUMP)
            {
                //var ground = GameObject.FindWithTag("ground_tag");
                //var ground_collider = ground.GetComponent<Collider2D>();
                //if (parent_rbd.IsTouching(ground_collider))
                if (creature_renderer.is_colliding)
                {
                    moveState = MOVE_STATE_IDLE;
                }
            }
        }

        private void setAnimation()
        {
            var creature_renderer = game_controller.creature_renderer;
            var local_size = 0.2f;
            creature_renderer.blend_rate = 0.2f;

            if (facingRight)
            {
                creature_renderer.transform.localScale = new Vector3(-local_size, local_size, local_size);
            }
            else
            {
                creature_renderer.transform.localScale = new Vector3(local_size, local_size, local_size);
            }

            if ((moveState == MOVE_STATE_RIGHT) || (moveState == MOVE_STATE_LEFT))
            {
                creature_renderer.BlendToAnimation("run");
            }
            else if ((moveState == MOVE_STATE_JUMP)
                || (moveState == MOVE_STATE_POST_JUMP))
            {
                creature_renderer.BlendToAnimation("jump");
            }
            else if (moveState == MOVE_STATE_IDLE)
            {
                creature_renderer.creature_manager.ResetBlendTime("jump");
                creature_renderer.BlendToAnimation("default");
            }
        }

        private void turnOffOtherObjectCollisions(GameObject[] objectList)
        {
            var creature_renderer = game_controller.creature_renderer;
            var parent_obj = creature_renderer.gameObject;
            var self_collider = parent_obj.GetComponent<Collider2D>();

            foreach (var curObject in objectList)
            {
                var object_collider = curObject.GetComponent<Collider2D>();
                Physics2D.IgnoreCollision(self_collider, object_collider);
            }
        }

        public override void initState()
        {
            base.initState();

            turnOffOtherObjectCollisions(GameObject.FindGameObjectsWithTag("horseman_tag"));
            turnOffOtherObjectCollisions(GameObject.FindGameObjectsWithTag("bat_tag"));
        }
    }

There are 2 primary methods to override for the agent: **UpdateStep** and **initState**. The **initState** method in this case turns off collisions of the fox with the other characters in the scene. You can add your own initialization code here as well.

The **updateStep** method does the actual gameplay state update of the Fox. It gathers the user's key inputs before deciding on setting the appropriate animations and processing the movement.
**setMovement** does the actual movement of the Fox while **setAnimation** does switching of animation states of the Fox character.

### Full Creature Plugin Documentation

Learn how to use the Creature Plugin here:
http://www.kestrelmoon.com/creaturedocs/Game_Engine_Runtimes_And_Integration/Unity_Runtimes.html

Creature Animation Tool:
http://creature.kestrelmoon.com/
