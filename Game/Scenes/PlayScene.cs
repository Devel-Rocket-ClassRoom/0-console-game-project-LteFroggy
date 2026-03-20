using Framework.Engine;
using System;
using System.Collections.Generic;

namespace Framework.MyGame
{
    public class PlayScene : Scene
    {
        private GameState _gameState = GameState.Playing;
        private int _score;

        // 장애물 생성용 팩토리클래스
        private ObstacleFactory _obstacleFactory;
        // 게임 종료 시 출력될 스트링 (차에 치었습니다 등)
        private string _gameOverString;
        
        // 게임 출력 화면 너비 (지면 렌더링 시 사용)
        private int _width;
        // 게임 출력 화면 높이 (해, 달 등 오브젝트 렌더링 시 사용)
        private int _height;
        // 게임 시작 후 지난 시간
        private float _elapsedTime;
        // 장애물 스폰 타이머
        private float _spawnTimer;
        private float _nextSpawnTime;

        private Random _rand = new Random();

        private float _acceleration => 15f + _elapsedTime * 0.5f;
        public event GameAction PlayAgainRequested;
        
        public PlayScene(int width, int height, ObstacleFactory factory) {
            _score = 0;
            _elapsedTime = 0;
            _width = width;
            _height = height;

            _obstacleFactory = factory;
        }

        public override void Load()
        {
            AddGameObject(new Dinosour(this)); 
            ObstacleRemoveEvent += (AddScore);
        }

        private void AddScore() {
            _score++;
        }

        public override void Update(float deltaTime)
        {
            if (_gameState == GameState.Playing) {
                _elapsedTime += deltaTime;
                _spawnTimer += deltaTime;
                UpdateGameObjects(deltaTime, _acceleration);
            
                // 장애물 스폰
                TrySpawn();

                // 충돌체크
                CheckCollision();
            }

            // 게임 오버 후라면, 업데이트는 중지
            else { }
        }

        public override void Draw(ScreenBuffer buffer)
        {
            // 가로줄 그리기 (바닥)
            buffer.DrawHLine(0, 19, _width, '-', ConsoleColor.DarkYellow);
            // 점수판
            buffer.WriteText(1, 1, $"점수 : {_score}점");
            // 오브젝트
            DrawGameObjects(buffer);

            // 게임 오버 후라면, 게임오버 대사 출력
            if (_gameState == GameState.GameOver) {
                buffer.WriteText(15, 6, _gameOverString);
            }
        }

        public override void Unload()
        {
            ClearGameObjects();
        }

        private void TrySpawn() {
            // 스폰시간이 되었다면, 장애물 추가
            if (_spawnTimer >= _nextSpawnTime) {
                // 랜덤 오브젝트 생성
                AddGameObject(_obstacleFactory.GetRandomObstacle(_rand.Next(1, 100), this, _width, _height));
                _spawnTimer = 0;
                _nextSpawnTime = _elapsedTime + 0.5f + (float)(_rand.NextDouble() * 2f);
            } else { }
        }

        private void CheckCollision() {
            IReadOnlyList<GameObject> objects = GameObjects;

            // 공룡의 현위치 파악
            int dinoXStart = objects[0].XLoc;
            int dinoXEnd = dinoXStart + objects[0].CollisionWidth;
            int dinoYStart = objects[0].YLoc;
            int dinoYEnd = dinoYStart + objects[0].CollisionHeight;

            // 0번이 공룡, 나머지는 모두 장애물임. 따라서 0번과 나머지만 비교
            for (int i = 1; i < objects.Count; i++) {
                int obstacleXStart = objects[i].XLoc;
                int obstacleXEnd = obstacleXStart + objects[i].CollisionWidth;

                int obstacleYStart = objects[i].YLoc;
                int obstacleYEnd = obstacleYStart + objects[i].CollisionHeight;

                // 공룡이 장애물보다 왼쪽에 있는 경우
                if (dinoXEnd < obstacleXStart) { continue; }
                // 공룡이 장애물보다 오른쪽에 있는 경우
                else if (dinoXStart > obstacleXEnd) { continue; }
                // 공룡이 장애물보다 위에 있는 경우
                else if (dinoYEnd < obstacleYStart) { continue; }
                // 공룡이 장애물보다 아래에 있는 경우
                else if (dinoYStart > obstacleYEnd) { continue; }
                // 다 아니라면 충돌
                else {
                    _gameState = GameState.GameOver;
                    _gameOverString = ((BasicObstacle)objects[i]).GameOverString;
                    break;
                }
            }
        }
    }
}