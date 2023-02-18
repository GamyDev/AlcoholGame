// Simple Scroll-Snap - https://assetstore.unity.com/packages/tools/gui/simple-scroll-snap-140884
// Copyright (c) Daniel Lochner

using UnityEngine;

namespace DanielLochner.Assets.SimpleScrollSnap
{
    public class SlotMachine : MonoBehaviour
    {
        #region Fields
        [SerializeField] private SimpleScrollSnap[] slots;
        private bool startSpine;
        private bool pressed;
        #endregion

        #region Methods

        //public void Spin()
        //{
        //    foreach (SimpleScrollSnap slot in slots)
        //    {
        //        slot.Velocity = Random.Range(250000, 500000) * Vector2.right;
        //    }
        //}

        private int[] speedArr = new int[] { 13500, 14000, 15000 };
        private int speedIndex;
        private int speed;
        
        public void Spin()
        {
            if (!pressed)
            {

                int currSpeed = Random.Range(0, speedArr.Length);
                while (currSpeed == speedIndex)
                {
                    currSpeed = Random.Range(0, speedArr.Length);
                }

                speedIndex = currSpeed;

                speed = speedArr[speedIndex] + Random.Range(-1000, 1000);

                Debug.Log($"Speed {speed}");
                startSpine = true;
                Invoke("StopSpine", 2f);
                pressed = true;
            }
            /* foreach (SimpleScrollSnap slot in slots)
             {
                 slot.Velocity += Random.Range(25000, 50000) * Vector2.left;
             }*/
        }

        public void StopSpine()
        {
            startSpine = false;
            pressed = false;
        }


        private void Update()
        {
            if (startSpine)
            {
                foreach (SimpleScrollSnap slot in slots)
                {
                    if (slot.gameObject.activeSelf)
                    {
                        slot.Velocity += speed * Time.deltaTime * Vector2.left;
                    }
                }
            }

        }
        #endregion
    }
}