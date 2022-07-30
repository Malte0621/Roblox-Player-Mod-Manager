using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Microsoft.Win32;

namespace RobloxPlayerModManager
{
    static class Program
    {
        [DllImport("Shcore.dll")]
        static extern int SetProcessDpiAwareness(int PROCESS_DPI_AWARENESS);
        
        public static readonly RegistryKey MainRegistry = Registry.CurrentUser.GetSubKey("SOFTWARE", "Roblox Player Mod Manager");
        public static readonly RegistryKey VersionRegistry = MainRegistry.GetSubKey("VersionData");

        public static readonly CultureInfo Format = CultureInfo.InvariantCulture;
        public const StringComparison StringFormat = StringComparison.InvariantCulture;
        public static readonly NumberFormatInfo NumberFormat = NumberFormatInfo.InvariantInfo;

        public static string RootDir { get; private set; }
        public static ModManagerState State { get; private set; }

        private static readonly Regex jsonPattern = new Regex("\"([^\"]*)\":\"?([^\"]*)\"?[,|}]");

        public static RegistryKey GetSubKey(this RegistryKey key, params string[] path)
        {
            string constructedPath = Path.Combine(path);
            return key.CreateSubKey(constructedPath, RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryOptions.None);
        }

        public static string GetString(this RegistryKey key, string name, string fallback = "")
        {
            var result = key.GetValue(name, fallback);
            return result.ToString();
        }

        public static bool GetBool(this RegistryKey key, string name)
        {
            string value = key.GetString(name);

            if (!bool.TryParse(value, out bool result))
                return false;

            return result;
        }

        public static int GetInt(this RegistryKey key, string name)
        {
            string value = key.GetString(name);

            if (!int.TryParse(value, out int result))
                return 0;

            return result;
        }

        public static RegistryKey GetSubKey(params string[] path)
        {
            return MainRegistry.GetSubKey(path);
        }

        public static string GetString(string name, string fallback = "")
        {
            return MainRegistry.GetString(name, fallback);
        }

        public static bool GetBool(string name)
        {
            return MainRegistry.GetBool(name);
        }

        public static int GetInt(string name)
        {
            return MainRegistry.GetInt(name);
        }

        public static void SetValue(string name, object value)
        {
            MainRegistry.SetValue(name, value);
        }

        public static Dictionary<string, string> ReadJsonDictionary(string json)
        {
            var matches = jsonPattern.Matches(json);
            var result = new Dictionary<string, string>();

            foreach (Match match in matches)
            {
                var data = match.Groups
                    .Cast<Group>()
                    .Select(g => g.Value)
                    .ToArray();

                string key = data[1],
                       val = data[2];

                result.Add(key, val);
            }

            return result;
        }

        // This sets up the following:
        // 1: The URI Protcol to join places from the website through my mod manager.

        public static void UpdatePlayerRegistryProtocols()
        {
            const string _ = ""; // Default empty key/value.

            string modManagerPath = Application.ExecutablePath
                .Replace('"', ' ')
                .Trim();

            // Register the base "Roblox.Place" open protocol.
            RegistryKey classes = Registry.CurrentUser.GetSubKey("SOFTWARE", "Classes");

            RegistryKey robloxPlace = classes.GetSubKey("Roblox.Place");
            robloxPlace.SetValue(_, "Roblox Place");

            // Setup the URI protocol for opening the mod manager through the website.
            RegistryKey robloxPlayerUrl = GetSubKey(classes, "roblox-player");
            robloxPlayerUrl.SetValue(_, "URL: Roblox Protocol");
            robloxPlayerUrl.SetValue("URL Protocol", _);

            RegistryKey PlayerUrlCmd = GetSubKey(robloxPlayerUrl, "shell", "open", "command");
            PlayerUrlCmd.SetValue(_, modManagerPath + " %1");

            // Set the default icon for both protocols.
            RegistryKey[] appReg =
            {
                robloxPlace,
                robloxPlayerUrl
            };

            foreach (RegistryKey app in appReg)
            {
                RegistryKey defaultIcon = GetSubKey(app, "DefaultIcon");
                defaultIcon.SetValue(_, $"{modManagerPath},0");
            }
        }

        static void ConvertLegacy(RegistryKey regKey, JObject node)
        {
            foreach (var name in regKey.GetValueNames())
            {
                string key = name.Replace(" ", "");
                object value = regKey.GetValue(name);

                var token = JToken.FromObject(value);
                node.Add(key, token);
            }

            foreach (var subKeyName in regKey.GetSubKeyNames())
            {
                var subKey = regKey.OpenSubKey(subKeyName);
                var child = new JObject();

                ConvertLegacy(subKey, child);
                node.Add(subKeyName, child);
            }
        }

        static Program()
        {
            const int SYSTEM_AWARE = 1;
            _ = SetProcessDpiAwareness(SYSTEM_AWARE);
        }

        public static void SaveState()
        {
            var stateFile = Path.Combine(RootDir, "state.json");
            string json = JsonConvert.SerializeObject(State, Formatting.Indented);
            File.WriteAllText(stateFile, json);
        }

        static void OnExiting(object sender, EventArgs e)
        {
            SaveState();
        }

        [STAThread]
        static void Main(string[] args)
        {
            // Initialize application state.
            var localAppData = Environment.GetEnvironmentVariable("localappdata");
            RootDir = Path.Combine(localAppData, "Roblox Player Mod Manager");

            if (!Directory.Exists(RootDir))
                Directory.CreateDirectory(RootDir);

            var stateFile = Path.Combine(RootDir, "state.json");
            string json = "";

            if (File.Exists(stateFile))
                json = File.ReadAllText(stateFile);

            if (string.IsNullOrEmpty(json))
            {
                var root = new JObject();
                ConvertLegacy(MainRegistry, root);

                json = root.ToString();
                File.WriteAllText(stateFile, json);
            }

            try
            {
                State = JsonConvert.DeserializeObject<ModManagerState>(json);
            }
            catch
            {
                State = new ModManagerState();
            }

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new Launcher(args));
        }
    }
}
