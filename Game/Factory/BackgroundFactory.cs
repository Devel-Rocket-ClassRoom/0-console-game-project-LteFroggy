using Framework.Engine;

public class BackgroundFactory {
    public Sun MakeSun(Scene scene, int width, int height) {
        return new Sun(scene, width, height);
    }

    public Cloud MakeCloud(Scene scene, int width, int height) {
        return new Cloud(scene, width, height);
    }

    public Moon MakeMoon(Scene scene, int width, int height) {
        return new Moon(scene, width, height);
    }
}
