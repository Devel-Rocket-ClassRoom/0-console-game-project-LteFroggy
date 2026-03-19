using System;

namespace Framework.Engine
{
    public abstract class GameObject
    {
        protected float _xLoc;
        protected float _yLoc;

        public abstract int XLoc { get; }
        public abstract int YLoc { get; }
        public abstract int Width { get; }
        public abstract int Height { get; }

        public string Name { get; set; } = "";
        public bool IsActive { get; set; } = true;
        public Scene Scene { get; }

        protected GameObject(Scene scene)
        {
            Scene = scene;
        }

        public abstract void Update(float deltaTime, float accerlation);
        public abstract void Draw(ScreenBuffer buffer);
    }
}
