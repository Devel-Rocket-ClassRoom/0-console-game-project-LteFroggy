using Framework.Engine;
using System;
using System.Collections.Generic;

namespace Framework.MyGame
{
    public class PlayScene : Scene
    {
        private List<GameObject> _obstacles;
        private List<GameObject> _pendingRemovalObstacles;
        private GameObject _player;
        private GameState _gameState = GameState.Playing;
        private int _score;
        // 게임 종료 시 출력될 스트링 (차에 치었습니다 등)
        private string _gameOverString;

        // 장애물 생성용 팩토리클래스
        private ObstacleFactory _obstacleFactory;
        // 배경 지형지물 생성용 팩토리클래스
        private BackgroundSpawner _backgroundSpawner;
        
        // 게임 출력 화면 너비 (지면 렌더링 시 사용)
        private readonly int _width;
        // 게임 출력 화면 높이 (해, 달 등 오브젝트 렌더링 시 사용)
        private readonly int _height;
        // 장애물 스폰 타이머
        private float _spawnTimer;
        private float _nextSpawnTime;

        private readonly Random _rand = new Random();
        private float _acceleration => 30f + _score * 2;

        public event GameAction PlayAgainRequested;
        public event GameAction BackToMain;
        
        public PlayScene(int width, int height, ObstacleFactory factory, BackgroundFactory backgroundFactory) {
            _score = 0;
            _width = width;
            _height = height;

            _obstacleFactory = factory;
            _backgroundSpawner = new BackgroundSpawner(backgroundFactory, this, _width, _height);

            _pendingRemovalObstacles = new List<GameObject>();
            _obstacles = new List<GameObject>();
        }

        public override void Load()
        {
            GameObject player = new Dinosour(this);
            _player = player;
            AddGameObject(player); 
        }

        public override void Update(float deltaTime)
        {
            if (_gameState == GameState.Playing) {
                _spawnTimer += deltaTime;
                UpdateGameObjects(deltaTime, _acceleration);

                // 지형지물 스폰
                BasicBackgroundObject backgroundObject = _backgroundSpawner.Update(_score);
                if (backgroundObject != null) { AddGameObject(backgroundObject); }
            
                // 장애물 스폰
                SpawnObstacle();

                // 충돌체크
                CheckCollision();

                // 넘어간 장애물 삭제
                DespawnObstacle();
            }

            // 게임 오버 후라면, 업데이트는 중지
            else { 
                // 스페이스 누르면 재시작
                if (Input.IsKeyDown(ConsoleKey.Spacebar)) {
                    _gameState = GameState.Playing;
                    PlayAgainRequested();
                }
                // 백스페이스 눌러서 메인으로
                else if (Input.IsKeyDown(ConsoleKey.Backspace)) {
                    BackToMain();
                }
            }
        }

        public override void Draw(ScreenBuffer buffer)
        {
            // 가로줄 그리기 (바닥)
            buffer.DrawHLine(0, 19, _width, '-', ConsoleColor.DarkYellow);
            // 점수판
            buffer.WriteText(0, 0, $"넘은 장애물 수 : {_score}개");
            // 오브젝트
            DrawGameObjects(buffer);

            // 게임 오버 후라면, 게임오버 대사 출력
            if (_gameState == GameState.GameOver) {
                buffer.WriteText(5, 9, _gameOverString);
                buffer.WriteTextCentered(2, $"다시 시작하려면 SPACE를 눌러주세요.");
                buffer.WriteTextCentered(3, $"BACKSPACE를 눌러 메인화면으로 돌아갑니다.");
            }
        }

        public override void Unload() {
            ClearGameObjects();
        }

        private void SpawnObstacle() {
            // 스폰시간이 되었다면, 장애물 추가
            if (_spawnTimer >= _nextSpawnTime) {
                // 랜덤 오브젝트 생성
                GameObject obj = _obstacleFactory.GetRandomObstacle(this, _width, _height);
                AddGameObject(obj);
                AddObstacle(obj);
                _spawnTimer = 0;
                _nextSpawnTime = 1.5f + (float)(_rand.NextDouble() * 2f);
            }
            // 시간이 되지 않았다면, 스킵
            else { }
        }

        private void DespawnObstacle() {
            // 공룡 위치와 나머지를 모두 비교
            foreach (var obstacle in _obstacles) {
                // inActive인 값은 삭제할 것
                if (!obstacle.IsActive) {
                    _pendingRemovalObstacles.Add(obstacle);
                    continue;
                }
            }

            // 삭제
            foreach (var target in _pendingRemovalObstacles) {
                _obstacles.Remove(target);
                _score++;
            }
            _pendingRemovalObstacles.Clear();
        }

        private void CheckCollision() {
            // 공룡의 현위치 파악
            int dinoXStart = _player.XLoc;
            int dinoXEnd = dinoXStart + _player.CollisionWidth;
            int dinoYStart = _player.YLoc;
            int dinoYEnd = dinoYStart + _player.CollisionHeight;

            // 공룡 위치와 장애물 위치 비교
            foreach (var obstacle in _obstacles) {
                int obstacleXStart = obstacle.XLoc;
                int obstacleXEnd = obstacleXStart + obstacle.CollisionWidth;
                int obstacleYStart = obstacle.YLoc;
                int obstacleYEnd = obstacleYStart + obstacle.CollisionHeight;

                // 공룡이 장애물보다 왼쪽에 있는 경우
                if (dinoXEnd < obstacleXStart) { continue; }
                // 공룡이 장애물보다 오른쪽에 있는 경우
                else if (dinoXStart > obstacleXEnd) { continue; }
                // 공룡이 장애물보다 위에 있는 경우
                else if (dinoYEnd < obstacleYStart) { continue; }
                // 공룡이 장애물보다 아래에 있는 경우
                else if (dinoYStart > obstacleYEnd) { continue; }
                // 다 아니라면 충돌
                //else {
                //    _gameState = GameState.GameOver;
                //    _gameOverString = ((BasicObstacle)obstacle).GameOverString;
                //    break;
                //}
            }
        }

        private void AddObstacle(GameObject obstacle) {
            _obstacles.Add(obstacle);
        }
    }
}