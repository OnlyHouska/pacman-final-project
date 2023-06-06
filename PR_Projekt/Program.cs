using BasicFunctionsLibrary;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace PR_Projekt
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MetaDataCheck();

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX) && !File.Exists(@"MetaData/0U1cbVWY790cpProuDfkQdQMO8vAlhNC"))
            {
                Color.Red();
                Console.WriteLine("Oh shit a Mac... I'm sorry but this content is unaccesible for you. This is due to lack of skill getting a Windows\ncomputer.");
                Color.DarkYellow();
                Console.Write("If you have a MacAccess code you can use it there: ");
                string? macAccessCode = Console.ReadLine();
                if (macAccessCode != "1fyBZCoOkx3IiNCXFZ3lenlNoV")
                {
                    Color.Red();
                    Console.Write("Wrong acces code!");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
                else
                {
                    Color.Green();
                    File.Create(@"MetaData/0U1cbVWY790cpProuDfkQdQMO8vAlhNC");
                    Console.Write("MacAccess code is correct! Welcome!");
                    Console.ReadKey();
                }
            }
            Console.CursorVisible = false;
            Settings.LoadSettings();

            CancelKeyPress();

            Player p = new();
            Tools.PlaySound("theme");
            UI.Reddirect(UI.Location.MainMenu, p);
            Console.ReadKey();
        }

        public const string fixFilePath = "ReinstallFiles.py";
        public const string reinstallFilesCommand = "python " + fixFilePath;
        private static async void MetaDataCheck()
        {
            bool missingFiles = false;
            if (!File.Exists(Settings.SettingsPath))
            {
                Color.Red();
                Console.WriteLine($"Can't find the file {{{Settings.SettingsPath}}}");
                missingFiles = true;
            }
            if (!File.Exists(Tools.PatchNotesPath))
            {
                Color.Red();
                Console.WriteLine($"Can't find the file {{{Tools.PatchNotesPath}}}");
                missingFiles = true;
            }
            if (!File.Exists(Settings.VersionPath))
            {
                Color.Red();
                Console.WriteLine($"Can't find the file {{{Settings.VersionPath}}}");
                missingFiles = true;
            }
            if (!File.Exists(Tools.BitmapPath))
            {
                Color.Red();
                Console.WriteLine($"Can't find the file {{{Tools.BitmapPath}}}");
                missingFiles = true;
            }
            if (!File.Exists(Tools.IssuesPath))
            {
                Color.Red();
                Console.WriteLine($"Can't find the file {{{Tools.IssuesPath}}}");
                missingFiles = true;
            }
            if (!File.Exists(Tools.TipsPath))
            {
                Color.Red();
                Console.WriteLine($"Can't find the file {{{Tools.TipsPath}}}");
                missingFiles = true;
            }

            if (missingFiles)
            {
                Console.WriteLine($"\n\nYou can fix the error by running the file {fixFilePath}.");
                Color.DarkYellow();
                Console.WriteLine("<ENTER> to fix automatically");
                Console.Write("<ESC> to exit");

                ConsoleKey key = Console.ReadKey(true).Key;
                bool inMenu = true;
                while (inMenu)
                {
                    switch (key)
                    {
                        case ConsoleKey.Enter:
                            Repair();
                            Console.SetCursorPosition(0, 0);
                            Tools.LoadingScreenAnimation();
                            Console.SetCursorPosition(0, 0);
                            inMenu = false;
                            break;
                        case ConsoleKey.Escape:
                            inMenu = false;
                            break;
                    }
                }
                Environment.Exit(0);
            }
        }

        private static void Repair()
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = reinstallFilesCommand;
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            start.CreateNoWindow = true;

            try
            {
                using (Process process = Process.Start(start))
                {
                    using (StreamReader reader = process.StandardOutput)
                    {
                        string result = reader.ReadToEnd();
                        Console.Write(result);
                    }
                }
            }
            catch (Win32Exception e)
            {
                Tools.ThrowException(e);
            }
        }

        public static void CancelKeyPress()
        {
            Console.CancelKeyPress += Console_CancelKeyPress;
        }

        private static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            e.Cancel = true;
        }
    }
}