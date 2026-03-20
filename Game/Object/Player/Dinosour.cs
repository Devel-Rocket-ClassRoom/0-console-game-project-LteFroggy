using Framework.Engine;
using System;
using System.Runtime.CompilerServices;

class Dinosour : GameObject {
    private int _runState;
    private readonly float _baseYLoc;

    private float _ySpeed;
    private readonly float _jumpPower;
    private readonly float _gravity;
    private readonly float _jumpDeadzone;
    private readonly float _maxJumpHoldTime;
    private float _jumpHoldTime;

    private readonly ConsoleColor _color;

    private readonly string[][] _runFrames = {
        new string[] {
        "  __ ",
        " /o )",
        "/|_/ ",
        " / / "
        },
        new string[] {
        "  __ ",
        " /o )",
        "/|_/ ",
        " \\\\"
        },
    };

    public override int Width => _runFrames[0][0].Length;
    public override int Height => _runFrames[0].Length;
    public override int XLoc => (int)_xLoc;
    public override int YLoc => (int)_yLoc;

    private bool OnGround => _yLoc >= _baseYLoc;

    public override int CollisionWidth => Width - 1;

    public override int CollisionHeight => Height - 1;

    public Dinosour(Scene scene) : base(scene) {
        _jumpPower = 15;
        _gravity = 70;
        _jumpDeadzone = 0.2f;
        _maxJumpHoldTime = _jumpDeadzone + 0.1f;

        _baseYLoc = 19 - Height;
        _runState = 0;
        _xLoc = 5;
        _yLoc = _baseYLoc;

        // 처음 스페이스바 눌러서 시작할 때 점프로 시작하도록
        _ySpeed = _jumpPower;

        _color = ConsoleColor.Green;
    }

    public override void Draw(ScreenBuffer buffer) {
        buffer.WriteLines((int)_xLoc, (int)_yLoc, _runFrames[_runState], _color);
        buffer.WriteLines((int)_xLoc, (int)_yLoc, _runFrames[_runState], _color);
    }

    public override void Update(float deltaTime, float accerlation) {
        // 점프 꾹 누르는 시간 계산
        _jumpHoldTime += deltaTime;

        // y위치 갱신
        _yLoc -= deltaTime * _ySpeed;

        // 점프 키를 눌렀을때 점프, 공중에 있을 때는 점프되면 안됨
        if (Input.IsKeyDown(ConsoleKey.Spacebar) && OnGround) {
            _ySpeed = _jumpPower;
            _jumpHoldTime = 0;
        }

        // 한번 점프를 눌렀다면, 일정 시간동안은 속도 변경 없음
        else if (_jumpHoldTime <= _jumpDeadzone) { }

        // 그 이후에도 키를 계속 누르고있다면, 추가 상승 (속도 변경 없는 시간 증가)
        else if (Input.IsKey(ConsoleKey.Spacebar) && _jumpHoldTime <= _maxJumpHoldTime) { }

        // 바닥에 닿으면, 모든 값 기본으로
        else if (OnGround) {
            _ySpeed = 0;
            _yLoc = _baseYLoc;
            _jumpHoldTime = 0;
        }

        // 이외 상황에서는 하강
        else {
            _ySpeed -= _gravity * deltaTime;
        } 

        if (_runState == 0) {
            _runState++;
        } else {
            _runState--;
        }
    }
}
