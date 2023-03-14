using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phyllotaxis : MonoBehaviour
{

    ControlParameters _controlParameters;

    public GameObject _dot;
    public float _degree, _c;
    public int _n;
    public float _dotScale;
    private Vector2 CalculatePhyllotaxis(float degree, float scale, int count){
        double angle = count * (degree * Mathf.Deg2Rad);
        float r = scale * Mathf.Sqrt(count);

        float x = r * (float)System.Math.Cos(angle);
        float y = r * (float)System.Math.Sin(angle);
        Vector2 vec2 = new Vector2(x, y);

        return vec2;
    }
    private Vector2 _phyllotaxisPosition;


    void Start()
    {
        _controlParameters = ControlParameters.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        if (_controlParameters._scene1_flag) {
            _phyllotaxisPosition = CalculatePhyllotaxis(_degree, _c, _n);
            GameObject dotInstance = (GameObject)Instantiate(_dot);

            dotInstance.layer = 7;
            dotInstance.transform.position = new Vector3(_phyllotaxisPosition.x, _phyllotaxisPosition.y, 20);
            dotInstance.transform.localScale = new Vector3(_dotScale, _dotScale, _dotScale);
            _n++;
        }
    }
}
