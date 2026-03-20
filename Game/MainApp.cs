using Framework.Engine;
using Framework.MyGame;
using System;

class MainApp : GameApp {
    private readonly ObstacleFactory _obstacleFactory;
    private readonly BackgroundFactory _backgroundFactory;
    private readonly SceneManager<Scene> _scenes;
    private readonly int _width;
    private readonly int _height;

    public MainApp(ObstacleFactory obstacleFactory, BackgroundFactory backgroundFactory, int width = 100, int height = 20) : base(width, height)
    {
        _scenes = new SceneManager<Scene>();
        _width = width;
        _height = height;
        _obstacleFactory = obstacleFactory;
        _backgroundFactory = backgroundFactory;
    }

    protected override void Initialize()
    {
        ChangeToTitle();
    }

    protected override void Update(float deltaTime)
    {
        if (Input.IsKeyDown(ConsoleKey.Escape))
        {
            Quit();
            return;
        }
        _scenes.CurrentScene?.Update(deltaTime);
    }

    protected override void Draw()
    {
        _scenes.CurrentScene?.Draw(Buffer);
    }

    private void ChangeToTitle()
    {
        TitleScene title = new TitleScene();
        title.StartRequested += () => ChangeToPlay();
        _scenes.ChangeScene(title);
    }

    private void ChangeToPlay()
    {
        PlayScene play = new PlayScene(_width, _height, _obstacleFactory, _backgroundFactory);
        play.PlayAgainRequested += () => ChangeToPlay();
        play.BackToMain += () => Initialize();
        _scenes.ChangeScene(play);
    }
}
