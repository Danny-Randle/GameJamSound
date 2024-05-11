using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Rope : MonoBehaviour
{
    public int p1Cnt = 0;
    public int p2Cnt = 0;
    public int keyChangeCntr = 0;

    // create Random class object for keys:
    public System.Random rndKey = new();

    // make an int to store random nums:
    public int rngIntP1 = 0;
    public int rngIntP2 = 0;

    // array containing the pools of keys that the system can ask each player to press.
    public string[] keyBankp1 = { "q", "w", "e", "r", "t", "y", "tab", "a", "s", "d", "f", "g", "backslash", "z", "x", "c", "v", "b","f1","f2","f3","f4","f5","f6"};
    public string[] keyBankP2 = { "u", "i", "o", "p", "[", "]", "#", "h", "j", "k", "l", ";", "'", "n", "m", ",", ".", "/", "f7","f8","f9","f10","f11","f12"};
    

    // these store the current key needed to be pressed:
    public string currentKeyP1;
    public string currentKeyP2;

    // these will store the paths to the key img graphics:
    public string keyGraphicP1;
    public string keyGraphicP2;

    // Load icon sprites for P1:
    public Sprite Q;// = Resources.Load<Sprite>("q");
    public Sprite W;// = Resources.Load<Sprite>("w");
    public Sprite E;// = Resources.Load<Sprite>("e");
    public Sprite R;// = Resources.Load<Sprite>("r");
    public Sprite T;// = Resources.Load<Sprite>("t");
    public Sprite Y;// = Resources.Load<Sprite>("y");
    public Sprite TAB;// = Resources.Load<Sprite>("tab");
    public Sprite A;// = Resources.Load<Sprite>("a");
    public Sprite S;// = Resources.Load<Sprite>("s");
    public Sprite D;// = Resources.Load<Sprite>("d");
    public Sprite F;// = Resources.Load<Sprite>("f");
    public Sprite G;// = Resources.Load<Sprite>("g");
    public Sprite BSLASH;// = Resources.Load<Sprite>("backslash");
    public Sprite Z;// = Resources.Load<Sprite>("z");
    public Sprite X;// = Resources.Load<Sprite>("x");
    public Sprite C;// = Resources.Load<Sprite>("c");
    public Sprite V;// = Resources.Load<Sprite>("v");
    public Sprite B;// = Resources.Load<Sprite>("b");
    public Sprite F1;// = Resources.Load<Sprite>("f1");
    public Sprite F2;// = Resources.Load<Sprite>("f2");
    public Sprite F3;// = Resources.Load<Sprite>("f3");
    public Sprite F4;// = Resources.Load<Sprite>("f4");
    public Sprite F5;// = Resources.Load<Sprite>("f5");
    public Sprite F6;//= Resources.Load<Sprite>("f6");

    // Load icon sprites for P2:
    public Sprite U;// = Resources.Load<Sprite>("q");
    public Sprite I;// = Resources.Load<Sprite>("w");
    public Sprite O;// = Resources.Load<Sprite>("e");
    public Sprite P;// = Resources.Load<Sprite>("r");
    public Sprite OPEN_SQUARE_BRACKET;// = Resources.Load<Sprite>("t");
    public Sprite CLOSE_SQUARE_BRAKET;// = Resources.Load<Sprite>("y");
    public Sprite HASH;// = Resources.Load<Sprite>("tab");
    public Sprite H;// = Resources.Load<Sprite>("a");
    public Sprite J;// = Resources.Load<Sprite>("s");
    public Sprite K;// = Resources.Load<Sprite>("d");
    public Sprite L;// = Resources.Load<Sprite>("f");
    public Sprite SEMI_COLON;// = Resources.Load<Sprite>("g");
    public Sprite APOSTROPHE;// = Resources.Load<Sprite>("backslash");
    public Sprite N;// = Resources.Load<Sprite>("z");
    public Sprite M;// = Resources.Load<Sprite>("x");
    public Sprite COMMA;// = Resources.Load<Sprite>("c");
    public Sprite FULL_STOP;// = Resources.Load<Sprite>("v");
    public Sprite FSLASH;// = Resources.Load<Sprite>("b");
    public Sprite F7;// = Resources.Load<Sprite>("f1");
    public Sprite F8;// = Resources.Load<Sprite>("f2");
    public Sprite F9;// = Resources.Load<Sprite>("f3");
    public Sprite F10;// = Resources.Load<Sprite>("f4");
    public Sprite F11;// = Resources.Load<Sprite>("f5");
    public Sprite F12;//= Resources.Load<Sprite>("f6");

    public Sprite[] p1IconPool = new Sprite[24];
    public Sprite[] p2IconPool = new Sprite[24];

    // Do the same for the second player's icons:

    // Start is called before the first frame update
    public void Start()
    {

       

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
        Sprite[] p1IconPool = { Q, W, E, R, T, Y, TAB, A, S, D, F, G, BSLASH, Z, X, C, V, B, F1, F2, F3, F4, F5, F6 };

    // add to p1IconPool Hopefully this should allow for the system to reuse the same index for both the key and its icon:
        p1IconPool[0] = Q;
        p1IconPool[1] = W;
        p1IconPool[2] = E;
        p1IconPool[3] = R;
        p1IconPool[4] = T;
        p1IconPool[5] = Y;
        p1IconPool[6] = TAB;
        p1IconPool[7] = A;
        p1IconPool[8] = S;
        p1IconPool[9] = D;
        p1IconPool[10] = F;
        p1IconPool[11] = G;
        p1IconPool[12] = BSLASH;
        p1IconPool[13] = Z;
        p1IconPool[14] = X;
        p1IconPool[15] = C;
        p1IconPool[16] = V;
        p1IconPool[17] = B;
        p1IconPool[18] = F1;
        p1IconPool[19] = F2;
        p1IconPool[20] = F3;
        p1IconPool[21] = F4;
        p1IconPool[22] = F5;
        p1IconPool[23] = F6;

        //Debug.Log("index0 is: " + p1IconPool[0]);

        Application.targetFrameRate = 30;

        // get P1 and P2 objects:
        GameObject p1 = findObjByName("P1");
        GameObject p2 = findObjByName("P2");
        GameObject rope = findObjByName("Rope");

        // get key graphic objects:
        GameObject p1KeyIcon = findObjByName("P1Key");
        GameObject p2KeyIcon = findObjByName("P2Key");

        // get rects for all of the above:
        Rect p1Rect = p1.GetComponent<RectTransform>().rect;
        Rect p2Rect = p2.GetComponent<RectTransform>().rect;
        Rect ropeRect = rope.GetComponent<RectTransform>().rect;

        // get sprite for keyIcons:
        Sprite p1KeyIconSpr = p1KeyIcon.GetComponent<Image>().sprite;


        // protect system from selecting invalid indexes:
        if (keyChangeCntr > 0 && keyChangeCntr < 10)
        {
            rngIntP1 = rndKey.Next(keyBankp1.Length);
            rngIntP2 = rndKey.Next(keyBankP2.Length);

            if(rngIntP1 < 0)
            {
                rngIntP1 = 0;
            }
            if (rngIntP2 < 0)
            {
                rngIntP2 = 0;
            }
            if (rngIntP1 > keyBankp1.Length)
            {
                rngIntP1 = keyBankp1.Length -1;
            }
            if (rngIntP2 > keyBankP2.Length)
            {
                rngIntP2 = keyBankP2.Length -1;
            }

            // set current key to be the current random index of each player's key pool:
            currentKeyP1 = keyBankp1[rngIntP1];
            currentKeyP2 = keyBankP2[rngIntP2];

            // set the graphics to use the correct imgs:
             keyGraphicP1 = currentKeyP1; // relative path to key graphic

            // try swapping sprites:
            p1KeyIcon.GetComponent<Image>().sprite = p1IconPool[rngIntP1];
            p2KeyIcon.GetComponent<Image>().sprite = p2IconPool[rngIntP2];

            //Debug.Log("Current Sprite Index sprite:" + p1IconPool[rngIntP1]);

            // Load the appropriate image and set it as the sprite for the key icon:
            p1KeyIconSpr = Resources.Load(keyGraphicP1, typeof(Sprite)) as Sprite;
            Debug.Log(keyGraphicP1);

            // output to debug console and jump the key change counter by 15 so the system doesn't get stuck cycling through keys:
            Debug.Log("P1 Key: " + currentKeyP1 + " P2 Key: " + currentKeyP2);
            keyChangeCntr += 15;
        }

        // reset key counter to zero when it reaches 500:
        if(keyChangeCntr == 5000)
        {
            keyChangeCntr = 0;
        }

        // increment the keyChange counter:
        keyChangeCntr++;

        // get the input for P1:
        if (Input.GetKey(currentKeyP1))
        {
            p1Cnt += 1;
        }

        // get the input for P2:
        if (Input.GetKey(currentKeyP2))
        {
            p2Cnt += 1;
        }
        if (p1Cnt > p2Cnt)
        {
            // calculate how much bigger it is:
            int dif = p1Cnt - p2Cnt;
            rope.transform.Translate(-0.1f - dif / 10, 0, 0);
        }
        if (p2Cnt > p1Cnt)
        {
            int dif = p2Cnt - p1Cnt;
            rope.transform.Translate(0.1f + dif / 10, 0, 0);
        }

        // win conditions:
        if(rope.transform.position[0] - (ropeRect.width / 4) <= p1.transform.position[0] + p1Rect.width)
        {
            Debug.Log("P1 Wins");
            SceneManager.LoadScene(3);
        }
        if (rope.transform.position[0] + (ropeRect.width / 2) > Screen.width - 390)
        {
            Debug.Log("P2 Wins");
            SceneManager.LoadScene(4);
        }
    }
}