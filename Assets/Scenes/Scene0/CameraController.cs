using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;


public class CameraController : MonoBehaviour {

    //[SerializeField] public Transform _cameraTarget;

    public PathCreator path01;

    public PathCreator path02;

    public EndOfPathInstruction end;
    [SerializeField] public float speed;
    [SerializeField] float dstTravelled;

    private Camera _moveCamera;
    private PathCreator[] paths;
    private int userPathCounter;

    void Start() {
        //this.gameObject.transform.LookAt(_cameraTarget);
        paths = new PathCreator[2];
        paths[0] = path01;
        paths[1] = path02;

        userPathCounter = 0;
    }

    void Update() {

        _moveCamera = this.GetComponentInChildren<Camera>();
        if (_moveCamera.isActiveAndEnabled) {

            PathCreator pathCreator;
            switch(userPathCounter % 2) {
                case 0:
                    pathCreator = path01;
                    break;
                case 1:
                    pathCreator = path02;
                    break;
                default:
                    pathCreator = path01;
                    break;
            }

            dstTravelled += speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(dstTravelled, end);
            transform.rotation = pathCreator.path.GetRotationAtDistance(dstTravelled, end);

            // 現在位置がpathの0.0 ~ 1.0の間のどこにあるか
            float threshold = pathCreator.path.GetClosestTimeOnPath(transform.position);
            if (threshold > 0.98) {
                userPathCounter++;
                dstTravelled = 0.0f;
            }

        }
    }
}
