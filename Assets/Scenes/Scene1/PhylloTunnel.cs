using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhylloTunnel : MonoBehaviour
{
    public Transform _tunnel;
    public float _tunnelInitialSpeed, _tunnelSpeed, _cameraDistance;
    ControlParameters _controlParameters;

    // Start is called before the first frame update
    void Start()
    {
        _controlParameters = ControlParameters.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        if (_controlParameters._rawAudio == null)  return;
        _tunnel.position = new Vector3(_tunnel.position.x, _tunnel.position.y,
          _tunnel.position.z + ((_controlParameters._rawAudio[0] + _tunnelInitialSpeed) * _tunnelSpeed));
        //_tunnel.position = new Vector3(_tunnel.position.x, _tunnel.position.y,
          //_tunnel.position.z + ((_controlParameters._freqBand[0] + _tunnelInitialSpeed) * _tunnelSpeed));

        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, _tunnel.position.z + _cameraDistance);
    }
}
