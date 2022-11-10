using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Windows.Forms;

using RobloxDeployHistory;

namespace RobloxPlayerModManager
{
    public partial class Launcher : Form
    {
        private static VersionManifest versionRegistry => Program.State.VersionData;
        private readonly string[] args = null;

        public Launcher(params string[] mainArgs)
        {
            if (mainArgs.Length > 0)
                args = mainArgs;

            InitializeComponent();
        }

        private Channel getSelectedChannel()
        {
            var result = channelSelect.SelectedItem;
            return result.ToString();
        }

        private async void Launcher_Load(object sender, EventArgs e)
        {
            Enabled = false;
            UseWaitCursor = true;

            if (args != null)
                openPlayerDirectory.Enabled = false;

            var channels = await PlayerBootstrapper.FetchKnownChannels();

            var setChannels = new Action(() =>
            {
                var items = channelSelect.Items;
                var channel = Program.State.Channel;

                items.Clear();
                items.AddRange(channels);

                selectChannel(channel);
            });

            Invoke(setChannels);
        }

        public static string getModPath()
        {
            string root = Path.Combine(Program.RootDir, "ModFiles");

            if (!Directory.Exists(root))
            {
                // Build a folder structure so the usage is more clear.
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
                    string path = Path.Combine(root, f);
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

                Width = 425,
                Height = 250,
                MaximizeBox = false,
                MinimizeBox = false,

                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterScreen,

                ShowInTaskbar = false
            };

            var errorIcon = new PictureBox()
            {
                Image = SystemIcons.Error.ToBitmap(),
                Location = new Point(12, 12),
                Size = new Size(32, 32),
            };

            var dontShowAgain = new CheckBox()
            {
                AutoSize = true,
                Location = new Point(54, 145),
                Text = "Do not show this warning again.",
                Font = new Font("Microsoft Sans Serif", 9.75f),
            };

            var buttonPanel = new FlowLayoutPanel()
            {
                FlowDirection = FlowDirection.RightToLeft,
                BackColor = SystemColors.ControlLight,
                Padding = new Padding(4),
                Dock = DockStyle.Bottom,
                Size = new Size(0, 40)
            };

            var infoLabel = new Label()
            {
                AutoSize = true,

                Font = new Font("Microsoft Sans Serif", 9.75f),
                Text = "Editing flags can make Roblox Player unstable, and could potentially corrupt your places and game data.\n\n" +
                       "You should not edit them unless you are just experimenting with new features locally, and you know what you're doing.\n\n" +
                       "Are you sure you would like to continue?",

                Location = new Point(50, 14),
                MaximumSize = new Size(350, 0),
            };

            var yes = new Button()
            {
                Size = new Size(100, 23),
                Text = "Yes",
            };

            var no = new Button()
            {
                Size = new Size(100, 23),
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

        private async void editFVariables_Click(object sender, EventArgs e)
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
                var channel = getSelectedChannel();

                Enabled = false;
                UseWaitCursor = true;

                var infoTask = PlayerBootstrapper.GetCurrentVersionInfo(channel);
                var info = await infoTask.ConfigureAwait(true);

                Hide();

                var updateTask = BootstrapperForm.BringUpToDate(channel, info.VersionGuid, "Some newer flags might be missing.");
                await updateTask.ConfigureAwait(true);

                using (FlagEditor editor = new FlagEditor())
                    editor.ShowDialog();

                Show();
                BringToFront();

                Enabled = true;
                UseWaitCursor = false;
            }
        }

