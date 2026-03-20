
using Framework.Engine;

public class BackgroundSpawner {
    // 배경 지형지물 생성용 팩토리클래스
    private BackgroundFactory _backgroundFactory;

    // 백그라운드가 스폰될 씬
    private Scene _scene;
    // 배경 스폰 차례값
    private int _backgroundPhase = default;
    // 배경 스폰 관련값
    private const int k_SunSpawnScore = 0;
    private const int k_CloudSpawnPeriod = 2;
    private const int k_MoonSpawnScore = 12;
    private const int k_BackgroundCycle = 24;
    private bool _isCloudSpawned = false;

    // 프레임 넓이, 높이
    private int _width, _height;

    public BackgroundSpawner(BackgroundFactory backgroundFactory, Scene scene, int width, int height) {
        _backgroundFactory = backgroundFactory;
        _width = width;
        _height = height;
        _scene = scene;
    }

    public BasicBackgroundObject Update(int score) {
        BasicBackgroundObject result = null;

        // 18점마다 사이클 반복
        int _scoreInCycle = score % k_BackgroundCycle;

        // 해 스폰, 달 스폰 시점 제외 구름 반복 생성
        if (!_isCloudSpawned
            && _scoreInCycle % k_CloudSpawnPeriod == 0 
            && _scoreInCycle != k_SunSpawnScore 
            && _scoreInCycle != k_MoonSpawnScore) {
            result = _backgroundFactory.MakeCloud(_scene, _width, _height);
            _isCloudSpawned = true;
        } else if (_scoreInCycle % k_CloudSpawnPeriod != 0) {
            _isCloudSpawned = false;
        }
        // 해 등장
        if (_backgroundPhase == 0 
                && _scoreInCycle >= k_SunSpawnScore) {
            result = _backgroundFactory.MakeSun(_scene, _width, _height);
            _backgroundPhase = 1;
        } 
        // 달 등장
        else if (_backgroundPhase == 1 
                && _scoreInCycle >= k_MoonSpawnScore) {
            result = _backgroundFactory.MakeMoon(_scene, _width, _height);
            _backgroundPhase = 2;
        }
        // 사이클 초기화
        if (_scoreInCycle == 0 && _backgroundPhase == 2) {
            _backgroundPhase = 0;
        }

        return result;
    }
}