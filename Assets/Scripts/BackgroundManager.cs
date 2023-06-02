using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroicArcade.CC.Core 
{ 
    public class BackgroundManager : MonoBehaviour {
     public Vector3 leftMovement;
     public Vector3 leftThreshold;
     public Vector3 rightThreshold;
     public GameObject[] backgroundPrefabs;

      public Player player;

     void Update()
       {
        leftMovement =  player.NewleftMovement;
       }

}
}