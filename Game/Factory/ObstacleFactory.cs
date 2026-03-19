using Framework.Engine;
using System;
using System.Collections.Generic;
class ObstacleFactory {
    private List<Func<Scene, int, int, BasicObstacle>> _obstacleFactory;

    public ObstacleFactory() {
        _obstacleFactory = new List<Func<Scene, int, int, BasicObstacle>> {
            (scene, x, y) => new Fence(scene, x, y),
            (scene, x, y) => new Tree(scene, x, y),
            (scene, x, y) => new Car(scene, x, y),
            (scene, x, y) => new Bird(scene, x, y),
        };
    }
    public BasicObstacle GetRandomObstacle(int randNum, Scene scene, int x, int y) {
        return _obstacleFactory[randNum % _obstacleFactory.Count]?.Invoke(scene, x, y);
    }
}
