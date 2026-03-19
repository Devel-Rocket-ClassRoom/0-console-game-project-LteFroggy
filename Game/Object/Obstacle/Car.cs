using Framework.Engine;
using System;

class Car : BasicObstacle {
    private string[] _shape = new string[] {
        "   ______  ",
        " _/|[][]|\\_",
        "|_        _| ",
        "  O------O   "
    };

    public Car(Scene scene, int width, int height) : base(scene, width, height) { }

    public override string GameOverString => $"교통사고가 났습니다..";

    public override int CollisionWidth => base.CollisionWidth - 1;

    public override int CollisionHeight => base.CollisionHeight;

    protected override string[] ObstacleShape => _shape;

    protected override ConsoleColor Color => ConsoleColor.Blue;
}