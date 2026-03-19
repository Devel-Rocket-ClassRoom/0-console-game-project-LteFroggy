using Framework.Engine;
using System;

class Bird : BasicObstacle {
    private string[] _shape = new string[] {
        " __ ",
        "\\(o>",
        " ( \\",
        " ^^"        
    };

    public Bird(Scene scene, int width, int height) : base(scene, width, height) {
        _yLoc -= 6;
    }

    public override string GameOverString => $"불쌍한 새와 충돌했습니다..";

    protected override string[] ObstacleShape => _shape;

    protected override ConsoleColor Color => ConsoleColor.Yellow;
}
