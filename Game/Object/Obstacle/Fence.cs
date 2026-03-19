using Framework.Engine;
using System;
using System.Reflection.Metadata.Ecma335;

class Fence : BasicObstacle {
    private string[] _obstacleShape = new string[] {
        "|",
        "|"
    };

    public Fence(Scene scene, int xLoc) : base(scene, xLoc) {  }

    protected override string[] ObstacleShape => _obstacleShape;
}