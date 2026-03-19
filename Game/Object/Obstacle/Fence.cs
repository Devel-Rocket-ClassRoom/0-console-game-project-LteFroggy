using Framework.Engine;

class Fence : BasicObstacle {
    private string[] _obstacleShape = new string[] {
        "||",
        "||",
        "||"
    };

    public Fence(Scene scene, int xLoc, int yLoc) : base(scene, xLoc, yLoc) {  }

    public override string GameOverString => $"울타리에 걸려 넘어졌습니다.";

    protected override string[] ObstacleShape => _obstacleShape;
}