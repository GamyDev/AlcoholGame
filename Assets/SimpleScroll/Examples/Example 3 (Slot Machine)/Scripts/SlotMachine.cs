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

        private int[] speedArr = new int[] { 12000, 13000, 14000, 15000 };
        private int speedIndex;

        int[] MixArray(int[] num)
        {
            for (int i = 0; i < num.Length; i++)
            {
                int currentValue = num[i];
                int randomIndex = Random.Range(i, num.Length);
                num[i] = num[randomIndex];
                num[randomIndex] = currentValue;
            }

            return num;
        }

        public void Spin()
        {
            if (!pressed)
            {
                speedArr = MixArray(speedArr);

                int currSpeed = Random.Range(0, speedArr.Length);
                while (currSpeed == speedIndex)
                {
                    currSpeed = Random.Range(0, speedArr.Length);
                }

                speedIndex = currSpeed;

                Debug.Log($"Speed {speedArr[speedIndex]}");
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
                        slot.Velocity += speedArr[speedIndex] * Time.deltaTime * Vector2.left;
                    }
                }
            }

        }
        #endregion
    }
}