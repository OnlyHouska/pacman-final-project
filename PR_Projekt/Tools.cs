using NAudio.Wave;
using Color = BasicFunctionsLibrary.Color;
using System.Drawing;

namespace PR_Projekt
{
    internal class Tools
    {
        public static bool patchNotesRead = false;

        public const string BitmapPath = @"MetaData/bitmap.png";
        public const string AudioPath = @"MetaData/Audio";
        public const string PatchNotesPath = @"MetaData/$patch-notes";
        public const string IssuesPath = @"MetaData/$issues";
        public const string TipsPath = @"MetaData/tips.txt";

        /// <summary>
        /// Public random number generator
        /// </summary>
        public static Random rnd = new();

        //private static Dictionary<string, string> audioFilePaths;
        private static WaveOutEvent? waveOut;
        private static readonly List<WaveOutEvent> players = new();

        public static void PlaySound(string name)
        {
            if (Settings.Music)
            {
                waveOut = new()
                {
                    Volume = Settings.GetVolume()
                };

                WaveStream waveStream;
                if (File.Exists($"{AudioPath}/{name}.mp3"))
                    waveStream = new Mp3FileReader($"{AudioPath}/{name}.mp3");
                else if (File.Exists($"{AudioPath}/{name}.wav"))
                    waveStream = new WaveFileReader($"{AudioPath}/{name}.wav");
                else
                    throw new FileNotFoundException($"Internal error: Audio file '{name}' not found.");

                if (waveOut != null)
                {
                    waveOut.Dispose();
                    waveOut.Stop();
                    waveOut.Init(waveStream);
                }
                else
                    waveOut.Init(waveStream);
                waveOut.Play();
                players.Add(waveOut);
            }
        }

        public static void StopMusic()
        {
            foreach (var waveOut in players)
            {
                waveOut.Stop();
                waveOut.Dispose();
            }

            players.Clear();
            waveOut = null;
        }

        internal static void ThrowException(Exception e)
        {
            Console.Clear();
            Color.Red();
            Console.WriteLine("An error occured - " + e);
            Environment.Exit(0);
        }

        internal static TileState[,] ReadBitMap(TileState[,] _map)
        {
            Game.TotalDots = 0;
            Bitmap? colorMap = new(BitmapPath);

            if (colorMap != null)
            {
                for (int x = 0; x < colorMap.Width; x++)
                {
                    for (int y = 0; y < colorMap.Height; y++)
                    {
                        System.Drawing.Color pixelColor = colorMap.GetPixel(x, y);

                        if (pixelColor == System.Drawing.Color.FromArgb(255, 0, 0, 0)) //Black - WallTop
                            _map[y, x] = TileState.WallTop;
                        else if (pixelColor == System.Drawing.Color.FromArgb(255, 255, 255, 255)) //White - Dot
                        {
                            Game.TotalDots++;
                            _map[y, x] = TileState.Dot;
                        }
                        else if (pixelColor == System.Drawing.Color.FromArgb(255, 255, 255, 0)) //Yellow - WallSide
                            _map[y, x] = TileState.WallSide;

                        else if (pixelColor == System.Drawing.Color.FromArgb(255, 0, 0, 255)) //Blue - BottomLeft
                            _map[y, x] = TileState.BottomLeft;
                        else if (pixelColor == System.Drawing.Color.FromArgb(255, 255, 0, 0)) //Red - BottomRight
                            _map[y, x] = TileState.BottomRight;

                        else if (pixelColor == System.Drawing.Color.FromArgb(255, 0, 255, 255)) //Cyan - TopRight
                            _map[y, x] = TileState.TopRight;
                        else if (pixelColor == System.Drawing.Color.FromArgb(255, 0, 255, 0)) //Green - TopLeft
                            _map[y, x] = TileState.TopLeft;

                        else if (pixelColor == System.Drawing.Color.FromArgb(255, 125, 0, 255)) //Purple - UpTripple
                            _map[y, x] = TileState.UpTriple;

                        else if (pixelColor == System.Drawing.Color.FromArgb(255, 255, 125, 0)) //Orange - PowerPellet
                            _map[y, x] = TileState.PowerPellet;
                        else if (pixelColor == System.Drawing.Color.FromArgb(255, 0, 125, 255)) //Light Blue - Fruit
                        {
                            Game.TotalFruits++;
                            _map[y, x] = TileState.Fruit;
                        }
                        else if (pixelColor == System.Drawing.Color.FromArgb(255, 255, 155, 255)) //Pink - GhostSpawn
                            _map[y, x] = TileState.GhostSpawn;

                        else
                            _map[y, x] = TileState.Blank;
                    }
                }

                return _map;
            }
            else
            {
                throw new Exception("Bitmap failed to load on a path" + BitmapPath);
            }

        }

