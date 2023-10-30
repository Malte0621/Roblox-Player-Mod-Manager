using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RobloxPlayerModManager.Forms
{
    public partial class Extra : Form
    {
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
                    int maxPlayers = Convert.ToInt32(server["maxPlayers"]);
                    List<string> playerTokens = ((JArray)server["playerTokens"]).ToObject<List<string>>();
                    int currentPlayers = Convert.ToInt32(server["playing"]);
                    int ping = Convert.ToInt32(server["ping"]);

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

        private void DownloadSettingsFile(string placeId, string settingsPath)
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

        private List<Dictionary<string, object>> ReadJsonFile(string filePath)
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
    }
}
