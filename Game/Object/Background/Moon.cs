

using Framework.Engine;
using System;
using System.Drawing;

public class Moon : BasicBackgroundObject {
    private string[] _shape = {
        "    _...   ",
        "  .::::.   ",
        " .::::'    ",
        " .::::.    ",
        "  `::::.   ",
        "    `''    "
    };

    public Moon(Scene scene, int width, int height) : base(scene, width, height) {
    }

    public override string[] ObjectShape => _shape;

    protected override ConsoleColor Color => ConsoleColor.Yellow;
}
