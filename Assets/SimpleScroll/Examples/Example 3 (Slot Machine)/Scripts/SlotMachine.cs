﻿// Simple Scroll-Snap - https://assetstore.unity.com/packages/tools/gui/simple-scroll-snap-140884
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
        //        slot.Velocity += Random.Range(2500, 5000) * Vector2.up;
        //    }
        //}

        public void Spin()
        {
            if (!pressed)
            {
                startSpine = true;
                Invoke("StopSpine", 2);
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
                        slot.Velocity += Random.Range(25000 * Time.deltaTime, 50000 * Time.deltaTime) * Vector2.left;
                    }
                }
            }

        }
        #endregion
    }
}