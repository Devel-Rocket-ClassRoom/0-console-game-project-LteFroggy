using System;
using Framework.Engine;

public abstract class BasicBackgroundObject : GameObject {

    protected BasicBackgroundObject(Scene scene, int width, int height) : base(scene) {
        _xLoc = width;
        _yLoc = 2;
    }

    public abstract string[] ObjectShape { get; }

    public override int ObjectWidth => ObjectShape[0].Length;
    public override int ObjectHeight => ObjectShape.Length;

    public override int XLoc => (int)_xLoc;
    public override int YLoc => (int)_yLoc;

    public override void Draw(ScreenBuffer buffer) {
        buffer.WriteLines((int)_xLoc, (int)_yLoc, ObjectShape, Color);
    }

    public override void Update(float deltaTime, float accerlation) {
        _xLoc -= deltaTime * 3f;

        if (_xLoc + ObjectWidth < 0) {
            IsActive = false;
        }
    }
}
