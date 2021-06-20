using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour {
    ControlParameters _controlParameters;

    private GameObject _camera0;
    private GameObject _camera1;
    private GameObject _camera2;


    private int _counter;

    void Start() {
        _controlParameters = ControlParameters.GetInstance();

        _camera0 = GameObject.Find("Camera0");
        _camera1 = GameObject.Find("Camera1");
        _camera2 = GameObject.Find("Camera2");

        _counter = 0;
    }

    void Update(){
        if ((_controlParameters._scene0_camera_switch_counter % 3) == 0) {
            _camera0.SetActive(true);
            _camera1.SetActive(false);
            _camera2.SetActive(false);
        }

        if ((_controlParameters._scene0_camera_switch_counter % 3) == 1) {
            _camera0.SetActive(false);
            _camera1.SetActive(true);
            _camera2.SetActive(false);
        }

        if ((_controlParameters._scene0_camera_switch_counter % 3) == 2) {
            _camera0.SetActive(false);
            _camera1.SetActive(false);
            _camera2.SetActive(true);
        }

    }
}
