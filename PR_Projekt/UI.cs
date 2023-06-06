using Color = BasicFunctionsLibrary.Color;

namespace PR_Projekt
{
    internal class UI
    {
        public enum Location
        {
            Start,
            Settings,
            Controls,
            Exit,
            Restart,
            MainMenu,
            None
        }

        public static Location loc = Location.MainMenu;
        
        public static void Reddirect(Location loc, Player p)
        {
            switch (loc)
            {
                case Location.Start:
                    Game.Restart(p);
                    break;
                case Location.Settings:
                    Console.Title = "PacMan 2023 - Settings";
                    SettingsMenu(p);
                    break;
                case Location.Controls:
                    try
                    {
                        Console.Title = "PacMan 2023 - Controls";
                        Controls(p);
                    }
                    catch (Exception e)
                    {
                        Tools.ThrowException(e);
                    }
                    break;
                case Location.Exit:
                    Exit(p);
                    break;
                case Location.Restart:
                    Game.Restart(p);
                    break;
                case Location.MainMenu:
                    Console.Title = "PacMan 2023";
                    MainMenu(p);
                    break;
                case Location.None:
                    throw new Exception("An unknown error occured!");
                default:
                    break;
            }
        }

        public static void MainMenu(Player p)
        {
            if (!Tools.patchNotesRead)
                Tools.ShowPatchNotes(p);

            if (Settings.loadingAnimation)
            {
                Tools.LoadingScreenAnimation();
                Settings.loadingAnimation = false;
            }
            Console.Clear();
            Console.Title = "PacMan 2023";
            Logo();

            bool inMenu = true;

            string[] buttonLabels = { "Start", "Settings", "Controls", "Exit" };
            int[] buttonOffsets = { 10, 15, 20, 25 };

            int selectedButton = 0;
            bool patchNotes = false;

            string tip = Tools.GenerateRandomTip();
            while (inMenu)
            {
                for (int i = 0; i < buttonLabels.Length; i++)
                {
                    if (i == selectedButton)
                    {
                        Color.Green();
                    }
                    else
                    {
                        Color.Yellow();
                    }

                    CreateButton(buttonLabels[i], buttonOffsets[i]);
                }

                string recText = @$"Recommendation: Turn off volume to reduce lag";

                AppVersion(patchNotes);

                Color.Yellow();
                Console.SetCursorPosition(Console.WindowWidth - recText.Length, 0);
                Console.Write(recText);

                Color.Magenta();
                Console.SetCursorPosition(Console.WindowWidth - tip.Length, Console.WindowHeight - 1);
                Console.Write(tip);

                ConsoleKey key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.Tab:
                        Tools.PlaySound("select");
                        if (selectedButton < 0)
                            selectedButton = 0;
                        else
                            selectedButton = -1;
                        patchNotes = !patchNotes;
                        break;
                    case ConsoleKey.Enter:
                        Tools.PlaySound("select");
                        if (selectedButton == 0)
                        {
                            ButtonBlink(buttonLabels[0], buttonOffsets[0]);
                            loc = Location.Start;
                            inMenu = false;
                        }
                        else if (selectedButton == 1)
                        {
                            ButtonBlink(buttonLabels[1], buttonOffsets[1]);
                            loc = Location.Settings;
                            inMenu = false;
                        }
                        else if (selectedButton == 2)
                        {
                            ButtonBlink(buttonLabels[2], buttonOffsets[2]);
                            loc = Location.Controls;
                            inMenu = false;
                        }
                        else if (selectedButton == 3)
                        {
                            ButtonBlink(buttonLabels[3], buttonOffsets[3]);
                            loc = Location.Exit;
                            inMenu = false;
                        }
                        else if (patchNotes)
                            Tools.ShowPatchNotes(p);
                        break;
                    case ConsoleKey.DownArrow:
                        if (!patchNotes)
                        {
                            Tools.PlaySound("button");
                            if (selectedButton < buttonLabels.Length - 1)
                            {
                                selectedButton++;
                            }
                            else selectedButton = 0;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (!patchNotes)
                        {
                            Tools.PlaySound("button");
                            if (selectedButton > 0)
                            {
                                selectedButton--;
                            }
                            else selectedButton = buttonLabels.Length - 1;
                        }
                        break;
                    case ConsoleKey.Escape:
                        Tools.PlaySound("button");
                        Exit(p);
                        break;
                    case ConsoleKey.F1:
                        Tools.PlaySound("button");
                        Start(p);
                        break;
                    case ConsoleKey.C:
                        Tools.PlaySound("button");
                        Controls(p);
                        break;
                    case ConsoleKey.S:
                        Tools.PlaySound("button");
                        SettingsMenu(p);
                        break;
                }
            }
            Reddirect(loc, p);
        }

