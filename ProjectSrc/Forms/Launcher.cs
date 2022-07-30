using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.Win32;
using RobloxDeployHistory;

namespace RobloxPlayerModManager
{
    public partial class Launcher : Form
    {
        private static RegistryKey versionRegistry => Program.GetSubKey("VersionData");
        private readonly string[] args = null;

        public Launcher(params string[] mainArgs)
        {
            if (mainArgs.Length > 0)
                args = mainArgs;

            InitializeComponent();
        }

        private string getSelectedBranch()
        {
            var result = branchSelect.SelectedItem;
            return result.ToString();
        }

        private void Launcher_Load(object sender, EventArgs e)
        {
            if (args != null)
                openPlayerDirectory.Enabled = false;

            string build = Program.GetString("BuildBranch");
            int buildIndex = branchSelect.Items.IndexOf(build);
            branchSelect.SelectedIndex = Math.Max(buildIndex, 0);
        }

        public static string getModPath()
        {
            string appData = Environment.GetEnvironmentVariable("AppData");
            string root = Path.Combine(appData, "RbxModManager", "ModFiles");

            if (!Directory.Exists(root))
            {
                // Build a folder structure so the usage of my mod manager is more clear.
                Directory.CreateDirectory(root);

                string[] folderPaths = new string[]
                {
                    "BuiltInPlugins",
                    "ClientSettings",

                    "content/avatar",
                    "content/fonts",
                    "content/models",
                    "content/scripts",
                    "content/sky",
                    "content/sounds",
                    "content/textures",
                    "content/translations"
                };

                foreach (string f in folderPaths)
                {
                    string path = Path.Combine(root, f.Replace("/", "\\"));
                    Directory.CreateDirectory(path);
                }
            }

            return root;
        }

        private void manageMods_Click(object sender, EventArgs e)
        {
            string modPath = getModPath();

            var open = new ProcessStartInfo()
            {
                FileName = modPath,
                UseShellExecute = true,
                Verb = "open"
            };

            Process.Start(open);
        }

        private static Form createFlagWarningPrompt()
        {
            var warningForm = new Form()
            {
                Text = "WARNING: HERE BE DRAGONS",

                Width = 700,
                Height = 400,
                MaximizeBox = false,
                MinimizeBox = false,

                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterScreen,

                ShowInTaskbar = false
            };

            var errorIcon = new PictureBox()
            {
                BackgroundImage = SystemIcons.Error.ToBitmap(),
                BackgroundImageLayout = ImageLayout.Zoom,
                Location = new Point(12, 12),
                Size = new Size(48, 48),
            };

            var dontShowAgain = new CheckBox()
            {
                AutoSize = true,
                Location = new Point(85, 235),
                Text = "Do not show this warning again.",
                Font = new Font("Segoe UI", 11f),
            };

            var buttonPanel = new FlowLayoutPanel()
            {
                FlowDirection = FlowDirection.RightToLeft,
                BackColor = SystemColors.ControlLight,
                Padding = new Padding(4),
                Dock = DockStyle.Bottom,
                Size = new Size(0, 60)
            };

            var infoLabel = new Label()
            {
                AutoSize = true,

                Font = new Font("Segoe UI", 11f),

                Text = "Editing flags can make Roblox Player unstable, and could potentially corrupt your places and game data.\n\n" +
                       "You should not edit them unless you are just experimenting with new features locally, and you know what you're doing.\n\n" +
                       "Are you sure you would like to continue?",

                Location = new Point(80, 14),
                MaximumSize = new Size(600, 0),
            };

            var yes = new Button()
            {
                Size = new Size(150, 40),
                Text = "Yes",
            };

            var no = new Button()
            {
                Size = new Size(150, 40),
                Text = "No",
            };

            yes.Click += (sender, e) =>
            {
                warningForm.DialogResult = DialogResult.Yes;
                warningForm.Enabled = dontShowAgain.Checked;
                warningForm.Close();
            };

            no.Click += (sender, e) =>
            {
                warningForm.DialogResult = DialogResult.No;
                warningForm.Enabled = dontShowAgain.Checked;
                warningForm.Close();
            };

            buttonPanel.Controls.Add(no);
            buttonPanel.Controls.Add(yes);

            warningForm.Controls.Add(errorIcon);
            warningForm.Controls.Add(infoLabel);
            warningForm.Controls.Add(buttonPanel);
            warningForm.Controls.Add(dontShowAgain);

            return warningForm;
        }

