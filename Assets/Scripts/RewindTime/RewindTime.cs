using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace RewindTime
{ 
    public class RewindTime : MonoBehaviour
    {
        private class Point
        {
            public Vector3 Position { get; }
            public Quaternion Rotation { get; }

            public Point(Vector3 position, Quaternion rotation)
            {
                Position = position;
                Rotation = rotation;
            }
        }

        private Rigidbody rb;
        private List<Point> points;
        private bool isRewinding = false;
        [SerializeField] float timeMultiplier = .5f;
        
        private void Start()
        {
            points = new List<Point>();
            rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
                StartRewinding();
            if (Input.GetKeyUp(KeyCode.R))
                StopRewinding();
        }
        
        private void FixedUpdate()
        {
            if (isRewinding)
                Rewind();
            else
                Record();
        }

        private void Record()
        {
            if (points.Count > Mathf.Round(5f / Time.fixedDeltaTime))
                points.RemoveAt(points.Count - 1);

            var newPoint = transform;
            points.Insert(0, new Point(newPoint.position, newPoint.rotation));
        }

        private void StopRewinding()
        {
            isRewinding = false;
            rb.isKinematic = false;
            timeMultiplier = 1f;
            Time.timeScale = timeMultiplier;
        }

        private void StartRewinding()
        {
            timeMultiplier = .5f;
            isRewinding = true;
            rb.isKinematic = true;
        }

        private void Rewind()
        {
            Time.timeScale = timeMultiplier;
            
            if (points.Count > 0)
            {
                timeMultiplier += 0.02f;
                transform.position = points[0].Position;
                transform.rotation = points[0].Rotation;
                points.RemoveAt(0);
            }
            else
                StopRewinding();
        }
    }
}

