using Framework.Engine;
using Framework.MyGame;
using System;

class MainApp : GameApp {
    private readonly SceneManager<Scene> _scenes;
    private readonly int _width;
    private readonly int _height;

    public MainApp(int width = 100, int height = 20) : base(width, height)
    {
        _scenes = new SceneManager<Scene>();
        _width = width;
        _height = height;
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
        PlayScene play = new PlayScene(_width);
        play.PlayAgainRequested += () => ChangeToPlay();
        _scenes.ChangeScene(play);
    }
}
