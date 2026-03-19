using Framework.Engine;

abstract class BasicObstacle : GameObject {
    protected float _xLoc;
    protected float _yLoc;

    protected BasicObstacle(Scene scene, int width, int height) : base(scene) {
        _xLoc = width - 1;
        _yLoc = height; 
    }

    public override int XLoc => (int)_xLoc;
    public override int YLoc => (int)_yLoc;

    // 장애물 모양에 따른 가로, 세로 길이
    public override int Width => ObstacleShape[0].Length;
    public override int Height => ObstacleShape.Length;

    // 장애물 모양
    protected abstract string[] ObstacleShape { get; }


    // 장애물 그리는 규칙도 모두 동일하므로, 동일하게 구현
    public override void Draw(ScreenBuffer buffer) {
        buffer.WriteLines((int)_xLoc - ObstacleWidth, (int)_yLoc - ObstacleHeight, ObstacleShape);
    }

    // 장애물 움직임 규칙은 모두 똑같으므로, Update는 동일하게 구현
    public override void Update(float deltaTime, float accerlation) {
        _xLoc -= deltaTime * accerlation;

        if (_xLoc < 0) {
            IsActive = false;
        }
    }
}