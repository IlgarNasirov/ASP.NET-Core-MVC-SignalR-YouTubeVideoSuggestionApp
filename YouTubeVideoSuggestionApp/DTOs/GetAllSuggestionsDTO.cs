namespace YouTubeVideoSuggestionApp.DTOs
{
    public class GetAllSuggestionsDTO
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string? ShortDescription { get; set; }
        public string YouTubeUrl { get; set; } = null!;
    }
}
