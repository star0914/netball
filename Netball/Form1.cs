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

        public string base_url = "https://mc.championdata.com";
        private string competitionID = string.Empty;

        private dynamic CompetitionsInfo;

        private string[] li_tournamentLists = new string[] { "ANZ Premiership", "Super Netball" };
        private dynamic appData;

        private JArray initialFormData = new JArray();

        public Netball()
        {
            InitializeComponent();
        }

        private void Netball_Load(object sender, EventArgs e)
        {
            this.initTournament();
            this.GetInitialAllData();
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
            string api = this.base_url + "/anz_premiership/settings/application_settings.json";
            await Task.Run(() => { this.appData = JsonConvert.DeserializeObject<dynamic>(GetAllData(api)); });
            if (this.appData != null)
            {
                this.CompetitionsInfo = this.appData.competitionList.competition;
                List<string> seasons = new List<string>() { };

                this.initialFormData = new JArray();

                JObject Job = new JObject(
                    new JProperty("season", ""),
                    new JProperty("ids"),
                    new JProperty("competitions")
                );
                JArray Ids = (JArray)Job["ids"];
                JArray Competitions = (JArray)Job["competitions"];

                foreach (var item in this.CompetitionsInfo)
                {
                    string competition_name = item.competition_name;
                    string season_name = competition_name.Split(' ')[0];

                    if (seasons.FindIndex(s => s.Equals(season_name)) == -1)
                    {
                        if (!Job["season"].Equals(""))
                        {
                            this.initialFormData.Add(Job);
                            Ids.Clear();
                            Competitions.Clear();
                        }
                        seasons.Add(season_name);
                        Job["season"] = season_name;
                        Ids.Add(item.id);
                        Competitions.Add(item.competition_name);
                    } else
                    {
                        Ids.Add(item.id);
                        Competitions.Add(item.competition_name);
                    }
                }

                li_season.DataSource = seasons;
                li_season.SelectedIndex = seasons.Count - 1;
            }
            else return;
        }

        private void GETCompetitionData()
        {
            foreach (var item in this.initialFormData)
            {
                if (item["season"].ToString() == li_season.Text)
                {
                    li_competition.DataSource = item["competitions"];
                }
            }
        }

        private async Task GETRoundData() {
            dynamic competitionLists = new object();
            string api = this.base_url + "/data/competitions.json";
            await Task.Run(() => { competitionLists = JsonConvert.DeserializeObject<dynamic>(GetAllData(api)); });
            if (competitionLists != null)
            {
                var roundCount = 0;
                dynamic compatitionInfo = competitionLists.competitionDetails.competition;
                foreach (var item in compatitionInfo)
                {
                    Console.WriteLine(item.name);
                    if (item.name == li_competition.Text)
                    {
                         roundCount = item.rounds;
                    }
                }

                Console.WriteLine(roundCount);

                List<string> roundArray = new List<string>() { };
                for (int i = 0; i < roundCount; i++)
                {
                    roundArray.Add((i+1).ToString());
                }

                li_round.DataSource = roundArray;
            }
            else return;
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

        }

        private void BtnFetchResult_Click(object sender, EventArgs e)
        {

        }

        private void Btn_refresh_Click(object sender, EventArgs e)
        {
            this.initTournament();
            this.GetInitialAllData();
            Console.WriteLine("---  refresh  ---");

        }

        private void Li_season_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GETCompetitionData();
        }

        private void Li_competition_SelectedIndexChanged(object sender, EventArgs e)
        {
             this.GETRoundData();
        }

        private void Li_round_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
