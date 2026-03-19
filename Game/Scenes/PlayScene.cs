using Framework.Engine;
using System;

namespace Framework.MyGame
{
    public class PlayScene : Scene
    {
        private int _width;
        private float _elapsedTime;
        private float _spawnTimer;
        private float _nextSpawnTime;

        private Random _rand = new Random();

        private float _accerlation => _elapsedTime / 100 + 0.3f;
        public event GameAction PlayAgainRequested;
        
        public PlayScene(int width) {
            _elapsedTime = 0;
            _width = width;
        }

        public int Accerlation

        public override void Load()
        {
            AddGameObject(new Dinosour(this)); 
        }

        public override void Update(float deltaTime)
        {
            _elapsedTime += deltaTime;
            _spawnTimer += deltaTime;
            UpdateGameObjects(deltaTime, _accerlation);
            
            TrySpawn();
        }

        private void TrySpawn() {
            // 스폰시간이 되었다면, 생기게 만들기
            if (_spawnTimer >= _nextSpawnTime) {
                AddGameObject(new Fence(this, _width));
                _spawnTimer = 0;
                _nextSpawnTime = 1.0f + (float)_rand.NextDouble();
            } else { }
        }

        public override void Draw(ScreenBuffer buffer)
        {
            // 가로줄 그리기 (바닥)
            buffer.DrawHLine(0, 19, 80);

            DrawGameObjects(buffer);
        }

        public override void Unload()
        {
            ClearGameObjects();
        }
    }
}