        private async void launchPlayer_Click(object sender = null, EventArgs e = null)
        {
            string branch = getSelectedBranch();

            var bootstrapper = new PlayerBootstrapper
            {
                ForceInstall = forceRebuild.Checked,
                dontUpdate = dontUpdate1.Checked,
                ApplyModManagerPatches = true,

                SetStartEvent = true,
                Branch = branch
            };

            Hide();

            using (var installer = new BootstrapperForm(bootstrapper))
            {
                var install = installer.Bootstrap();
                await install.ConfigureAwait(true);
            }
            
            string PlayerRoot = PlayerBootstrapper.GetPlayerDirectory();
            string modPath = getModPath();

            string[] modFiles = Directory.GetFiles(modPath, "*.*", SearchOption.AllDirectories);

            foreach (string modFile in modFiles)
            {
                try
                {
                    byte[] fileContents = File.ReadAllBytes(modFile);
                    FileInfo modFileControl = new FileInfo(modFile);

                    string relativeFile = modFile.Replace(modPath, PlayerRoot);

                    string relativeDir = Directory
                        .GetParent(relativeFile)
                        .ToString();

                    if (!Directory.Exists(relativeDir))
                        Directory.CreateDirectory(relativeDir);

                    if (File.Exists(relativeFile))
                    {
                        byte[] relativeContents = File.ReadAllBytes(relativeFile);

                        if (fileContents.SequenceEqual(relativeContents))
                            continue;

                        modFileControl.CopyTo(relativeFile, true);
                        continue;
                    }

                    File.WriteAllBytes(relativeFile, fileContents);
                }
                catch (IOException)
                {
                    Console.WriteLine("Failed to overwrite {0}!", modFile);
                }
            }

            var robloxPlayerInfo = new ProcessStartInfo()
            {
                FileName = PlayerBootstrapper.GetPlayerPath(),
                Arguments = $"-startEvent {PlayerBootstrapper.StartEvent}"
            };

            if (args != null)
            {
                string firstArg = args[0];

                if (firstArg != null && firstArg.StartsWith("roblox-player", Program.StringFormat))
                {
                    // Arguments were passed by URI.
                    var argMap = new Dictionary<string, string>();

                    foreach (string commandPair in firstArg.Split('+'))
                    {
                        if (commandPair.Contains(':'))
                        {
                            string[] kvPair = commandPair.Split(':');

                            string key = kvPair[0];
                            string val = kvPair[1];

                            //if (key == "gameinfo")
                            //{
                            //    // The user is authenticating. This argument is a special case.
                            //    robloxPlayerInfo.Arguments += " -url https://www.roblox.com/Login/Negotiate.ashx -ticket " + val;
                            //}
                            //else
                            //{
                            argMap.Add(key, val);
                            // robloxPlayerInfo.Arguments += " -" + key + ' ' + val;
                            // }
                        }
                    }

                    if (argMap.ContainsKey("launchmode") && !argMap.ContainsKey("task"))
                    {
                        string launchMode = argMap["launchmode"];

                        if (launchMode == "play")
                        {
                            string gameinfo = argMap["gameinfo"];
                            string launchtime = argMap["launchtime"];
                            string placelauncherurl = argMap["placelauncherurl"].Replace("%3A",":").Replace("%2F","/").Replace("%3F","?").Replace("%3D","=").Replace("%26","&");
                            string browsertrackerid = argMap["browsertrackerid"];
                            string robloxLocale = argMap["robloxLocale"];
                            string gameLocale = argMap["gameLocale"];
                            robloxPlayerInfo.Arguments += " --play -t " + gameinfo + " -j " + placelauncherurl + " -b " + browsertrackerid + " --launchtime " + launchtime + " --rloc " + robloxLocale + " --gloc " + gameLocale;
                        }
                    }
                }
                else
                {
                    // Arguments were passed directly.
                    for (int i = 0; i < args.Length; i++)
                    {
                        string arg = args[i];

                        if (arg.Contains(' '))
                            arg = $"\"{arg}\"";

                        robloxPlayerInfo.Arguments += ' ' + arg;
                    }
                }
            }

            if (openPlayerDirectory.Checked)
            {
                Process.Start(PlayerRoot);
                Environment.Exit(0);
            }
            else
            {
                string currentVersion = versionRegistry.GetString("VersionGuid");
                versionRegistry.SetValue("LastExecutedVersion", currentVersion);

                Process.Start(robloxPlayerInfo);
            }
        }

        private async void branchSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Save the user's branch preference.
            string branch = getSelectedBranch();
            Program.SetValue("BuildBranch", branch);

            // Grab the version currently being targetted.
            string targetId = Program.GetString("TargetVersion");

            // Clear the current list of target items.
            targetVersion.Items.Clear();
            targetVersion.Items.Add("(Use Latest)");

            // Populate the items list using the deploy history.
            Enabled = false;
            UseWaitCursor = true;

            var getDeployLogs = PlayerDeployLogs.Get(branch);
            var deployLogs = await getDeployLogs.ConfigureAwait(true);

            Enabled = true;
            UseWaitCursor = false;

            HashSet<DeployLog> targets;

            //if (Environment.Is64BitOperatingSystem)
            //    targets = deployLogs.CurrentLogs_x64;
            //else
            //    targets = deployLogs.CurrentLogs_x86;

            targets = deployLogs.CurrentLogs_x86; // No 64-bit player yet.

