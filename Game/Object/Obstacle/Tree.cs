using Framework.Engine;

class Tree : BasicObstacle {
    private string[] _shape = new string[] {
        " ▲ ",
        "/ \\",
        "---",
        " | "
    };

    public Tree(Scene scene, int width, int height) : base(scene, width, height) { }

    public override string GameOverString => "나무에 박아 쓰러졌습니다..";

    protected override string[] ObstacleShape => _shape;
}