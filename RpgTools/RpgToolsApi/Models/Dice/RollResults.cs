﻿namespace RpgToolsApi.Models.Dice
{
    public class RollResults
    {
        public RollResults(string PlayerName, uint Dice, uint Sides)
        {
            this.PlayerName = PlayerName ?? string.Empty;
            this.Dice = Dice;
            this.Sides = Sides;
            var list = new List<int>();
            var rand = new Random();

            for (uint i = 0; i < Dice; i++)
            {
                list.Add(rand.Next(1, (int)Sides + 1));
            }

            this.Results = list;
            this.Total = (uint)list.Sum();
            this.Timestamp = DateTime.UtcNow;
        }

        public string PlayerName { get; set; }
        public uint Dice { get; set; }
        public uint Sides { get; set; }
        public IEnumerable<int> Results { get; set; }
        public uint Total { get; set; }
        public DateTime Timestamp { get; }
    }
}
