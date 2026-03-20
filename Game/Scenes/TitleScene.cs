using System;
using Framework.Engine;

namespace Framework.MyGame
{
    public class TitleScene : Scene
    {
        public event GameAction StartRequested;
        string[] dinoArt = new string[]
        {
            "                    __                                __",
            "                   / _)                              / _)",
            "             _.---/ /                          _.---/ /",
            "            /     /                           /     /",
            "         __/ ( | ( |                      __/ ( | ( |",
            "        /__.-'|_|--|_|                   /__.-'|_|--|_|",
        };
        public override void Load() { }

        public override void Update(float deltaTime)
        {
            if (Input.IsKeyDown(ConsoleKey.Spacebar))
            {
                StartRequested?.Invoke();
            }
        }

        public override void Draw(ScreenBuffer buffer)
        {
            buffer.WriteLines(20, 1, dinoArt, ConsoleColor.Green);
            buffer.WriteTextCentered(10, "장애물을 피해 달려야 합니다.", ConsoleColor.DarkGreen);
            buffer.WriteTextCentered(11, "스페이스바를 눌러보세요.", ConsoleColor.DarkGreen);
            buffer.WriteTextCentered(15, "ESC키를 눌러 종료합니다.", ConsoleColor.DarkGray);
        }

        public override void Unload() { }
    }
}