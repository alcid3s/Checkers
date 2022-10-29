using Raylib_cs;
using static Raylib_cs.Raylib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Checkers.Custom;
using Checkers.Screens;

namespace Checkers.Screens
{
    public class JoinScreen : Screen
    {
        private InputBox _ipAddress;
        private InputBox _port;

        private Button _enter;

        public string Ip { get; private set; } = string.Empty;
        public int Port { get; private set; } = 0;
        public JoinScreen()
        {
            _ipAddress = new InputBox(new Rectangle(GetScreenWidth() / 2 - 200, (GetScreenHeight() / 4), 400, 75), 60);
            _port = new InputBox(new Rectangle(GetScreenWidth() / 2 - 200, (GetScreenHeight() / 2), 200, 75), 60);
            _enter = new Button(new Rectangle(GetScreenWidth() / 2 - 200, (GetScreenHeight() / 1.25f), 250, 75), Color.GREEN, 60, "ENTER");
        }
        public override void Draw()
        {
            DrawText("IP of server:", (int)_ipAddress.TextBox.x + 5, (int)_ipAddress.TextBox.y - 70, 60, Color.MAROON);
            _ipAddress.Draw();
            DrawText("PORT of server:", (int)_port.TextBox.x + 5, (int)_port.TextBox.y - 70, 60, Color.MAROON);
            _port.Draw();
            _enter.Draw();
        }

        public override void Update()
        {
            if (_ipAddress.MouseOntextBox || _port.MouseOntextBox)
                SetMouseCursor(MouseCursor.MOUSE_CURSOR_IBEAM);
            else
                SetMouseCursor(MouseCursor.MOUSE_CURSOR_DEFAULT);

            _ipAddress.Update();
            _port.Update();
            _enter.Update();

            _enter.OnAction += delegate
            {
                if (Ip.Equals(string.Empty) && Port == 0 || Ip.Equals("127.0.0.1") && Port == 1337)
                {
                    Ip = "127.0.0.1";
                    Port = 1337;
                    ScreenManager.State = ScreenManager.ScreenState.PlayState;
                }
                else
                {
                    string ip = string.Empty;
                    for (int i = 0; i < _ipAddress.Index; i++)
                    {
                        ip += _ipAddress.Text[i];
                    }
                    Ip = ip;
                    Ip = Ip.Trim();
                    try
                    {
                        Port = int.Parse(_port.Text);
                        ScreenManager.State = ScreenManager.ScreenState.PlayState;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            };
        }

    }
}


