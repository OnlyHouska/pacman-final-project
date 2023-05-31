namespace PR_Projekt
{
    internal class Settings
    {
        public const string SettingsPath = @"MetaData/internal-settings.txt";
        public const string VersionPath = @"MetaData/app-version.txt";

        public static Dictionary<string, ConsoleKey> ControlsSettings = new()
            {
                { "MoveUp", ConsoleKey.W },
                { "MoveDown", ConsoleKey.D},
                { "MoveLeft", ConsoleKey.A},
                { "MoveRight", ConsoleKey.D},

                { "LeaveTheGame", ConsoleKey.Escape},
                { "Back", ConsoleKey.Backspace},
                { "Select", ConsoleKey.Enter},

                { "StartTheGame", ConsoleKey.F1},
                { "Settings", ConsoleKey.S},
                { "Controls", ConsoleKey.C},
                { "ExitTheApp", ConsoleKey.Escape}
            };

        public static void LoadSettings()
        {
            LoadControls();
            if (File.Exists(SettingsPath))
            {
                string[] text = File.ReadAllLines(SettingsPath);
                for (int i = 0; i < text.Length; i++)
                {
                    switch (i)
                    {
                        case 0:
                            Music = bool.Parse(text[i]);
                            break;
                        case 1:
                            PlayerSpeed = int.Parse(text[i]);
                            break;
                        case 2:
                            Tools.patchNotesRead = bool.Parse(text[i]);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public static void SaveSettings()
        {
            string[] text =
            {
                $"{Music}",
                $"{PlayerSpeed}",
                $"{Tools.patchNotesRead}",

                $"{Enum.GetName(typeof(ConsoleKey), ControlsSettings["MoveUp"])}",
                $"{Enum.GetName(typeof(ConsoleKey), ControlsSettings["MoveDown"])}",
                $"{Enum.GetName(typeof(ConsoleKey), ControlsSettings["MoveLeft"])}",
                $"{Enum.GetName(typeof(ConsoleKey), ControlsSettings["MoveRight"])}",

                $"{Enum.GetName(typeof(ConsoleKey), ControlsSettings["LeaveTheGame"])}",
                $"{Enum.GetName(typeof(ConsoleKey), ControlsSettings["Back"])}",
                $"{Enum.GetName(typeof(ConsoleKey), ControlsSettings["Select"])}",

                $"{Enum.GetName(typeof(ConsoleKey), ControlsSettings["StartTheGame"])}",
                $"{Enum.GetName(typeof(ConsoleKey), ControlsSettings["Settings"])}",
                $"{Enum.GetName(typeof(ConsoleKey), ControlsSettings["Controls"])}",
                $"{Enum.GetName(typeof(ConsoleKey), ControlsSettings["ExitTheApp"])}"
            };

            try
            {
                File.WriteAllLines(SettingsPath, text);
            }
            catch (Exception e)
            {
                Tools.ThrowException(e);
            }
        }

        public static bool loadingAnimation = true;
        /// <summary>
        /// Volume of sound effects and music.
        /// Min: 0
        /// Max: 1
        /// Default: 0.5
        /// </summary>
        private static float Volume = 1f;
        /// <summary>
        /// Pause between every player move in milliseconds. 
        /// Default: 220)
        /// </summary>
        private static int PlayerSpeed = 220;
        private static readonly string Version = File.ReadAllLines(VersionPath)[0];

        public static string VersionGet(bool justNumber = false)
        {
            if (justNumber)
                return Version;
            else
                return "version: " + Version;
        }
        public static void VolumeModify(bool add = false)
        {
            if (add)
            {
                if (Volume < 1f)
                    Volume += 0.1f;
            }
            else
            {
                if (Volume > 0f)
                    Volume -= 0.1f;
            }
        }

        public static void SpeedModify(bool add = false)
        {
            if (add)
            {
                if (PlayerSpeed < 1000)
                    PlayerSpeed += 10;
            }
            else
            {
                if (PlayerSpeed > 0)
                    PlayerSpeed -= 10;
            }
        }

        internal static float GetVolume()
        {
            //if (Volume < 1f)
            //    return Volume;
            //else
            //    Volume -= .1f;
            return Volume;

            //if (Volume < 1f && Volume > 0f)
            //    return Volume;
            //else
            //{
            //    if (Volume < 0)
            //        Volume += .1f;
            //    else if (Volume > 1f)
            //        Volume -= 1f;
            //}

            //return Volume;
        }

        internal static int GetPlayerSpeed()
        {
            return PlayerSpeed;
        }




        internal static bool DebugMode = false;
        internal static bool Music = true;


        private static void LoadControls()
        {
            string[] settings = File.ReadAllLines(SettingsPath);

            //ControlsSettings["MoveUp"] = Enum.Parse<ConsoleKey>(settings[3]);
            //ControlsSettings["MoveDown"] = Enum.Parse<ConsoleKey>(settings[4]);
            //ControlsSettings["MoveLeft"] = Enum.Parse<ConsoleKey>(settings[5]);
            //ControlsSettings["MoveRight"] = Enum.Parse<ConsoleKey>(settings[6]);

            //ControlsSettings["LeaveTheGame"] = Enum.Parse<ConsoleKey>(settings[7]);
            //ControlsSettings["Back"] = Enum.Parse<ConsoleKey>(settings[8]);
            //ControlsSettings["Select"] = Enum.Parse<ConsoleKey>(settings[9]);

            //ControlsSettings["StartTheGame"] = Enum.Parse<ConsoleKey>(settings[10]);
            //ControlsSettings["Settings"] = Enum.Parse<ConsoleKey>(settings[11]);
            //ControlsSettings["Controls"] = Enum.Parse<ConsoleKey>(settings[12]);
            //ControlsSettings["ExitTheApp"] = Enum.Parse<ConsoleKey>(settings[13]);
        }
    }
}