        private static void AppVersion(bool selected = false)
        {
            Color.Yellow();
            string text = Settings.VersionGet();
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            Console.Write(text);

            if (selected)
                Color.Green();
            else
                Color.Yellow();

            string patchNotes = $"   <Patch Notes> [PRESS TAB]";
            Console.Write(patchNotes);
        }

        public static void Start(Player p)
        {
            Ghost g = new();

            Tools.LoadingScreenAnimation(true);
                
            Game.DrawMap();
            Game.InitBorder();

            Tools.StopMusic();
            Game.PlayMainTheme(p);

            p.direction = Player.Direction.Up;
            Game.RenderPlayer(p);

            PressKeyToStart(p, g);

            Game.InitBorder();

            p.UpdatePlayerScore();

            Movement.Move(p, g);
        }

        public static bool PressAnyKeyLoop = true;
        private static void PressKeyToStart(Player p, Ghost g)
        {
            Console.Title = "Press any key to start";
            PressAnyKeyLoop = true;
            string text = "Press any key to start the game";

            while (PressAnyKeyLoop)
            {
                Console.SetCursorPosition(Console.WindowWidth / 2 - text.Length + 15, 0);
                Color.Green();
                Console.Write(text);
                LoadKey();
                Thread.Sleep(500);
                Console.SetCursorPosition(Console.WindowWidth / 2 - text.Length + 15, 0);
                Color.White();
                Console.Write(text);
                LoadKey();
                Thread.Sleep(500);
            }
            
        }

        private static void LoadKey()
        {
            if (Console.KeyAvailable)
            {
                _ = Console.ReadKey(true).Key;
                PressAnyKeyLoop = false;
            }
        }

