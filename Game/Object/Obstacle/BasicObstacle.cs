using Framework.Engine;
using System;

public abstract class BasicObstacle : GameObject {

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
    // 색상
    protected abstract ConsoleColor Color { get; }


    // 장애물 그리는 규칙도 모두 동일
    public override void Draw(ScreenBuffer buffer) {
        buffer.WriteLines((int)_xLoc, (int)_yLoc, ObstacleShape, Color);
    }

    // 장애물 움직임 규칙은 모든 장애물 동일
    public override void Update(float deltaTime, float accerlation) {
        _xLoc -= deltaTime * accerlation;

        if (_xLoc + Width < 0) {
            IsActive = false;
        }
    }
}