﻿using System;
using System.Net.Http.Json;

namespace GuernseyPool.Model
{

    public class ScoreCard
    {

        public string League { get; set; }
        public Boolean IsWinterLeague { get; set; }
        public DateTime Date { get; set; }
        public string Venue { get; set; }
        public Team Home { get; set; } = new Team();
        public Team Away { get; set; } = new Team();
        public List<Frame> Frames { get; set; } = new List<Frame>();
        public string Comments { get; set; }
        public int HomeWins { get; set; }
        public int AwayWins { get; set; }

        public DateTime? Submitted { get; set; }

        public ScoreCard(Boolean IsWinterLeague){
            this.IsWinterLeague = IsWinterLeague;
        }

        public async Task<bool> Submit()
        {
            this.Submitted = DateTime.Now;
            var Data = JsonContent.Create(this);
            var Client = new HttpClient();
            var Response = await Client.PostAsync("https://leaguenights.click/GuernseyPool/Submit", Data);
            if (Response.IsSuccessStatusCode)
            {
                // TODO: Save Locally
                return true;
            }
            else
            {
                this.Submitted = null;
                return false;
            }
        }

    }

    public class Team
    {
        public string Name { get; set; }
        public List<Player> Players { get; set; }
    }

    public class Player
    {
        public int Index { get; set; }
        public string Name { get; set; }
    }

    public class Frame
    {
        public int HomePlayerIndex { get; set; }
        public int AwayPlayerIndex { get; set; }
        public bool IsHomeWin { get; set; }
    }

}