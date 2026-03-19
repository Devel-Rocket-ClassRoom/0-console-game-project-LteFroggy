using Framework.Engine;
using System;

class Dinosour : GameObject {
    private int _runState;
    private int _previousYLoc;
    private int _yLoc;
    private int _yAcc;

    private readonly string[][] _runFrames = {
        new string[] {
        "  __",
        " /o )",
        "/|_/",
        " / /"
        },
        new string[] {
        "  __",
        " /o )",
        "/|_/",
        " \\\\"
        },
    };

    public Dinosour(Scene scene) : base(scene) {
        _runState = 0;
        _yLoc = 0;
        _previousYLoc = 0;
        _yAcc = 0;
    }

    public override void Draw(ScreenBuffer buffer) {
        buffer.WriteLines(0, _yLoc, _runFrames[_runState]);
        buffer.WriteLines(0, _yLoc, _runFrames[_runState]);
    }

    public override void Update(float deltaTime, float gameDuration) {
        _yLoc = _previousYLoc + (int)(deltaTime * _yAcc);



        if (_runState == 0) {
            _runState++;
        } else {
            _runState--;
        }
    }
}
