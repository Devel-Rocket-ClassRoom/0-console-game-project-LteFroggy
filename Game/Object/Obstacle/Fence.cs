using Framework.Engine;

class Fence : BasicObstacle {
    private string[] _obstacleShape = new string[] {
        "||",
        "||",
        "||"
    };

    public Fence(Scene scene, int xLoc, int yLoc) : base(scene, xLoc, yLoc) {  }

    protected override string[] ObstacleShape => _obstacleShape;
}