using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//const float _sideLength = 0.5f;


public class CubeDetail {
    public Vector3 _pos; 

    public Vector3 _size;

    public float _v;
    public float _t;

    public float _sideLength;

    public CubeDetail(int i, int j, Vector3 center) {
        _sideLength = 0.5f;
        _size.x = _sideLength;
        _size.z = _sideLength;
        _size.y = Random.Range(0f, 50.0f);

        _pos = new Vector3(_sideLength * i, 0, _sideLength * j) - center;

        _v = Random.Range(-1.0f, 1.0f);
    }

    public void MoveOn(float moveDistance) {
        _pos.x += moveDistance; 
    }

    public void UpdateHeight() {
        float nextHeight = _size.y + _v;
        if (nextHeight > 50.0f) {
            nextHeight = 50.0f;
            _v = Random.Range(-0.9f, -0.2f) * 0.7f;
        }

        if (nextHeight < 0.0f) {
            nextHeight = 0.0f;
            _v = Random.Range(0.2f, 0.9f) * 0.7f;
        }
        _size.y = nextHeight;
    }

    public void UpdateHeightCircle(float minX, float maxX, float minZ, float maxZ) {
        float wholeLengthX = Mathf.Abs(minX) + Mathf.Abs(maxX) / 2.0f;
        float wholeLengthZ = Mathf.Abs(minZ) + Mathf.Abs(maxZ) / 2.0f;

        Vector3 uv = new Vector3(_pos.x / wholeLengthX, 0.0f, _pos.z / wholeLengthZ);

        float lengthFromCenter = Mathf.Sqrt(Mathf.Pow(uv.x, 2.0f) + Mathf.Pow(uv.z, 2.0f));
        float tmpY = 10.0f / Mathf.Sin(30.0f * (Time.time * 0.05f - lengthFromCenter));
        tmpY = tmpY < 0 ? -tmpY : tmpY;
        _size.y = Mathf.Clamp(
            tmpY, 
            0.01f,
            50.0f
        );
    }

    public void ResetPosition(float baseMaxX, float currentMinX) {
        float nextPosX = _pos.x;
        if (nextPosX > baseMaxX) {
            nextPosX = currentMinX - _sideLength / 2.0f;
        }
        _pos = new Vector3(nextPosX, _pos.y, _pos.z);
    }

    public void SetSpectrumHeight(float baseMaxX, float currentMinX, float volume) {
        float nextPosX = _pos.x;
        // reset postionと同じ条件のときに高さを更新
        if (nextPosX > baseMaxX) {
            _size.y = volume * 10.0f;
        }
    }

    private float Fract(float value) {
        return value - Mathf.FloorToInt(value);
    }

}

public class CubeRoot {

    private int _cubeSideNum;
    public CubeDetail[,] _cubeDetails { get; set; }

    private Vector3 _center;
    private float _sideLength;
    private float _baseMinX;
    private float _baseMaxX;
    private float _baseMinZ;
    private float _baseMaxZ;
    private float _currentMinX;
    private float _currentMaxX;
    private float _currentMinZ;
    private float _currentMaxZ;

    public CubeRoot(int cubeSideNum) {
        _cubeSideNum = cubeSideNum; 
        _sideLength = 0.5f;

        _baseMinX = 0.0f; // 一番小さいcubeのx(生成時の基準位置)
        _baseMaxX = 0.0f; // 一番大きいcubeのx(生成時の基準位置)
        _baseMinZ = 0.0f; // 一番小さいcubeのz(生成時の基準位置)
        _baseMaxZ = 0.0f; // 一番大きいcubeのz(生成時の基準位置)
        _currentMinX = 0.0f; // 一番小さいcubeのx(フレーム毎)
        _currentMaxX = 0.0f; // 一番大きいcubeのx(フレーム毎)
        _currentMinZ = 0.0f; // 一番小さいcubeのz(フレーム毎)
        _currentMaxZ = 0.0f; // 一番大きいcubeのz(フレーム毎)


        _cubeDetails = new CubeDetail[_cubeSideNum, _cubeSideNum];
        _center = new Vector3((_cubeSideNum * _sideLength / 2.0f), 0, (_cubeSideNum * _sideLength / 2.0f));

        for(int i = 0; i < _cubeSideNum; i++) {
            for(int j = 0; j < _cubeSideNum; j++) {
                _cubeDetails[i,j] = new CubeDetail(i, j, _center);
                UpdateBasePosition(_cubeDetails[i,j]._pos); 
            }
        }
    }


    public void UpdateEdgeCubePositionX(int i){
        _currentMinX = _cubeDetails[i, 0]._pos.x < _baseMinX ? _cubeDetails[i, 0]._pos.x : _baseMinX;
        _currentMaxX = _cubeDetails[i, 0]._pos.x > _baseMaxX ? _cubeDetails[i, 0]._pos.x : _baseMaxX;
    }

