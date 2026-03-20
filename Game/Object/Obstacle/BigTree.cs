using Framework.Engine;
using System;

class BigTree : BasicObstacle {
    private string[] _shape = new string[] {
        "  ▲  ",
        " / \\ ",
        "/___\\",
        " / \\ ",
        "/___\\",
        "  |  "
    };

    public BigTree(Scene scene, int width, int height) : base(scene, width, height) { }

    public override string GameOverString => "나무에 걸려 쓰러졌습니다...";

    protected override ConsoleColor Color => ConsoleColor.Green;

    protected override string[] ObstacleShape => _shape;
}