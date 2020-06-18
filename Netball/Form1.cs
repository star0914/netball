using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Netball
{
    public partial class Netball : Form
    {
        private bool isReady = false;
        private bool is_checked = false; //TODO all checked

        private string seasonVal = "";
        private string competitionVal = "";
        private string roundVal = "";

        public string base_url = "https://mc.championdata.com";
        public string secondRouter = "/anz_premiership";
        private JArray avaliableIDs = new JArray();
        private List<dynamic> AvaliableGameInfo = new List<dynamic>() { };

        private string[] li_tournamentLists = new string[] { "ANZ Premiership", "Super Netball" };

        private string selectedId = "";
        private string filePath = string.Empty;

        private Dictionary<string, object> teamInfo = new Dictionary<string, object>();
        private string[] TitleRow = { "Season", "Competition", "Round", "Date", "Team", "Opposition", "POS", "Name", "MIN", "QP", "G", "A", "GS%", "GA", "F", "FWA", "GN", "I", "IPT", "DEFG", "DEFN", "R", "CR", "PU", "C", "O", "GPT", "BP", "BH", "OFF", "CPB", "1ST", "2ND", "3RD", "4TH", "OT", "TOTAL", "Goals from Centre Pass", "Centre Pass to Goal Percentage", "Goals from Gain", "Gain to Goal Percentage", "Goals from Turnovers", "Turnover to Goal Percentage", "Missed Shot Conversion" };
        private List<string[]> scrapedData = new List<string[]>() { };
        private dynamic TeamData = new Object();

        private string fileName = "ANZ Premiership";

        public Netball()
        {
            InitializeComponent();
        }

        private void Netball_Load(object sender, EventArgs e)
        {
            this.DisableCount();
            this.DisableFormReady();
            this.initTournament();
        }

        private void initTournament()
        {
            li_tournament.DataSource = this.li_tournamentLists;
        }

        private async Task GetInitialAllData()
        {
            await this.GETSeasonData();
            if (this.isReady) this.EnableReady();
        }

        private async Task GETSeasonData()
        {
            await this.GETAvaliableIDS();
            await this.GETAvaliableGameInfo();
            this.SETSeasonData();
        }

        private async Task GETAvaliableIDS()
        {
            dynamic appData = new object();
            dynamic CompetitionsInfo = new object();
            this.avaliableIDs = new JArray();

            string api = this.base_url + secondRouter + "/settings/application_settings.json";
            await Task.Run(() => { appData = JsonConvert.DeserializeObject<dynamic>(GetAllData(api)); });
            if (appData != null)
            {
                CompetitionsInfo = appData.competitionList.competition;
                foreach (var item in CompetitionsInfo)
                {
                    this.avaliableIDs.Add(item.id);
                }
            }
            else return;
        }

        private async Task GETAvaliableGameInfo()
        {
            this.AvaliableGameInfo = new List<dynamic>() { };
            dynamic competitionLists = new object();
            string api = this.base_url + "/data/competitions.json";

            await Task.Run(() => { competitionLists = JsonConvert.DeserializeObject<dynamic>(GetAllData(api)); });
            if (competitionLists != null)
            {
                dynamic compatitionInfo = competitionLists.competitionDetails.competition;
                foreach (var item in compatitionInfo)
                {
                    if (this.avaliableIDs.ToList().IndexOf(item.id) > -1)
                    {
                        this.AvaliableGameInfo.Add(item);
                    }
                }
            }
            else return;
        }

        private void SETSeasonData()
        {
            List<string> seasons = new List<string>() { };
            if (this.AvaliableGameInfo != null)
            {
                foreach (var item in this.AvaliableGameInfo)
                {
                    string season_name = item.season;

                    if (seasons.FindIndex(s => s.Equals(season_name)) == -1)
                    {
                        seasons.Add(season_name);
                    }
                }
                li_season.DataSource = seasons;
            }
        }

        private void GETCompetitionData()
        {
            List<string> competitions = new List<string>() { };
            if (this.AvaliableGameInfo != null)
            {
                foreach (var item in this.AvaliableGameInfo)
                {
                    string competition_name = item.name;

                    if (item.season == this.seasonVal)
                    {
                        competitions.Add(competition_name);
                    }
                }
                li_competition.DataSource = competitions;
            }
        }

        private void GETRoundData()
        {
            if (this.AvaliableGameInfo != null)
            {
                var roundCount = 0;
                foreach (var item in this.AvaliableGameInfo)
                {
                    if (item.season == this.seasonVal && item.name == this.competitionVal)
                    {
                        roundCount = item.rounds;
                        this.selectedId = item.id;
                    }
                }

                if (roundCount > 0 )
                {
                    List<string> roundArray = new List<string>() { };
                    for (int i = 0; i < roundCount; i++)
                    {
                        roundArray.Add((i + 1).ToString());
                    }
                    li_round.DataSource = roundArray;
                }
            }
        }

        private string GetAllData(string Url)
        {
            string result = "";
            try
            {
                // Create a request for the URL.
                WebRequest request = WebRequest.Create(Url);
                // If required by the server, set the credentials.
                request.Credentials = CredentialCache.DefaultCredentials;

                // Get the response.
                WebResponse response = request.GetResponse();
                // Display the status.
                var data = ((HttpWebResponse)response).GetResponseStream();
                StreamReader sr = new StreamReader(response.GetResponseStream());
                result = sr.ReadToEnd();
                sr.Close();
                this.isReady = true;
            }
            catch (WebException webExcp)
            {
                // If you reach this point, an exception has been caught.  
                Console.WriteLine("A WebException has been caught.");
                // Write out the WebException message.  
                Console.WriteLine(webExcp.ToString());
                this.ErrorMsg("Error: A WebException has been caught.");
                // Get the WebException status code.  
                WebExceptionStatus status = webExcp.Status;
                System.Windows.Forms.MessageBox.Show(status + ". Please press refresh icon!", "Message");
                // If status is WebExceptionStatus.ProtocolError,
                //   there has been a protocol error and a WebResponse
                //   should exist. Display the protocol error.  
                if (status == WebExceptionStatus.ProtocolError)
                {
                    Console.Write("The server returned protocol error ");
                    this.ErrorMsg("Error: The server returned protocol error.");
                    System.Windows.Forms.MessageBox.Show("The server returned protocol error. Please press refresh icon!", "Message");
                    // Get HttpWebResponse so that you can check the HTTP status code.  
                    HttpWebResponse httpResponse = (HttpWebResponse)webExcp.Response;
                    Console.WriteLine((int)httpResponse.StatusCode + " - "
                       + httpResponse.StatusCode);
                }
            }
            catch (Exception e)
            {
                // Code to catch other exceptions goes here.  
            }
            return result;
        }

        private void ErrorMsg(string errorMsg)
        {
            btnFetchResult.Enabled = false;
            this.isReady = false;
        }

        private void EnableReady()
        {
            btnFetchResult.Enabled = true;
        }

        private void Li_tournament_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.DisableCount();
            this.DisableFormReady();
            this.fileName = "";
            if (li_tournament.Text == "ANZ Premiership")
            {
                this.secondRouter = "/anz_premiership";
                this.fileName = "ANZ_Premiership";
            }
            else if (li_tournament.Text == "Super Netball")
            {
                this.secondRouter = "/super_netball";
                this.fileName = "Super_Netball";
            }
            this.GetInitialAllData();
        }

        private void Btn_refresh_Click(object sender, EventArgs e)
        {
            li_tournament.Enabled = true;
            this.DisableFormReady();
            Console.WriteLine("--- reload ---");
            this.DisableCount();
            this.GetInitialAllData();
        }

        private void Li_season_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.seasonVal = li_season.Text;
            this.GETCompetitionData();
        }

        private void Li_competition_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.competitionVal = li_competition.Text;
            this.GETRoundData();
        }

        private void Li_round_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.roundVal = li_round.Text;
            this.EnableFormReady();
        }

        private void BtnFetchResult_Click(object sender, EventArgs e)
        {
            this.filePath = string.Empty;

            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    this.filePath = fbd.SelectedPath;
                    this.GetTeamInfo();
                }
            }
        }

        private async Task GetTeamInfo()
        {
            this.DisableFormReady();
            this.DisplayCount();
            li_tournament.Enabled = false;

            this.scrapedData = new List<string[]>() { };
            this.scrapedData.Add(this.TitleRow);

            dynamic PlayDetail = new object();
            dynamic matchInfo = new object();

            string api = this.base_url + "/data/" + this.selectedId + "/fixture.json";
            await Task.Run(() => { PlayDetail = JsonConvert.DeserializeObject<dynamic>(GetAllData(api)); });
            if (PlayDetail != null)
            {
                matchInfo = PlayDetail.fixture.match;

                int index = 0;

                List<dynamic> Infos = new List<dynamic>(matchInfo);
                List<dynamic> matchInfos = Infos.Where(t => (string)t["roundNumber"] == this.roundVal).ToList();

                string total_count = (matchInfos.Count).ToString();
                la_total.Text = total_count;

                foreach (var item in matchInfos)
                {
                    la_cu_state.Text = (index + 1).ToString();

                    string MatchID = "";
                    MatchID = item.matchId;
                    DateTime LocalStartTime = Convert.ToDateTime(item.localStartTime);
                    string homeSquadName = item.homeSquadName;
                    string awaySquadName = item.awaySquadName;

                    if (MatchID != null)
                    {
                        await this.GetPlayersInfo(MatchID, LocalStartTime, homeSquadName, awaySquadName);
                    }

                    index++;
                }
            }

            Console.WriteLine("--------generating Data----------");
            this.GenerateCSV(this.scrapedData);
        }

        private void GenerateCSV(List<string[]> output)
        {
            DateTime now = DateTime.Now;
            var nowDay = now.Day + "_" + now.Month + "_" + now.Year + "_" + now.Hour + "_" + now.Minute + "_" + now.Second;
            string generatePath = this.filePath + @"\" + this.fileName + "_" + nowDay + ".csv";
            string delimiter = ",";

            int length = output.ToArray().GetLength(0);
            StringBuilder sb = new StringBuilder();
            for (int index = 0; index < length; index++)
            {
                sb.AppendLine(string.Join(delimiter, output[index]));
            }
            File.WriteAllText(generatePath, sb.ToString());
            System.Windows.Forms.MessageBox.Show("CSV file creation success!", "Message");
            li_tournament.Enabled = true;
            this.EnableFormReady();
            this.DisableCount();
        }

        private async Task GetPlayersInfo(string matchID, DateTime StartTime, string HSName, string ASName)
        {
            int rowAmount = this.TitleRow.Length;
            dynamic PlayDetail = new Object();
            string detailApi = this.base_url + "/data/" + this.selectedId + "/" + matchID + ".json";
            Console.WriteLine(detailApi);

            string homeSquadId = "";
            string awaySquadId = "";

            await Task.Run(() => { PlayDetail = JsonConvert.DeserializeObject<dynamic>(GetAllData(detailApi)); });
            if (PlayDetail != null)
            {
                // homeTeam Data
                homeSquadId = PlayDetail.matchStats.matchInfo.homeSquadId;

                if (PlayDetail.matchStats.playerStats.ToString() != "")
                {
                    List<dynamic> PlayersInfoA = new List<dynamic>(PlayDetail.matchStats.playerStats.player);
                    List<dynamic> SortedTeamAValue = PlayersInfoA.Where(t => (string)t["squadId"] == homeSquadId).OrderByDescending(x => Convert.ToInt32(x.goals)).ToList();

                    int lenA = SortedTeamAValue.Count;
                    for (int i = 0; i < lenA; i++)
                    {
                        string[] rowDataA = this.IndividialPlayerInfo(rowAmount, SortedTeamAValue[i], StartTime, HSName, ASName, PlayDetail, homeSquadId, i);
                        this.scrapedData.Add(rowDataA);
                    }
                }

                // AwayTeam Data
                awaySquadId = PlayDetail.matchStats.matchInfo.awaySquadId;

                if (PlayDetail.matchStats.playerStats.ToString() != "")
                {
                    List<dynamic> PlayersInfoB = new List<dynamic>(PlayDetail.matchStats.playerStats.player);
                    List<dynamic> SortedTeamBValue = PlayersInfoB.Where(t => (string)t["squadId"] == awaySquadId).OrderByDescending(x => Convert.ToInt32(x.goals)).ToList();

                    int lenB = SortedTeamBValue.Count;
                    for (int i = 0; i < lenB; i++)
                    {
                        string[] rowDataB = this.IndividialPlayerInfo(rowAmount, SortedTeamBValue[i], StartTime, ASName, HSName, PlayDetail, awaySquadId, i);
                        this.scrapedData.Add(rowDataB);
                    }
                }
              
            }
        }

        private string[] IndividialPlayerInfo(int rowLen, dynamic Player, DateTime startTime, string TeamName, string Opposition, dynamic AllDetail, string SquadId, int index)
        {
            string playerID = Player.playerId;
            List<dynamic> playerDetails = new List<dynamic>(AllDetail.matchStats.playerInfo.player);
            List<dynamic> playerInfo = playerDetails.Where(t => (string)t["playerId"] == playerID).ToList();

            List<dynamic> teamPeriodStats = new List<dynamic>(AllDetail.matchStats.teamPeriodStats.team);
            List<dynamic> matchedPeriodState = teamPeriodStats.Where(t => (string)t["squadId"] == SquadId).ToList();

            List<dynamic> teamStats = new List<dynamic>(AllDetail.matchStats.teamStats.team);
            List<dynamic> matchedteamStats = teamStats.Where(t => (string)t["squadId"] == SquadId).ToList();

            double goal_per = 0;

            long goalValue = Player.goals.Value;
            long goalAttemptsValue = Player.goalAttempts.Value;

            goal_per = Math.Round(goalValue * 1000.0 / goalAttemptsValue) / 10;
            if (Double.IsNaN(goal_per)) goal_per = 0;

            string[] newRow = new string[rowLen];

            newRow[0] = this.seasonVal;
            newRow[1] = this.competitionVal;
            newRow[2] = this.roundVal;
            newRow[3] = startTime.ToString("dddd. MMMM dd. yyyy");
            newRow[4] = TeamName;
            newRow[5] = Opposition;
            newRow[6] = Player.startingPositionCode;
            newRow[7] = playerInfo[0].firstname + " " + playerInfo[0].surname;
            newRow[8] = Player.minutesPlayed;
            newRow[9] = Player.quartersPlayed;
            newRow[10] = Player.goals + " | " + Player.goalAttempts;
            newRow[11] = Player.goalAttempts;
            newRow[12] = goal_per.ToString();
            newRow[13] = Player.goalAssists;
            newRow[14] = Player.feeds;
            newRow[15] = Player.feedWithAttempt;
            newRow[16] = Player.gain;
            newRow[17] = Player.intercepts;
            newRow[18] = Player.interceptPassThrown;
            newRow[19] = Player.deflectionWithGain; //DEFG
            newRow[20] = Player.deflectionWithNoGain;
            newRow[21] = Player.rebounds;
            newRow[22] = Player.centrePassReceives;
            newRow[23] = Player.pickups;
            newRow[24] = Player.contactPenalties;
            newRow[25] = Player.obstructionPenalties;
            newRow[26] = Player.generalPlayTurnovers;
            newRow[27] = Player.badPasses;
            newRow[28] = Player.badHands;
            newRow[29] = Player.offsides;
            newRow[30] = Player.centrePassReceives; // CPB

            if (index == 0)
            {
                newRow[31] = matchedPeriodState[0].goals;
                newRow[32] = matchedPeriodState[1].goals;
                newRow[33] = matchedPeriodState[2].goals;
                newRow[34] = matchedPeriodState[3].goals;
                newRow[35] = ""; // OT
                newRow[36] = matchedteamStats[0].goals;

                newRow[37] = matchedteamStats[0].goalsFromCentrePass;
                newRow[38] = matchedteamStats[0].centrePassToGoalPerc;
                newRow[39] = matchedteamStats[0].goalsFromGain;
                newRow[40] = matchedteamStats[0].gainToGoalPerc;
                newRow[41] = matchedteamStats[0].goalsFromTurnovers;
                newRow[42] = matchedteamStats[0].turnoverToGoalPerc;
                newRow[43] = matchedteamStats[0].missedShotConversion;

            } else
            {
                newRow[31] = "";
                newRow[32] = "";
                newRow[33] = "";
                newRow[34] = "";
                newRow[35] = "";
                newRow[36] = "";
                newRow[37] = "";
                newRow[38] = "";
                newRow[39] = "";
                newRow[40] = "";
                newRow[41] = "";
                newRow[42] = "";
                newRow[43] = "";
            }

            return newRow;
        }

        private void DisableCount()
        {
            la_total.Text = "";
            la_slash.Text = "";
            la_cu_state.Text = "";
            la_team.Text = "";
        }

        private void DisplayCount()
        {
            la_total.Text = "0";
            la_slash.Text = "/";
            la_cu_state.Text = "0";
            la_team.Text = "teams";
        }

        private void EnableFormReady()
        {
            li_season.Enabled = true;
            li_competition.Enabled = true;
            li_round.Enabled = true;
            btnFetchResult.Enabled = true;
        }

        private void DisableFormReady()
        {
            li_season.Enabled = false;
            li_competition.Enabled = false;
            li_round.Enabled = false;
            btnFetchResult.Enabled = false;
        }

    }
}
