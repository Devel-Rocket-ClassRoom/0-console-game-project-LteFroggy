using Framework.Engine;
using System;

public class ObstacleSpawner {
    // 배경 지형지물 생성용 팩토리클래스
    private ObstacleFactory _obstacleFactory;

    // 생성될 신
    private Scene _scene;

    // 프레임 넓이, 높이
    private int _width, _height;

    // 장애물 스폰 타이머
    private float _spawnTimer;
    private float _nextSpawnTime;

    private Random _random;

    public ObstacleSpawner(ObstacleFactory obstacleFactory, Scene scene, int width, int height) {
        _random = new Random();

        _obstacleFactory = obstacleFactory;
        _width = width;
        _height = height;
        _scene = scene;

        _spawnTimer = 0;
    }

    public BasicObstacle Update(float deltaTime) {
        BasicObstacle result = null;
        _spawnTimer += deltaTime;
        
        // 스폰시간이 되었다면, 장애물 추가
        if (_spawnTimer >= _nextSpawnTime) {
            // 랜덤 오브젝트 생성
            result = _obstacleFactory.GetRandomObstacle(_scene, _width, _height);
            // 스폰 타이머 초기화 후 다음 스폰 시간 랜덤하게 조정
            _spawnTimer = 0;
            _nextSpawnTime = 1.5f + (float)(_random.NextDouble() * 2f);
        } else { }

        return result;
    }
}
