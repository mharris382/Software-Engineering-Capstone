using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    [RequireComponent(typeof(LineRenderer))]
    public class Rope : MonoBehaviour
    {
        private struct RopeSegment
        {
            public Vector2 currentPosition;

            public Vector2 previousPosition;

            public RopeSegment(Vector2 position)
            {
                previousPosition = position;
                currentPosition = position;
            }
        }

        private RopeSegment _pivotSegment;

        [SerializeField] private int _segmentCount = 35;
        [SerializeField] private Transform _startPoint;
        [SerializeField] private Transform _endPoint;

        private float _ropeSegmentLength = 0.25f;

        public Transform StartPoint
        {
            get { return _startPoint; }
            set { _startPoint = value; }
        }

        public Transform EndPoint
        {
            get { return _endPoint; }
            set { _endPoint = value; }
        }

        private LineRenderer _lineRenderer;
        public LineRenderer LineRenderer => _lineRenderer;
        private List<RopeSegment> _ropeSegments = new List<RopeSegment>();
        [SerializeField] private float gravityScale = 1;

        void Start()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            InitializeRope();
        }

        void Update()
        {
            DrawRope();
            Simulate();
        }

        void DrawRope()
        {
            try {
                Vector3[] array = new Vector3[_segmentCount];
                for (int i = 0; i < _segmentCount; i++) {
                    array[i] = _ropeSegments[i].currentPosition;
                }

                _lineRenderer.positionCount = array.Length;
                _lineRenderer.SetPositions(array);
            }
            catch (Exception value) {
                Console.WriteLine(value);
            }
        }

        void InitializeRope()
        {
            Vector3 localPosition = _startPoint.localPosition;
            for (int i = 0; i < _segmentCount; i++) {
                _ropeSegments.Add(new RopeSegment(localPosition));
                localPosition.y -= _ropeSegmentLength;
            }
        }


        private void Simulate()
        {
            try {
                Vector2 vector = new Vector2(0f, -1f);
                for (int i = 0; i < _segmentCount; i++) {
                    RopeSegment value = _ropeSegments[i];
                    Vector2 vector2 = value.currentPosition - value.previousPosition;
                    value.previousPosition = value.currentPosition;
                    value.currentPosition += vector2;
                    value.currentPosition += vector * Time.deltaTime;
                    _ropeSegments[i] = value;
                }

                for (int j = 0; j < 200; j++) {
                    ApplyConstraints();
                }
            }
            catch (Exception value2) {
                Console.WriteLine(value2);
            }
        }

        private void ApplyConstraints()
        {
            RopeSegment firstSegment = _ropeSegments[0];
            RopeSegment lastSegment = _ropeSegments[_segmentCount - 1];

            //set start and end points directly
            firstSegment.currentPosition = _startPoint.position;
            lastSegment.currentPosition = _endPoint.position;

            _ropeSegments[0] = firstSegment;
            _ropeSegments[_segmentCount - 1] = lastSegment;

            for (int i = 0; i < _segmentCount - 1; i++) {
                RopeSegment curSeg = _ropeSegments[i];
                RopeSegment nexSeg = _ropeSegments[i + 1];
                //rope segment length is constant
                var dx = (curSeg.currentPosition - nexSeg.currentPosition);
                float
                    num = dx.magnitude -
                          _ropeSegmentLength; //difference between change in position along rope and segment length constant
                Vector2 vector = dx.normalized * num; //direction of segment, length of difference segment and constant
                if (i != 0) {
                    //translate the segments towards so that the segment length is equal to the constant?
                    curSeg.currentPosition -= vector * 0.5f;
                    nexSeg.currentPosition += vector * 0.5f;
                    _ropeSegments[i] = curSeg;
                    _ropeSegments[i + 1] = nexSeg;
                }
                else {
                    nexSeg.currentPosition += vector;
                    _ropeSegments[i + 1] = nexSeg;
                }
            }
        }

        ///My refactoring of original simulate method 
        private void Simulate(float deltaTime)
        {
            try {
                Vector2 gravityVector = new Vector2(0f, -1f * this.gravityScale);
                for (int i = 0; i < _segmentCount; i++) {
                    RopeSegment value = _ropeSegments[i];
                    Vector2 dx = value.currentPosition - value.previousPosition; //get change in position on last frame
                    value.previousPosition = value.currentPosition;
                    value.currentPosition +=
                        dx; //apply the change in position from previous frame to current frame, essentially continue the change in position
                    value.currentPosition += gravityVector * deltaTime; //apply change in position due to gravity
                    _ropeSegments[i] = value;
                }

                for (int j = 0; j < 200; j++) {
                    ApplyConstraints();
                }
            }
            catch (Exception value2) {
                Console.WriteLine(value2);
            }
        }
    }
}