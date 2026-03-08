namespace AnimeVerse.Models
{
    public class Anime
    {
        public int MalId { get; set; }
        public string Title { get; set; } = string.Empty;
        public double Score { get; set; }
        public int Year { get; set; }
        public string Synopsis { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        
        public List<string> Genres { get; set; } = new();
    }
}