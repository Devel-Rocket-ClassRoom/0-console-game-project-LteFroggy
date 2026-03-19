using Framework.Engine;

namespace Framework.MyGame
{
    public class PlayScene : Scene
    {
        private int _score;
        private string _floor = new string('ㅡ', 40);

        public event GameAction PlayAgainRequested;

        public override void Load()
        {
            _score = 0;
            AddGameObject(new Dinosour(this));
        }

        public override void Update(float deltaTime)
        {
            UpdateGameObjects(deltaTime);
        }

        public override void Draw(ScreenBuffer buffer)
        {
            DrawGameObjects(buffer);
            buffer.DrawHLine(0, 11, 80);
        }

        public override void Unload()
        {
            ClearGameObjects();
        }
    }
}