    public void UpdateEdgeCubePositionZ(int j) {
        _currentMinZ = _cubeDetails[0, j]._pos.z < _baseMinZ ? _cubeDetails[0, j]._pos.z : _baseMinZ;
        _currentMaxZ = _cubeDetails[0, j]._pos.z > _baseMaxZ ? _cubeDetails[0, j]._pos.z : _baseMaxZ;
    }

    public void MoveOn(int i, int j, float moveDistance = 0.2f) {
        _cubeDetails[i, j].MoveOn(moveDistance);
    }

    public void UpdateHeight(int i, int j) {
        _cubeDetails[i, j].UpdateHeight();
    }

    public void UpdateHeightCircle(int i, int j) {
        _cubeDetails[i, j].UpdateHeightCircle(_currentMinX, _currentMaxX, _currentMinZ, _currentMaxZ);
    }

    public void ResetPosition(int i, int j) {
        _cubeDetails[i, j].ResetPosition(_baseMaxX, _currentMinX);
    }

    public void SetSpectrum(float[] pSpectrum, int i, int j) {
        int index = pSpectrum.Length / _cubeSideNum;
        _cubeDetails[i, j].SetSpectrumHeight(_baseMaxX, _currentMinX, pSpectrum[j * index]);
    }
    private void UpdateBasePosition(Vector3 cubePos){
        _baseMinX = cubePos.x < _baseMinX ? cubePos.x : _baseMinX;
        _baseMaxX = cubePos.x > _baseMaxX ? cubePos.x : _baseMaxX;

        _baseMinZ = cubePos.z < _baseMinZ ? cubePos.x : _baseMinZ;
        _baseMaxZ = cubePos.z > _baseMaxZ ? cubePos.x : _baseMaxZ;
    }

}

public class Scene0Controller : MonoBehaviour {


    [SerializeField] bool _enableAudioInput;

    [SerializeField] float _audioBoost;

    ControlParameters _controlParameters;


    [SerializeField] public GameObject _cubePrefab;
    [SerializeField] public Material _cubeMaterial;

    [SerializeField] public Transform _parentTran;

    private GameObject[,] _cubes;

    private CubeRoot _cubeRoot;

    private int _cubeSideNum;

    void Start() {

        _controlParameters = ControlParameters.GetInstance();
        _audioBoost = 1.0f;
        _cubeSideNum = 64;

        _cubes = new GameObject[_cubeSideNum, _cubeSideNum];
        _cubeRoot = new CubeRoot(_cubeSideNum);

        for(int i = 0; i < _cubeSideNum; i++) {
            for(int j = 0; j < _cubeSideNum; j++) {
                _cubes[i, j] = Instantiate(_cubePrefab, _cubeRoot._cubeDetails[i,j]._pos, Quaternion.identity);
                _cubes[i, j].gameObject.GetComponentInChildren<MeshRenderer>().material = _cubeMaterial;

                // 親オブジェクトを設定してhierarchyをきれいに
                _cubes[i,j].transform.SetParent(_parentTran);

            }
        }
    }


    void Update(){
        for(int i = 0; i < _cubeSideNum; i++) {
            _cubeRoot.UpdateEdgeCubePositionX(i);
            _cubeRoot.UpdateEdgeCubePositionZ(i);
        }

        for(int i = 0; i < _cubeSideNum; i++) {
            for(int j = 0; j < _cubeSideNum; j++) {
                switch (_controlParameters._scene0_move_type) {
                    case 0:
                        _cubeRoot.MoveOn(i, j);
                        _cubeRoot.UpdateHeight(i, j);
                        break;

                    case 1:
                        _cubeRoot.MoveOn(i, j, 0.05f);
                        _cubeRoot.UpdateHeightCircle(i, j);
                        break;

                    case 2:
                        _cubeRoot.MoveOn(i, j);
                        //_cubeRoot.SetSpectrum(_controlParameters._spectrum, i, j);
                        _cubeRoot.SetSpectrum(_controlParameters._rawAudio, i, j);
                        break;

                    default:
                        _cubeRoot.UpdateHeight(i, j);
                        break;
                }
                //_cubeRoot.MoveOn(i, j);
                //_cubeRoot.UpdateHeight(i, j);
                //_cubeRoot.SetSpectrum(_controlParameters._spectrum, i, j);
                //_cubeRoot.UpdateHeightCircle(i, j);
            }
        }

        for(int i = 0; i < _cubeSideNum; i++) {
            for(int j = 0; j < _cubeSideNum; j ++) {
                _cubeRoot.ResetPosition(i, j);
                _cubes[i, j].transform.position = _cubeRoot._cubeDetails[i, j]._pos;
                _cubes[i, j].transform.localScale = _cubeRoot._cubeDetails[i, j]._size;
            }
        }
    }
}