            var items = targets
                .OrderByDescending(log => log.Changelist)
                .Cast<object>()
                .Skip(1)
                .ToArray();

            targetVersion.Items.AddRange(items);

            // Select the deploy log being targetted.
            DeployLog target = targets
                .Where(log => log.VersionId == targetId)
                .FirstOrDefault();

            if (target != null)
            {
                targetVersion.SelectedItem = target;
                return;
            }

            // If the target isn't valid, fallback to the latest version.
            targetVersion.SelectedIndex = 0;
        }

        private void targetVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (targetVersion.SelectedIndex == 0)
            {
                Program.SetValue("TargetVersion", "");
                return;
            }

            var target = targetVersion.SelectedItem as DeployLog;
            Program.SetValue("TargetVersion", target.VersionId);
        }

        private bool reverting = false;

        private void revertButton_Click(object sender, EventArgs e)
        {
            if (reverting)
                return;
            reverting = true;
            var request = WebRequest.Create("http://setup.roblox.com/version");
            request.Method = "GET";

            var webResponse = request.GetResponse();
            var webStream = webResponse.GetResponseStream();

            var reader = new StreamReader(webStream);
            var data = reader.ReadToEnd();
            reader.Dispose();
            webStream.Close();


            using (var client = new WebClient())
            {
                string fp = Path.Combine(Environment.GetEnvironmentVariable("temp"), "RobloxPlayerLauncher.exe");
                if (File.Exists(fp))
                {
                    File.Delete(fp);
                }
                client.DownloadFile("https://setup.rbxcdn.com/" + data + "-Roblox.exe", fp);
                var open = new ProcessStartInfo()
                {
                    FileName = fp,
                    UseShellExecute = true,
                    Verb = "open"
                };

                try
                {
                    Directory.Delete(PlayerBootstrapper.GetPlayerDirectory(), true);
                }
                catch { }
                try
                {
                    string localAppData = Environment.GetEnvironmentVariable("LocalAppData");

                    string settingsDir = Path.Combine(localAppData, "Roblox", "ClientSettings");
                    string settingsPath = Path.Combine(settingsDir, "ClientAppSettings.json");
                    if (File.Exists(settingsPath))
                    {
                        File.Delete(settingsPath);
                    }
                    else
                    {
                        localAppData = Environment.GetEnvironmentVariable("programfiles");

                        settingsDir = Path.Combine(localAppData, "Roblox", "ClientSettings");
                        settingsPath = Path.Combine(settingsDir, "ClientAppSettings.json");
                        if (File.Exists(settingsPath))
                        {
                            File.Delete(settingsPath);
                        }
                        else
                        {
                            localAppData = Environment.GetEnvironmentVariable("programfiles(x86)");

                            settingsDir = Path.Combine(localAppData, "Roblox", "ClientSettings");
                            settingsPath = Path.Combine(settingsDir, "ClientAppSettings.json");
                            if (File.Exists(settingsPath))
                            {
                                File.Delete(settingsPath);
                            }
                        }
                    }
                }
                catch { }

                try
                {
                    try
                    {
                        Registry.CurrentUser.GetSubKey("SOFTWARE").DeleteSubKey("Roblox Player Mod Manager");
                    }
                    catch { }

                    var stateFile = Path.Combine(Program.RootDir, "state.json");
                    if (File.Exists(stateFile))
                    {
                        File.Delete(stateFile);
                    }
                }
                catch { }

                Process.Start(open);
                Application.Exit();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.linkLabel1.LinkVisited = true;
            Process.Start("https://github.com/Malte0621/Roblox-Player-Mod-Manager");
        }

        private async void openFlagEditor_ClickAsync(object sender, EventArgs e)
        {
            bool allow = true;

            // Create a warning prompt if the user hasn't disabled this warning.
            var warningDisabled = Program.State.DisableFlagWarning;

            if (!warningDisabled)
            {
                SystemSounds.Hand.Play();
                allow = false;

                using (Form warningPrompt = createFlagWarningPrompt())
                {
                    warningPrompt.ShowDialog();

                    if (warningPrompt.DialogResult == DialogResult.Yes)
                    {
                        Program.State.DisableFlagWarning = warningPrompt.Enabled;
                        allow = true;
                    }
                }
            }

            if (allow)
            {
                string branch = getSelectedBranch();

                Enabled = false;
                UseWaitCursor = true;

                var infoTask = PlayerBootstrapper.GetCurrentVersionInfo(branch);
                var info = await infoTask.ConfigureAwait(true);

                Hide();

                var updateTask = BootstrapperForm.BringUpToDate(branch, info.VersionGuid, "Some newer flags might be missing.");
                await updateTask.ConfigureAwait(true);

                using (FlagEditor editor = new FlagEditor())
                    editor.ShowDialog();

                Show();
                BringToFront();

                Enabled = true;
                UseWaitCursor = false;
            }
        }
    }
}