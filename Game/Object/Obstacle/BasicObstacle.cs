using Framework.Engine;

abstract class BasicObstacle : GameObject {
    protected float _xLoc;
    protected float _yLoc;

    protected BasicObstacle(Scene scene, int width) : base(scene) {
        _xLoc = width;
    }

    protected abstract string[] ObstacleShape { get; }

    // 장애물 그리는 규칙도 모두 동일하므로, 동일하게 구현
    public override void Draw(ScreenBuffer buffer) {
        buffer.WriteLines((int)_xLoc, (int)_yLoc, ObstacleShape);
    }

    // 장애물 움직임 규칙은 모두 똑같으므로, Update는 동일하게 구현
    public override void Update(float deltaTime, float accerlation) {
        _xLoc -= deltaTime * (accerlation / 100);

        if (_xLoc < 0) {
            IsActive = false;
        }
    }
}