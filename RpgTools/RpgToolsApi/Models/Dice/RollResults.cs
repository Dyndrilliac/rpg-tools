namespace RpgToolsApi.Models.Dice
{
    public class RollResults
    {
        public int Dice { get; set; }
        public int Sides { get; set; }
        public IEnumerable<int>? Results { get; set; }
        public int Total { get; set; }
    }
}
