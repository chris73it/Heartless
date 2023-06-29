using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroicArcade.CC.Core {

    public class final_wfc_property : MonoBehaviour {

        [HideInInspector] public string myTag;
        public int myProperty;
        public List<int> acceptableProperties = new List<int>();

        void Start() {
            myTag = gameObject.tag;
            myProperty = transform.GetSiblingIndex();
        }

        void Update() {
            
        }
    }
}