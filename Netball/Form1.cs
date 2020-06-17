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
            if (li_tournament.Text == "ANZ Premiership")
            {
                this.secondRouter = "/anz_premiership";
            }
            else if (li_tournament.Text == "Super Netball")
            {
                this.secondRouter = "/super_netball";
            }
            this.GetInitialAllData();
        }

        private void Btn_refresh_Click(object sender, EventArgs e)
        {
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
            //this.filePath = string.Empty;

            //using (var fbd = new FolderBrowserDialog())
            //{
            //    DialogResult result = fbd.ShowDialog();

            //    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            //    {
            //        this.filePath = fbd.SelectedPath;
            //        this.GetTeamInfo();
            //    }
            //}

            this.GetTeamInfo();

        }

        private async Task GetTeamInfo()
        {
            Console.WriteLine("--------team info------------");
            Console.WriteLine(this.selectedId);
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
