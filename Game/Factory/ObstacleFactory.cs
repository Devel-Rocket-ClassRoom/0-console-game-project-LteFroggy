using Framework.Engine;
using System;
using System.Collections.Generic;
class ObstacleFactory {
    private List<Func<Scene, int, int, BasicObstacle>> _obstacleFactory;

    public ObstacleFactory() {
        _obstacleFactory = new List<Func<Scene, int, int, BasicObstacle>> {
            (scene, x, y) => new Fence(scene, x, y),
        };
    }
    public void GetRandomObstacle() {

    }
}
