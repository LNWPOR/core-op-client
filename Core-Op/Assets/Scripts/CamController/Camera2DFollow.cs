using UnityEngine;

namespace UnitySampleAssets._2D
{

    public class Camera2DFollow : MonoBehaviour
    {
    	int playerNumber = 0;
        public Transform[] target;
        public float damping = 1;
        public float lookAheadFactor = 3;
        public float lookAheadReturnSpeed = 0.5f;
        public float lookAheadMoveThreshold = 0.1f;

        private float offsetZ;
        private Vector3 lastTargetPosition;
        private Vector3 currentVelocity;
        private Vector3 lookAheadPos;

        // Use this for initialization
        private void Start()
        {
        	playerNumber = UserManager.Instance.userData.playerNumber;
            SetOffset(playerNumber);
            transform.parent = null;
        }

        // Update is called once per frame
        private void Update()
        {
        	MoveCam(playerNumber);
        }

        void SetOffset(int playerNumber){
        	lastTargetPosition = target[playerNumber].position;
			offsetZ = (transform.position - target[playerNumber].position).z;
        }

        void MoveCam(int playerNumber){
        	// only update lookahead pos if accelerating or changed direction
            float xMoveDelta = (target[playerNumber].position - lastTargetPosition).x;

            bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

            if (updateLookAheadTarget)
            {
                lookAheadPos = lookAheadFactor*Vector3.right*Mathf.Sign(xMoveDelta);
            }
            else
            {
                lookAheadPos = Vector3.MoveTowards(lookAheadPos, Vector3.zero, Time.deltaTime*lookAheadReturnSpeed);
            }

            Vector3 aheadTargetPos = target[playerNumber].position + lookAheadPos + Vector3.forward*offsetZ;
            Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref currentVelocity, damping);

            transform.position = newPos;

            lastTargetPosition = target[playerNumber].position;
        }
    }
}