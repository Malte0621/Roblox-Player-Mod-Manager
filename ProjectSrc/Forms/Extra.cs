using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Microsoft.Build.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MessageBox = System.Windows.Forms.MessageBox;
using SystemColors = System.Drawing.SystemColors;

namespace RobloxPlayerModManager.Forms
{
    public partial class Extra : Form
    {
        private Dictionary<string, FVariable> changedFflagsList;

        private const string apiEndpoint = "https://games.roblox.com/v1/games/";
        private const int serverLimit = 100;
        private string placeId = new Launcher(Program.appArguments).placeId;

        private List<Dictionary<string, object>> serverData;

        public Extra()
        {
            InitializeComponent();
            if (placeId != null) {
                placeIdTextBox.Text = placeId;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string placeId = placeIdTextBox.Text;
            string settingsPath = Program.RootDir + "serverList.json";

            try
            {
                DownloadSettingsFile(placeId, settingsPath);
                serverData = ReadJsonFile(settingsPath);
            }
            catch
            {
                MessageBox.Show("An error occured while trying to get the server list.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Use the 'serverData' list of dictionaries as needed
            if (serverData.Count > 0)
            {
                dataGridView1.Rows.Clear();
                foreach (Dictionary<string, object> server in serverData)
                {
                    string serverId = server["id"].ToString();
                    int maxPlayers = Convert.ToInt32(server["maxPlayers"], CultureInfo.InvariantCulture);
                    List<string> playerTokens = ((JArray)server["playerTokens"]).ToObject<List<string>>();
                    int currentPlayers = Convert.ToInt32(server["playing"], CultureInfo.InvariantCulture);
                    int ping = Convert.ToInt32(server["ping"], CultureInfo.InvariantCulture);

                    // Example output to demonstrate accessing the values for each server
                    //MessageBox.Show($"Server ID: {serverId}\nMax Players: {maxPlayers}\nCurrent Players: {currentPlayers}\nFirst Player Token: {(playerTokens.Count > 0 ? playerTokens[0] : "N / A")}");
                    dataGridView1.Rows.Add(serverId, ping, $"{currentPlayers}/{maxPlayers}","Set Server");
                }
            }
            else
            {
                MessageBox.Show("No server data found.");
            }
        }

        private static void DownloadSettingsFile(string placeId, string settingsPath)
        {
            string url = $"{apiEndpoint}{placeId}/servers/0?sortOrder=2&excludeFullGames=false&limit={serverLimit}";

            using (var client = new WebClient())
            {
                try
                {
                    client.DownloadFile(url, settingsPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "An error has occured!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return;
                }
            }
        }

        private static List<Dictionary<string, object>> ReadJsonFile(string filePath)
        {
            string jsonContent = File.ReadAllText(filePath);
            JObject data = JObject.Parse(jsonContent);

            if (data.TryGetValue("data", out var dataArray))
            {
                JArray servers = (JArray)dataArray;
                return servers.ToObject<List<Dictionary<string, object>>>();
            }

            return new List<Dictionary<string, object>>();
        }

        // #### Extra options methods #### //

        private void AddFVariable(string type, string name, string value)
        {
            if (value == null)
            {
                value = 0.ToString(CultureInfo.InvariantCulture);
            }

            FVariable variable = new FVariable(type, value, true)
            {
                Name = name
            };

            if (changedFflagsList == null)
            {
                changedFflagsList = new Dictionary<string, FVariable>();
            }

            if (changedFflagsList.ContainsKey(variable.Key))
            {
                // Update existing entry
                changedFflagsList[variable.Key].Value = variable.Value;
            }
            else
            {
                // Add new entry
                try
                {
                    changedFflagsList.Add(variable.Key, variable);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An exception occured while trying to add the FFlag {name} of type {type} with the value {value} \n" + ex.Message, "An Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void extraSettingsPage_Load(object sender, EventArgs e)
        {
            if (Program.State.extraMaxFPS != null)
            {
                fpsNumberTextBox.Text = Program.State.extraMaxFPS;
            }

            if (Program.State.extraSelectedRendererIndex != null)
            {
                graphicsApisComboBox.SelectedIndex = Program.State.extraSelectedRendererIndex;
            }

            if (Program.State.extraDPIDisabled != null)
            {
                disableDpiCheckBox.Checked = Program.State.extraDPIDisabled;
            }
        }

        private void ApplyButton_Click(object sender, EventArgs e)
        {
            using (FlagEditor flagEditor = new FlagEditor())
            {
                flagEditor.InitializeEditor();

                //go through the dictionary and apply the values
                if (changedFflagsList != null)
                {
                    foreach (var kvp in changedFflagsList)
                    {
                        flagEditor.addFlagOverride(kvp.Value, false);

                        Program.State.extraMaxFPS = fpsNumberTextBox.Text;
                        Program.State.extraSelectedRendererIndex = graphicsApisComboBox.SelectedIndex;
                        Program.State.extraDPIDisabled = disableDpiCheckBox.Checked;

                        Program.SaveState();
                    }
                }
            }

            try
            {
                ApplyButton.Text = $"Applied {changedFflagsList.Count} FFlags!";
                ApplyButton.Enabled = false;
                ApplyButton.Refresh();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("No options were changed, no FFLags were applied", "No option changed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //reset the dictionary
            changedFflagsList = null;

            Task.Delay(500).ContinueWith(_ =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    ApplyButton.Text = "Apply";
                    ApplyButton.ForeColor = SystemColors.ControlText;
                    ApplyButton.Enabled = true;
                    ApplyButton.Refresh();
                });
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void fpsNumberTextBox_TextChanged(object sender, EventArgs e)
        {
            AddFVariable("DFInt", "TaskSchedulerTargetFps", fpsNumberTextBox.Text);
        }

        private void disableDpiCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (disableDpiCheckBox.Checked)
            {
                AddFVariable("DFFlag", "DisableDPIScale", "True");
            }
            else
            {
                AddFVariable("DFFlag", "DisableDPIScale", "False");
            }
        }

        private void graphicsApisComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (graphicsApisComboBox.SelectedIndex)
            {
                case 0:
                    AddFVariable("FFlag", "DebugGraphicsPreferOpenGL", "False");
                    AddFVariable("FFlag", "DebugGraphicsPreferVulkan", "False");
                    AddFVariable("FFlag", "DebugGraphicsPreferPreferD3D11FL10", "False");
                    AddFVariable("FFlag", "DebugGraphicsPreferD3D11", "True");
                    break;

                case 1:
                    AddFVariable("FFlag", "DebugGraphicsPreferOpenGL", "False");
                    AddFVariable("FFlag", "DebugGraphicsPreferVulkan", "False");
                    AddFVariable("FFlag", "DebugGraphicsPreferPreferD3D11FL10", "True");
                    AddFVariable("FFlag", "DebugGraphicsPreferD3D11", "False");
                    break;

                case 2:
                    AddFVariable("FFlag", "DebugGraphicsPreferOpenGL", "True");
                    AddFVariable("FFlag", "DebugGraphicsPreferVulkan", "False");
                    AddFVariable("FFlag", "DebugGraphicsPreferPreferD3D11FL10", "False");
                    AddFVariable("FFlag", "DebugGraphicsPreferD3D11", "False");
                    break;

                case 3:
                    AddFVariable("FFlag", "DebugGraphicsPreferOpenGL", "False");
                    AddFVariable("FFlag", "DebugGraphicsPreferVulkan", "True");
                    AddFVariable("FFlag", "DebugGraphicsPreferPreferD3D11FL10", "False");
                    AddFVariable("FFlag", "DebugGraphicsPreferD3D11", "False");
                    break;
            }
        }
    }
}
