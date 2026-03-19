using Framework.Engine;
using System;

class Dinosour : GameObject {
    private int _runState;

    private readonly string[] _runFrame1 =
    {
        "  __",
        " /o )",
        "/|_/",
        " / \\"
    };

    private readonly string[] _runFrame2 =
    {
        "  __",
        " /o )",
        "/|_/",
        " /\\\\"
    };

    public Dinosour(Scene scene) : base(scene) {
        _runState = 0;
    }

    public override void Draw(ScreenBuffer buffer) {
        if (_runState == 0) {
            buffer.WriteText(0, 10, string.Join("\n", _runFrame1));
        } else {
            buffer.WriteText(0, 10, string.Join("\n", _runFrame2));
        }
    }

    public override void Update(float deltaTime) {
        if (_runState == 0) {
            _runState++;
        } else {
            _runState--;
        }
    }
}