        public static void SettingsMenu(Player p)
        {
            Console.Clear();

            Color.Yellow();
            Console.SetCursorPosition(Console.WindowWidth / 2 - 36, 2);
            Console.WriteLine("╔" + new string('═', 80) + "╗");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 36, 3);
            Console.WriteLine("║" + @" ______     ______     ______   ______   __     __   __    ______     ______    " + "║");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 36, 4);
            Console.WriteLine("║" + @"/\  ___\   /\  ___\   /\__  _\ /\__  _\ /\ \   /\ ""-.\ \  /\  ___\   /\  ___\   " + "║");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 36, 5);
            Console.WriteLine("║" + @"\ \___  \  \ \  __\   \/_/\ \/ \/_/\ \/ \ \ \  \ \ \ -. \ \ \ \__ \  \ \___  \  " + "║");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 36, 6);
            Console.WriteLine("║" + @" \/\_____\  \ \_____\    \ \_\    \ \_\  \ \_\  \ \_\\""\_\ \ \_____\  \/\_____\ " + "║");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 36, 7);
            Console.WriteLine("║" + @"  \/_____/   \/_____/     \/_/     \/_/   \/_/   \/_/ \/_/  \/_____/   \/_____/ " + "║");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 36, 8);
            Console.WriteLine("╚" + new string('═', 80) + "╝");

            bool inMenu = true;

            string[] buttonLabels = { "Volume", "Render Wait Time", "Debug Mode", "Back" };
            int[] buttonOffsets = { 10, 15, 20, 25 };

            int selectedButton = 0;

            while (inMenu)
            {
                for (int i = 0; i < buttonLabels.Length; i++)
                {
                    string label = buttonLabels[i];

                    if (label == buttonLabels[0])
                    {
                        //int volumePercentage = (int)Math.Round(Settings.GetVolume() * 100);
                        //string volumeString = volumePercentage.ToString();
                        //if (volumePercentage < 100 && volumePercentage > 0)
                        //    volumeString = " " + volumeString;
                        //else if (volumePercentage == 0)
                        //    volumeString = "  " + volumeString;

                        //label = "Volume: " + volumeString + " %";
                        if (Settings.Music)
                        {
                            label = "Music: " + " ON";
                        }
                        else if (!Settings.Music)
                        {
                            label = "Music: " + "OFF";
                        }
                    }
                    if (label == buttonLabels[1])
                    {
                        int playerSpeed = (int)Settings.GetPlayerSpeed();
                        string speedString = playerSpeed.ToString();
                        if (playerSpeed < 1000 && playerSpeed >= 100)
                            speedString = " " + speedString;
                        else if (playerSpeed < 100 && playerSpeed >= 10)
                            speedString = "  " + speedString;
                        else if (playerSpeed < 10)
                            speedString = "   " + speedString;

                        label = "Render Wait Time: " + speedString + " ms";
                    }
                    if (label == buttonLabels[2])
                    {
                        if (Settings.DebugMode)
                        {
                            label = "Debug Mode: " + " ON";
                        }
                        else if (!Settings.DebugMode)
                        {
                            label = "Debug Mode: " + "OFF";
                        }
                    }

                    if (i == selectedButton)
                    {
                        Color.Green();
                    }
                    else
                    {
                        Color.Yellow();
                    }

                    CreateButton(label, buttonOffsets[i]);
                }

                ConsoleKey key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.Enter:
                        Tools.PlaySound("button");
                        if (selectedButton == 3)
                        {
                            Settings.SaveSettings();
                            ButtonBlink(buttonLabels[3], buttonOffsets[3]);
                            loc = Location.MainMenu;
                            inMenu = false;
                        }
                        if (selectedButton == 2)
                        {
                            Settings.DebugMode = !Settings.DebugMode;
                        }
                        else if (selectedButton == 0)
                        {
                            Settings.Music = !Settings.Music;
                            if (!Settings.Music)
                                Tools.StopMusic();
                            else
                                Tools.PlaySound("theme");
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        Tools.PlaySound("button");
                        //if (selectedButton == 0)
                        //{
                        //    Settings.VolumeModify(true);
                        //}
                        if (selectedButton == 1)
                        {
                            Settings.SpeedModify(true);
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        Tools.PlaySound("button");
                        //if (selectedButton == 0)
                        //{
                        //    Settings.VolumeModify();
                        //}
                        if (selectedButton == 1)
                        {
                            Settings.SpeedModify();
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        Tools.PlaySound("button");
                        if (selectedButton < buttonLabels.Length - 1)
                        {
                            selectedButton++;
                        }
                        else selectedButton = 0;
                        break;
                    case ConsoleKey.UpArrow:
                        Tools.PlaySound("button");
                        if (selectedButton > 0)
                        {
                            selectedButton--;
                        }
                        else selectedButton = buttonLabels.Length - 1;
                        break;
                    case ConsoleKey.Backspace:
                        Tools.PlaySound("button");
                        Settings.SaveSettings();
                        loc = Location.MainMenu;
                        inMenu = false;
                        break;
                }
            }
            Reddirect(loc, p);

        }
        public static void Controls(Player p)
        {
            Console.Clear();

            Color.Yellow();
            Console.SetCursorPosition(Console.WindowWidth / 2 - 34, 2);
            Console.WriteLine("╔" + new string('═', 68) + "╗");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 34, 3);
            Console.Write("║" + @" ______  ______  __   __  ______  ______  ______  __      ______    " + "║");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 34, 4);
            Console.Write("║" + @"/\  ___\/\  __ \/\ ""-.\ \/\__  _\/\  == \/\  __ \/\ \    /\  ___\   " + "║");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 34, 5);
            Console.Write("║" + @"\ \ \___\ \ \/\ \ \ \-.  \/_/\ \/\ \  __<\ \ \/\ \ \ \___\ \___  \  " + "║");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 34, 6);
            Console.Write("║" + @" \ \_____\ \_____\ \_\\""\_\ \ \_\ \ \_\ \_\ \_____\ \_____\/\_____\ " + "║");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 34, 7);
            Console.Write("║" + @"  \/_____/\/_____/\/_/ \/_/  \/_/  \/_/ /_/\/_____/\/_____/\/_____/ " + "║");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 34, 8);
            Console.WriteLine("╚" + new string('═', 68) + "╝");

            ControlsSectionHeader("Game", 32, 10);
            WriteKey("W", 19, 11, "Move up");
            WriteKey("S", 19, 15, "Move down");
            WriteKey("A", 19, 19, "Move left");
            WriteKey("D", 19, 23, "Move right");

            ControlsSectionHeader("Others", 60, 10);
            WriteKey("Esc", 49, 11, "Leave the game");
            WriteKey("Bksp", 49, 15, "Back");
            WriteKey("Enter", 49, 19, "Select");

            ControlsSectionHeader("Quick Menu", 89, 10);
            WriteKey("F1", 79, 11, "Start the game");
            WriteKey("S", 79, 15, "Settings");
            WriteKey("C", 79, 19, "Controls");
            WriteKey("Esc", 79, 23, "Exit the app");

            Location loc;
            while (true)
            {
                Color.Green();
                CreateButton("Back", 24, 60);
                ConsoleKey key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Enter)
                {
                    Tools.PlaySound("button");
                    ButtonBlink("Back", 24, 60);
                    loc = Location.MainMenu;
                    break;
                }
                else if (key == ConsoleKey.Backspace)
                {
                    Tools.PlaySound("button");
                    loc = Location.MainMenu;
                    break;
                }
            }

            Reddirect(loc, p);
        }

        private static void ControlsSectionHeader(string header, int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write("▼" + header + "▼");
        }

        private static void WriteKey(string key, int x, int y, string label)
        {
            if (key.Length == 1)
                key = " " + key + " ";
            else if (key.Length == 2)
                key += " ";

            if (key.Length == 5)
            {
                Console.SetCursorPosition(x - 1, y);
                Console.Write(@" _______ ");
                Console.SetCursorPosition(x - 1, y + 1);
                Console.Write(@$"||{key}||");
                Console.SetCursorPosition(x - 1, y + 2);
                Console.Write(@"||_____||");
                Console.Write(" >  " + label);
                Console.SetCursorPosition(x - 1, y + 3);
                Console.Write(@"|/_____\|");
            }
            else if (key.Length == 4)
            {
                Console.SetCursorPosition(x, y);
                Console.Write(@" ______ ");
                Console.SetCursorPosition(x, y + 1);
                Console.Write(@$"||{key}||");
                Console.SetCursorPosition(x, y + 2);
                Console.Write(@"||____||");
                Console.Write(" >  " + label);
                Console.SetCursorPosition(x, y + 3);
                Console.Write(@"|/____\|");
            }
            else if (key.Length == 3)
            {
                Console.SetCursorPosition(x, y);
                Console.Write(@" _____ ");
                Console.SetCursorPosition(x, y + 1);
                Console.Write(@$"||{key}||");
                Console.SetCursorPosition(x, y + 2);
                Console.Write(@"||___||");
                Console.Write("  >  " + label);
                Console.SetCursorPosition(x, y + 3);
                Console.Write(@"|/___\|");
            }
        }

        public static void Exit(Player p)
        {
            Console.Clear();

            Color.Green();
            string label = "Are you sure you want to exit?";
            Console.SetCursorPosition(Console.WindowWidth / 2 - label.Length / 2 + 3, 12);
            Console.Write("Are you sure you want to exit?");

            string[] buttonLabels = { "Yes", "No" };
            int[] buttonOffsets = { 15, 20 };

            bool inMenu = true;
            int selectedButton = 0;

            while (inMenu)
            {
                for (int i = 0; i < buttonLabels.Length; i++)
                {
                    if (i == selectedButton)
                    {
                        if (selectedButton == 0)
                            Color.Red();
                        else
                            Color.Green();
                    }
                    else
                    {
                        Color.Yellow();
                    }

                    CreateButton(buttonLabels[i], buttonOffsets[i]);
                }

                ConsoleKey key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.Enter:
                        Tools.PlaySound("button");
                        if (selectedButton == 0)
                        {
                            Settings.SaveSettings();
                            Environment.Exit(0);
                        }
                        else
                        {
                            ButtonBlink(buttonLabels[1], buttonOffsets[1]);
                            loc = Location.MainMenu;
                            inMenu = false;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        Tools.PlaySound("button");
                        if (selectedButton < buttonLabels.Length - 1)
                        {
                            selectedButton++;
                        }
                        else selectedButton = 0;
                        break;
                    case ConsoleKey.UpArrow:
                        Tools.PlaySound("button");
                        if (selectedButton < buttonLabels.Length - 1)
                        {
                            selectedButton--;
                        }
                        else selectedButton = 0;
                        break;
                }
            }
            Reddirect(loc, p);
        }


        public static void ButtonBlink(string label, int yOffset, int x = 60)
        {
            for (int i = 0; i < 4; i++)
            {
                Color.Green();
                CreateButton(label, yOffset, x);
                Thread.Sleep(100);
                Color.Yellow();
                CreateButton(label, yOffset, x);
                Thread.Sleep(100);
            }
        }

        public static void CreateButton(string label, int yOffset, int x = 60)
        {
            Console.SetCursorPosition((x) - (label.Length / 2), yOffset);
            Console.WriteLine("╔" + new string('═', label.Length + 2) + "╗");
            Console.SetCursorPosition((x) - (label.Length / 2), yOffset + 1);
            Console.WriteLine("║ " + label + " ║");
            Console.SetCursorPosition((x) - (label.Length / 2), yOffset + 2);
            Console.WriteLine("╚" + new string('═', label.Length + 2) + "╝");
        }

        private static void Logo()
        {
            Color.Yellow();
            Console.SetCursorPosition(Console.WindowWidth / 2 - 34, 2);
            Console.WriteLine("╔" + new string('═', 67) + "╗");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 34, 3);
            Console.WriteLine("║" + " ______   ______     ______     __    __     ______     __   __    " + "║");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 34, 4);
            Console.WriteLine("║" + @"/\  == \ /\  __ \   /\  ___\   /\ ""-./  \   /\  __ \   /\ ""-.\ \ " + "  ║");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 34, 5);
            Console.WriteLine("║" + @"\ \  _-/ \ \  __ \  \ \ \____  \ \ \-./\ \  \ \  __ \  \ \ \-.  \  " + "║");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 34, 6);
            Console.WriteLine("║" + @" \ \_\    \ \_\ \_\  \ \_____\  \ \_\ \ \_\  \ \_\ \_\  \ \_\\""\_\" + " ║");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 34, 7);
            Console.WriteLine("║" + @"  \/_/     \/_/\/_/   \/_____/   \/_/  \/_/   \/_/\/_/   \/_/ \/_/ " + "║");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 34, 8);
            Console.WriteLine("╚" + new string('═', 67) + "╝");
        }
    }
}
