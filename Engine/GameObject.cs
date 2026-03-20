using System;
using System.Drawing;

namespace Framework.Engine
{
    public abstract class GameObject
    {
        protected float _xLoc;
        protected float _yLoc;

        protected virtual ConsoleColor Color { get; }

        // 장애물 충돌 판정 거리
        public virtual int CollisionWidth { get; }
        public virtual int CollisionHeight { get; }

        public abstract int XLoc { get; }
        public abstract int YLoc { get; }
        public abstract int ObjectWidth { get; }
        public abstract int ObjectHeight { get; }

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
