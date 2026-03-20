using Framework.Engine;
using System;

public class Cloud : BasicBackgroundObject {

    private Random _random = new Random();

    private string[] _shape = new string[] {
        "      .--.    ",
        "   .-(    ).  ",
        "  (___.__)__) "
    };

    public Cloud(Scene scene, int width, int height) : base(scene, width, height) {
        _yLoc += _random.Next(0, 8);
    }

    protected override ConsoleColor Color => ConsoleColor.White;

    public override string[] ObjectShape => _shape;
}
