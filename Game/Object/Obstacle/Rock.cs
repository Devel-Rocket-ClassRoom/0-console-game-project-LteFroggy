
using Framework.Engine;
using System;

public class Rock : BasicObstacle {
    private string[] _shape = new string[] {
        "  ____   ",
        " /    \\ ",
        "/______\\"
    };
    public Rock(Scene scene, int width, int height) : base(scene, width, height) { }

    public override string GameOverString => $"바위에 부딫혔습니다...";


    protected override ConsoleColor Color => ConsoleColor.Gray;
    protected override string[] ObstacleShape => _shape;
}