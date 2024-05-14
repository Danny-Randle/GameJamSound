using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ptsPelletScript
{
    public class ptsPellet : MonoBehaviour
    {
        // random class variable to handle movement direction:
        public System.Random ptsDirChooser = new();

        // int to hold random value:
        public int ptsDirChoice = -1;

        // movement variables:
        public int movementAmountX = 0;
        public int movementAmountY = 0;

        // pts worth:
        public int SCORE_VALUE = 1; // worth 1 pts.

        // decide movement type:
        public int movementType = 0; // this will be random. There will be 8 in total however.


        // Start is called before the first frame update
        void Start()
        {
            // set the ptsDirChoice to a value chosen by ptsDirChooser:
            ptsDirChoice = 0;
            //Debug.Log("ptsDirChoice: " + ptsDirChoice);
        }

        // method to call unity's object finder and return it's GameObject:
        GameObject findObjByName(string objName)
        {
            GameObject obj = GameObject.Find(objName);
            return obj;
        }

        // Update is called once per frame
        void Update()
        {
            // get the ball GameObject and its collider box and its script data:
            GameObject ball = findObjByName("ball");
            BoxCollider2D ballBox = ball.GetComponent<BoxCollider2D>();
            Ball ballVarData = ball.GetComponent<Ball>();
            GameObject bg = findObjByName("BG");



            // decide what movement should occur based on the ptsDirChoice value:
            switch (ptsDirChoice)
            {
                case 0:
                    movementAmountX = 10;
                    movementAmountY = 0;
                    break;

                case 1:
                    movementAmountX = 10;
                    movementAmountY = 10;
                    break;

                case 2:
                    movementAmountX = 0;
                    movementAmountY = 10;
                    break;

                case 3:
                    movementAmountX = -10;
                    movementAmountY = 10;
                    break;

                case 4:
                    movementAmountX = -10;
                    movementAmountY = 0;
                    break;

                case 5:
                    movementAmountX = -10;
                    movementAmountY = -10;
                    break;

                case 6:
                    movementAmountX = 0;
                    movementAmountY = -10;
                    break;

                case 7:
                    movementAmountX = 10;
                    movementAmountY = -10;
                    break;

                default:
                    Debug.Log("ERROR: 01 - Something has gone wrong with the direction chooser, please see line 28."); // error if nothing matches.
                    break;
            }

            // now do the movement based on direction:
            gameObject.transform.Translate(movementAmountX, movementAmountY, 0);

            // deal with collision:
            if (gameObject.GetComponent<BoxCollider2D>().IsTouching(ballBox))
            {
                ballVarData.pts += SCORE_VALUE; // add this pellet's score value to the arcade score.
                Destroy(gameObject); // delete self from memory.
            }

            // finally deal with it getting too far off screen:
            if (gameObject.transform.position[0] > Screen.width + 128 || gameObject.transform.position[0] < -128)
            {
                gameObject.transform.SetPositionAndRotation(new Vector3(-64, ptsDirChooser.Next(1080), 0), transform.rotation);
            }
            if (gameObject.transform.position[1] > Screen.height + 128 || gameObject.transform.position[1] < -128)
            {
                gameObject.transform.SetPositionAndRotation(new Vector3(-64, ptsDirChooser.Next(1080), 0), transform.rotation);
            }


        }
    }

}
