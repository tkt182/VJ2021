public enum DisplayedLayer {
    Layer0 = 0,
    Layer1 = 1
}

public sealed class OutputLayers {

    public DisplayedLayer _displayedLayer { get; set; } 

    static OutputLayers _singleInstance = new OutputLayers();

    public static OutputLayers GetInstance() {
        return _singleInstance;
    }

    private OutputLayers() {
        _displayedLayer = DisplayedLayer.Layer0;
    }

}