        private async void launchPlayer_Click(object sender = null, EventArgs e = null)
        {
            var channel = getSelectedChannel();

            var bootstrapper = new PlayerBootstrapper
            {
                ForceInstall = forceRebuild.Checked,
                ApplyModManagerPatches = true,

                SetStartEvent = true,
                Channel = channel
            };

            Hide();

            using (var installer = new BootstrapperForm(bootstrapper))
            {
                var install = installer.Bootstrap();
                await install.ConfigureAwait(true);
            }

            string PlayerRoot = PlayerBootstrapper.GetPlayerDirectory();
            string modPath = getModPath();

            var modFiles = Directory.GetFiles(modPath, "*.*", SearchOption.AllDirectories);

            foreach (string file in modFiles)
            {
                try
                {
                    var info = new FileInfo(file);
                    var filePath = file;
                    var delete = false;

                    if (info.Length == 0 && info.Name.StartsWith("DELETE"))
                    {
                        var dir = info.DirectoryName;

                        var realName = info.Name
                            .Substring(6)
                            .TrimStart();

                        filePath = Path.Combine(dir, realName);
                        delete = true;
                    }

                    string relativeFile = filePath.Replace(modPath, PlayerRoot);

                    string relativeDir = Directory
                        .GetParent(relativeFile)
                        .ToString();

                    if (!Directory.Exists(relativeDir))
                        Directory.CreateDirectory(relativeDir);

                    byte[] contents = info.Length > 0
                        ? File.ReadAllBytes(file)
                        : Array.Empty<byte>();

                    if (File.Exists(relativeFile))
                    {
                        byte[] relative = File.ReadAllBytes(relativeFile);

                        if (relative.Length == contents.Length)
                            if (relative.SequenceEqual(contents))
                                continue;

                        if (delete)
                        {
                            File.Delete(relativeFile);
                            continue;
                        }

                        info.CopyTo(relativeFile, true);
                    }
                    else
                    {
                        if (delete)
                            continue;

                        File.WriteAllBytes(relativeFile, contents);
                    }
                }
                catch
                {
                    Console.WriteLine("Failed to overwrite {0}!", file);
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
                            //    robloxPlayerInfo.Arguments += " -" + key + ' ' + val;
                            //}
                        }
                    }

                    if (argMap.ContainsKey("launchmode") && !argMap.ContainsKey("task"))
                    {
                        string launchMode = argMap["launchmode"];

                        if (launchMode == "play")
                        {
                            string gameinfo = argMap["gameinfo"];
                            string launchtime = argMap["launchtime"];
                            string placelauncherurl = argMap["placelauncherurl"].Replace("%3A", ":").Replace("%2F", "/").Replace("%3F", "?").Replace("%3D", "=").Replace("%26", "&");
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
                string currentVersion = versionRegistry.VersionGuid;
                versionRegistry.LastExecutedVersion = currentVersion;

                Process.Start(robloxPlayerInfo);
            }
        }

        private async void channelSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Save the user's branch preference.
            var channel = getSelectedChannel();
            Program.State.Channel = channel;
            Program.SaveState();

            // Grab the version currently being targetted.
            string targetId = Program.State.TargetVersion;
            const string latest = "(Use Latest)";

            // Clear the current list of target items.
            targetVersion.Items.Clear();
            targetVersion.Items.Add(latest);

            // Populate the items list using the deploy history.
            Enabled = false;
            UseWaitCursor = true;

            var getDeployLogs = PlayerDeployLogs.Get(channel);
            var deployLogs = await getDeployLogs.ConfigureAwait(true);

            Enabled = true;
            UseWaitCursor = false;

            HashSet<DeployLog> targets;

            if (Environment.Is64BitOperatingSystem)
                targets = deployLogs.CurrentLogs_x64;
            else
                targets = deployLogs.CurrentLogs_x86;


            targetVersion.Enabled = deployLogs.HasHistory;
            targetVersionLabel.Enabled = deployLogs.HasHistory;

            if (deployLogs.HasHistory)
            {
                var items = targets
                    .OrderByDescending(log => log.TimeStamp)
                    .Cast<object>()
                    .ToArray();

                targetVersion.Items.AddRange(items);
            }

            // Select the deploy log being targetted.
            DeployLog target = targets
                .Where(log => log.VersionId == targetId)
                .FirstOrDefault();

            if (target != null)
            {
                targetVersion.SelectedItem = target;
                return;
            }

            // If the target isn't valid, fallback to live.
            targetVersion.SelectedItem = latest;
        }

        private void targetVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (targetVersion.SelectedIndex == 0)
            {
                Program.State.TargetVersion = "";
                return;
            }

            var target = targetVersion.SelectedItem as DeployLog;
            Program.State.TargetVersion = target.VersionId;
        }

        private async void selectChannel(string text)
        {
            object existing = null;

            foreach (var item in channelSelect.Items)
            {
                string name = item.ToString();

                if (name.ToLowerInvariant() == text.ToLowerInvariant())
                {
                    existing = item;
                    break;
                }
            }

            if (existing != null)
            {
                var index = channelSelect.Items.IndexOf(existing);
                channelSelect.SelectedIndex = index;
                return;
            }

            var channel = new Channel(text);

            try
            {
                var logs = await PlayerDeployLogs
                    .Get(channel)
                    .ConfigureAwait(true);

                bool valid = Environment.Is64BitOperatingSystem
                    ? logs.CurrentLogs_x64.Any()
                    : logs.CurrentLogs_x86.Any();

                if (valid)
                {
                    var setItem = new Action(() =>
                    {
                        int index = channelSelect.Items.Add(text);
                        channelSelect.SelectedIndex = index;
                    });

                    Program.State.Channel = channel;
                    Program.SaveState();

                    Invoke(setItem);
                    return;
                }
                throw new Exception("No channels to work with!");
            }
            catch
            {
                var reset = new Action(() => channelSelect.SelectedIndex = 0);

                MessageBox.Show
                (
                    $"Channel '{channel}' had no valid data on Roblox's CDN!",
                    "Invalid channel!",

                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                Invoke(reset);
            }
        }

        private void channelSelect_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            selectChannel(channelSelect.Text);
            e.SuppressKeyPress = true;
        }
    }
}
