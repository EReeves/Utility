using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Timers;
using Game.Shared.Core;
using Game.Shared.Network;
using MonoGame.Extended;
using QuakeConsole;

namespace Game.Shared.Utility
{
    public class QConsole : ConsoleComponent, ICoreComponent
    {
        //Singleton Implementation
        private static QConsole instance;

        private QConsole(Microsoft.Xna.Framework.Game game) : base(game)
        {
            var interpreter = new ManualInterpreter();
            Interpreter = interpreter;

            interpreter.RegisterCommand("ss", (Action<string[]>) ServerCommand);
            interpreter.RegisterCommand("dd", (Action<string[]>) DebugCommand);
        }

        public static Microsoft.Xna.Framework.Game GameReference { get; set; }

        public static QConsole Instance
        {
            get
            {
                instance = instance ?? new QConsole(GameReference);
                return instance;
            }
        }

        public CoreController Core { get; set; }
        public bool Enabled { get; set; } = true;
        public Camera2D Camera { get; set; }

        public void Log(string str)
        {
            str = Regex.Replace(str, "[^0-9a-zA-Z.\\s\\:\\{\\\\}\\(\\)[\\]]+", "");
            instance.Output.Append(str);
        }

        private void DebugCommand(string[] obj)
        {
            Core.DoDebugDraw = !Core.DoDebugDraw;
        }

        private void ServerCommand(string[] strings)
        {
            var network = NetworkSingleton.Instance;
            network.InitNetwork(NetworkSingleton.NetworkType.Listen);
            network.Server.Listen(1777);
            var timer = new Timer
            {
                Interval = 3000,
                AutoReset = false
            };
            timer.Elapsed += (sender, args) =>
            {
                var split = "127.0.0.1:1777".Split(':');
                var ip = split[0];
                var port = Convert.ToInt32(split[1]);
                network.Client.Connect(new IPEndPoint(IPAddress.Parse(ip), port));
            };
            timer.Start();
        }
    }
}