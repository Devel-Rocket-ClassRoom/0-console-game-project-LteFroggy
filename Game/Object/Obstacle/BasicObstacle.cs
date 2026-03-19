using Framework.Engine;

abstract class BasicObstacle : GameObject {

    protected BasicObstacle(Scene scene, int width, int height) : base(scene) {
        _xLoc = width - 1;
        _yLoc = height - Height;
    }

    public abstract string GameOverString { get; }
    public override int XLoc => (int)_xLoc;
    public override int YLoc => (int)_yLoc;

    // 장애물 모양에 따른 가로, 세로 길이
    public override int Width => ObstacleShape[0].Length;
    public override int Height => ObstacleShape.Length;

    public override int CollisionWidth => Width - 1;
    public override int CollisionHeight => Height - 1;


    // 장애물 모양
    protected abstract string[] ObstacleShape { get; }


    // 장애물 그리는 규칙도 모두 동일하므로, 동일하게 구현
    public override void Draw(ScreenBuffer buffer) {
        buffer.WriteLines((int)_xLoc, (int)_yLoc, ObstacleShape);
    }

    // 장애물 움직임 규칙은 모두 똑같으므로, Update는 동일하게 구현
    public override void Update(float deltaTime, float accerlation) {
        _xLoc -= deltaTime * accerlation;

        if (_xLoc + Width < 0) {
            IsActive = false;
        }
    }
}