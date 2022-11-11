using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDragCommand : HazardCommand
    {
        [SerializeField]
        private float linearDrag;
        public float LinearDrag => linearDrag;
        [SerializeField]
        private float angularDrag;
        public float AngularDrag => angularDrag;

        public DragChangeCommand(float linearDrag, float angularDrag=0)
        {
            this.linearDrag = linearDrag;
            this.angularDrag = angularDrag;
        }

    }
