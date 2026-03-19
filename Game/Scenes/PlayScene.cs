using Framework.Engine;
using System;
using System.Collections.Generic;

namespace Framework.MyGame
{
    public class PlayScene : Scene
    {
        private GameState _gameState = GameState.Playing;
        private string _gameOverString;

        private int _score;

        private int _width;
        private int _height;
        private float _elapsedTime;
        private float _spawnTimer;
        private float _nextSpawnTime;

        private Random _rand = new Random();

        private float _accerlation => _elapsedTime + 15f;
        public event GameAction PlayAgainRequested;
        
        public PlayScene(int width, int height) {
            _score = 0;
            _elapsedTime = 0;
            _width = width;
            _height = height;
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
                UpdateGameObjects(deltaTime, _accerlation);
            
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
            buffer.DrawHLine(0, 19, 80);
            // 점수판
            buffer.WriteTextCentered(5, $"Score : {_score}");

            // 테스트
            buffer.WriteText(0, 2, $"GameState : {_gameState}");

            DrawGameObjects(buffer);

            // 게임 오버 후라면, 게임오버 대사 출력
            if (_gameState == GameState.GameOver) {
                buffer.WriteTextCentered(10, _gameOverString);
            }
            
        }

        public override void Unload()
        {
            ClearGameObjects();
        }

        private void TrySpawn() {
            // 스폰시간이 되었다면, 장애물 추가
            if (_spawnTimer >= _nextSpawnTime) {
                // 테스트코드
                AddGameObject(new Fence(this, _width, _height));
                _spawnTimer = 0;
                _nextSpawnTime = (_elapsedTime / 100) + (float)(_rand.NextDouble() * 3) + 1;
            } else { }
        }

        private void CheckCollision() {
            IReadOnlyList<GameObject> objects = GameObjects;

            // 공룡의 현위치 파악
            int dinoXStart = objects[0].XLoc;
            int dinoXEnd = dinoXStart + objects[0].Width;

            int dinoYStart = objects[0].YLoc;
            int dinoYEnd = dinoYStart + objects[0].Height;

            // 0번이 공룡, 나머지는 모두 장애물임. 따라서 0번과 나머지만 비교
            for (int i = 1; i < objects.Count; i++) {
                int obstacleXStart = objects[i].XLoc;
                int obstacleXEnd = obstacleXStart + objects[i].Width;

                int obstacleYStart = objects[i].YLoc;
                int obstacleYEnd = obstacleYStart + objects[i].Height;

                // 공룡이 장애물보다 왼쪽에 있는 경우
                if (dinoXEnd < obstacleXStart) { continue; }
                // 공룡이 장애물보다 오른쪽에 있는 경우
                else if (dinoXStart > obstacleXEnd) { continue; }
                // 공룡이 장애물보다 위에 있는 경우
                else if (dinoYEnd < obstacleYStart) { continue; }
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