
using Framework.Engine;
using System;

public class Sun : BasicBackgroundObject {
    private string[] _shape = new string[] {
        "    \\ | /    ",
        "  -- ( ) --  ",
        "    / | \\    "
    };

    public Sun(Scene scene, int width, int height) : base(scene, width, height) { }

    protected override ConsoleColor Color => ConsoleColor.Red;
    public override string[] ObjectShape => _shape;
    public override int XLoc => (int)_xLoc;
    public override int YLoc => (int)_yLoc;
}
