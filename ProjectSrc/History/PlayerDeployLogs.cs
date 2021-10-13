using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RobloxDeployHistory
{
    public class PlayerDeployLogs
    {
        private const string LogPattern = "New (WindowsPlayer?) (version-[a-f\\d]+) at (\\d+/\\d+/\\d+ \\d+:\\d+:\\d+ [A,P]M), file version: (\\d+), (\\d+), (\\d+), (\\d+)";
        private const StringComparison StringFormat = StringComparison.InvariantCulture;
        private static readonly NumberFormatInfo NumberFormat = NumberFormatInfo.InvariantInfo;

        public string Branch { get; private set; }

        private string LastDeployHistory = "";
        private static readonly Dictionary<string, PlayerDeployLogs> LogCache = new Dictionary<string, PlayerDeployLogs>();

        public HashSet<DeployLog> CurrentLogs_x86 { get; private set; } = new HashSet<DeployLog>();

        public HashSet<DeployLog> CurrentLogs_x64 { get; private set; } = new HashSet<DeployLog>();

        private PlayerDeployLogs(string branch)
        {
            Branch = branch;
            LogCache[branch] = this;
        }

        private static void MakeDistinct(HashSet<DeployLog> targetSet)
        {
            var byChangelist = new Dictionary<int, DeployLog>();
            var rejected = new List<DeployLog>();

            foreach (DeployLog log in targetSet)
            {
                int changelist = log.Changelist;

                if (byChangelist.ContainsKey(changelist))
                {
                    DeployLog oldLog = byChangelist[changelist];

                    if (oldLog.TimeStamp.CompareTo(log.TimeStamp) < 0)
                    {
                        byChangelist[changelist] = log;
                        rejected.Add(oldLog);
                    }
                }
                else
                {
                    byChangelist.Add(changelist, log);
                }
            }

            rejected.ForEach(log => targetSet.Remove(log));
        }

        private void UpdateLogs(string deployHistory)
        {
            var now = DateTime.Now;
            var matches = Regex.Matches(deployHistory, LogPattern);

            CurrentLogs_x86.Clear();
            CurrentLogs_x64.Clear();

            foreach (Match match in matches)
            {
                string[] data = match.Groups.Cast<Group>()
                    .Select(group => group.Value)
                    .Where(value => value.Length != 0)
                    .ToArray();

                string buildType = data[1];
                bool is64Bit = buildType.EndsWith("64", StringFormat);

                var deployLog = new DeployLog()
                {
                    Is64Bit = is64Bit,
                    VersionGuid = data[2],
                    TimeStamp = DateTime.Parse(data[3], DateTimeFormatInfo.InvariantInfo),

                    MajorRev = int.Parse(data[4], NumberFormat),
                    Version = int.Parse(data[5], NumberFormat),
                    Patch = int.Parse(data[6], NumberFormat),
                    Changelist = int.Parse(data[7], NumberFormat)
                };

                // olive71 (Ganesh) said we should expect builds older than ~3 months to be deleted.
                // Although in practice this isn't consistently done, it's better to be safe than sorry.
                // https://devforum.roblox.com/t/previous-roblox-builds-missing-from-deployment-server/469698/3

                var timespan = now - deployLog.TimeStamp;

                if (timespan.TotalDays > 90)
                    continue;

                // Unverified builds might need a moment.

                if (timespan.TotalMinutes < 5)
                    continue;

                HashSet<DeployLog> targetList;

                //if (deployLog.Is64Bit)
                //    targetList = CurrentLogs_x64;
                //else
                //    targetList = CurrentLogs_x86;

                targetList = CurrentLogs_x86; // No 64-bit player yet.

                targetList.Add(deployLog);
            }

            MakeDistinct(CurrentLogs_x64);
            MakeDistinct(CurrentLogs_x86);
        }

        public static async Task<PlayerDeployLogs> Get(string branch)
        {
            PlayerDeployLogs logs;

            if (LogCache.ContainsKey(branch))
                logs = LogCache[branch];
            else
                logs = new PlayerDeployLogs(branch);

            var getDeployHistory = HistoryCache.GetDeployHistory(branch);
            string deployHistory = await getDeployHistory.ConfigureAwait(false);

            if (logs.LastDeployHistory != deployHistory)
            {
                logs.LastDeployHistory = deployHistory;
                logs.UpdateLogs(deployHistory);
            }

            Console.WriteLine(logs);

            return logs;
        }
    }
}
