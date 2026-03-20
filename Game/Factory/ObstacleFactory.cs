using Framework.Engine;
using System;
using System.Collections.Generic;
public class ObstacleFactory {
    private List<Func<Scene, int, int, BasicObstacle>> _obstacleFactory;
    private Random _rand = new Random();

    public ObstacleFactory() {
        _rand = new Random();

        _obstacleFactory = new List<Func<Scene, int, int, BasicObstacle>> {
            (scene, x, y) => new Fence(scene, x, y),
            (scene, x, y) => new Tree(scene, x, y),
            (scene, x, y) => new Car(scene, x, y),
            (scene, x, y) => new Bird(scene, x, y),
        };
    }
    
    /// <summary>
    /// 랜덤한 Obstacle을 생성하고 반환
    /// </summary>
    /// <param name="scene">해당 Obstacle이 그려질 scene</param>
    /// <param name="x">x좌표</param>
    /// <param name="y">y좌표</param>
    /// <returns>생성된 장애물</returns>
    public BasicObstacle GetRandomObstacle(Scene scene, int width, int height) {
        return _obstacleFactory[_rand.Next(1, 100) % _obstacleFactory.Count]?.Invoke(scene, width, height);
    }
}
