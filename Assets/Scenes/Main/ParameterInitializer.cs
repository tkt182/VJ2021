using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ParameterInitializer : MonoBehaviour {

    LayerSceneStatuses _layerSceneStatuses;
    ControlParameters _controlParameters;
    OutputLayers _outputLayers;


    void Awake() {
        // 一応ここでグローバルにアクセスしたいシングルトンインスタンスを生成
        // 把握するためにリスト化しているだけ...
        _layerSceneStatuses = LayerSceneStatuses.GetInstance();
        _controlParameters = ControlParameters.GetInstance();
        _outputLayers = OutputLayers.GetInstance();


        SceneManager.LoadScene("Scene0", LoadSceneMode.Additive);
        SceneManager.LoadScene("Scene1", LoadSceneMode.Additive);
        SceneManager.LoadScene("Scene2", LoadSceneMode.Additive);
        SceneManager.LoadScene("Scene3", LoadSceneMode.Additive);
    }

}