        public static void LoadingScreenAnimation(bool tips = false)
        {
            Console.Clear();
            if (tips)
            {
                string tip = GenerateRandomTip();
                Color.Magenta();
                Console.SetCursorPosition(Console.WindowWidth / 2 - tip.Length / 3, Console.WindowHeight / 2 - 2);
                Console.Write(tip);
            }
            Color.Green();
            Console.Title = "Loading...";
            Console.SetCursorPosition(Console.WindowWidth / 2 - 4, Console.WindowHeight / 2);
            Console.Write("█▒▒▒▒▒▒▒▒▒ 10%");
            Thread.Sleep(200);
            Console.SetCursorPosition(Console.WindowWidth / 2 - 4, Console.WindowHeight / 2);
            Console.Write("██▒▒▒▒▒▒▒▒ 20%");
            Thread.Sleep(400);
            Console.SetCursorPosition(Console.WindowWidth / 2 - 4, Console.WindowHeight / 2);
            Console.Write("███▒▒▒▒▒▒▒ 30%");
            Thread.Sleep(200);
            Console.SetCursorPosition(Console.WindowWidth / 2 - 4, Console.WindowHeight / 2);
            Console.Write("████▒▒▒▒▒▒ 40%");
            Thread.Sleep(200);
            Console.SetCursorPosition(Console.WindowWidth / 2 - 4, Console.WindowHeight / 2);
            Console.Write("█████▒▒▒▒▒ 50%");
            Thread.Sleep(100);
            Console.SetCursorPosition(Console.WindowWidth / 2 - 4, Console.WindowHeight / 2);
            Console.Write("██████▒▒▒▒ 60%");
            Thread.Sleep(200);
            Console.SetCursorPosition(Console.WindowWidth / 2 - 4, Console.WindowHeight / 2);
            Console.Write("███████▒▒▒ 70%");
            Thread.Sleep(200);
            Console.SetCursorPosition(Console.WindowWidth / 2 - 4, Console.WindowHeight / 2);
            Console.Write("████████▒▒ 80%");
            Thread.Sleep(200);
            Console.SetCursorPosition(Console.WindowWidth / 2 - 4, Console.WindowHeight / 2);
            Console.Write("█████████▒ 90%");
            Thread.Sleep(100);
            Console.SetCursorPosition(Console.WindowWidth / 2 - 4, Console.WindowHeight / 2);
            Console.Write("██████████ 100%");
            Thread.Sleep(200);
            Console.SetCursorPosition(Console.WindowWidth / 2 - 4, Console.WindowHeight / 2);
        }

        public static string GenerateRandomTip()
        {
            string[] tips = File.ReadAllLines(TipsPath);

            int rnd = Tools.rnd.Next(tips.Length);

            return "TIP: " + tips[rnd];
        }

        internal static void ShowPatchNotes(Player p)
        {
            Console.Clear();

            Color.DarkYellow();
            Console.WriteLine("Patch Notes\n");

            Color.Green();
            Console.WriteLine("Patch " + Settings.VersionGet(true) + "\n");

            Color.Yellow();
            string[] text = File.ReadAllLines(PatchNotesPath);
            foreach (string line in text)
            {
                Console.Write(line + "\n");
            }

            Color.DarkYellow();
            Console.WriteLine("\n\n\nOngoing Issues\n");

            Color.Yellow();
            string[] issues = File.ReadAllLines(IssuesPath);
            foreach (string line in issues)
            {
                Console.Write(line + "\n");
            }

            //string keyword = "<red>";
            //string colorKeyword = "<blue>";

            //string[] lines = File.ReadAllLines(PatchNotesPath);

            //for (int i = 0; i < lines.Length; i++)
            //{
            //    string line = lines[i];
            //    int startIndex = line.IndexOf(keyword);

            //    while (startIndex >= 0)
            //    {
            //        int endIndex = line.IndexOf(">", startIndex + keyword.Length);
            //        if (endIndex >= 0)
            //        {
            //            int nextKeywordIndex = line.IndexOf("<", endIndex + 1);
            //            if (nextKeywordIndex >= 0)
            //            {
            //                line = line.Remove(startIndex, nextKeywordIndex - startIndex);
            //            }
            //            else
            //            {
            //                line = line.Remove(startIndex);
            //            }
            //        }

            //        startIndex = line.IndexOf(keyword, startIndex + keyword.Length);
            //    }

            //    startIndex = line.IndexOf(colorKeyword);
            //    if (startIndex >= 0)
            //    {
            //        int endIndex = line.IndexOf(">", startIndex + colorKeyword.Length);
            //        if (endIndex >= 0)
            //        {
            //            line = line.Remove(startIndex, endIndex - startIndex + 1);
            //            Console.Write(line.Substring(0, startIndex));
            //            BasicFunctionsLibrary.Color.Blue();
            //            Console.Write(line.Substring(startIndex));
            //        }
            //    }
            //    else
            //    {
            //        Console.WriteLine(line);
            //    }
            //}

            Console.ReadKey();

            patchNotesRead = true;
            UI.Reddirect(UI.Location.MainMenu, p);
        }
    }
}
