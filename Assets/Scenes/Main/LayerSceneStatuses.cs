using UnityEngine;


public enum RenderSceneCommand {
    Nothing = 0, // No nothing
    Show = 1, // Show specified scene
    Hide = 2 // Hide specified scene
}

public enum SceneStatus {
    Hidden = 0, // Scene is hidden 
    Showed = 1 // Scene is showed
}

public class LayerSceneStatuses {

    public int _activeSceneNum;

    private SceneStatus[] _scenesLayer0;
    private SceneStatus[] _scenesLayer1;


    // シーンを有効にするか無効にするかのsignalを保持
    RenderSceneCommand[] _commandsLayer0;
    RenderSceneCommand[] _commandsLayer1;

    private static LayerSceneStatuses _singleInstance = new LayerSceneStatuses();

    public static LayerSceneStatuses GetInstance() {
        return _singleInstance; 
    }

    private LayerSceneStatuses() {
        _activeSceneNum = 4;
        _scenesLayer0 = new SceneStatus[_activeSceneNum];
        _scenesLayer1 = new SceneStatus[_activeSceneNum];
        _commandsLayer0 = new RenderSceneCommand[_activeSceneNum];
        _commandsLayer1 = new RenderSceneCommand[_activeSceneNum];

        for (int i = 0; i < _activeSceneNum; i++) {
            _scenesLayer0[i] = SceneStatus.Hidden;
            _scenesLayer1[i] = SceneStatus.Hidden;
            // 起動時は表示なにも表示しない
            _commandsLayer0[i] = RenderSceneCommand.Hide;
            _commandsLayer1[i] = RenderSceneCommand.Hide;
        }
    }


    public RenderSceneCommand GetLayerSceneCommand(int layerNo, int sceneNo) {
        if (layerNo == 0) return _commandsLayer0[sceneNo];
        if (layerNo == 1) return _commandsLayer1[sceneNo];
        return RenderSceneCommand.Nothing;
    }

    public void ResetLayerSceneCommand(int layerNo, int sceneNo) {
        if (layerNo == 0) _commandsLayer0[sceneNo] = RenderSceneCommand.Nothing;
        if (layerNo == 1) _commandsLayer1[sceneNo] = RenderSceneCommand.Nothing;
    }

    public void ToggleSceneStatus(int layerNo, int sceneNo) {
        if (layerNo == 0) UpdateCommand(_scenesLayer0, _commandsLayer0, sceneNo);
        if (layerNo == 1) UpdateCommand(_scenesLayer1, _commandsLayer1, sceneNo);
    }

    public SceneStatus GetSceneStatus(int layerNo, int sceneNo) {
        SceneStatus status = SceneStatus.Hidden;
        if (layerNo == 0) { status = _scenesLayer0[sceneNo]; }
        if (layerNo == 1) { status = _scenesLayer1[sceneNo]; }

        return status;
    }

    void UpdateCommand(SceneStatus[] sceneStatuses, RenderSceneCommand[] command, int sceneNo) {
        SceneStatus currentStatus = sceneStatuses[sceneNo];
        SceneStatus nexStatus = 
            (currentStatus == SceneStatus.Hidden) ? SceneStatus.Showed : SceneStatus.Hidden;

        // シーンのRenderTextureの状態を変更する命令を更新
        int changed = CheckStatusChanged(currentStatus, nexStatus);
        if (changed > 0) command[sceneNo] = RenderSceneCommand.Show;
        if (changed < 0) command[sceneNo] = RenderSceneCommand.Hide;

        // シーンの状態を更新
        sceneStatuses[sceneNo] = nexStatus;
    }

    int CheckStatusChanged(SceneStatus current, SceneStatus next) {
        // Showed(1) - Hidden(0) = 1 となり有効にする
        // Hidden(0) - Showed(1) = -1 となり無効にする
        return next - current;
    }

}
