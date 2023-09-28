using YouTubeVideoSuggestionApp.DTOs;

namespace YouTubeVideoSuggestionApp.IRepositories
{
    public interface ISuggestionRepository
    {
        public Task AddSuggestion(AddSuggestionDTO addSuggestionDTO);
        public Task<CustomReturnDTO> UpdateSuggestion(int id, AddSuggestionDTO addSuggestionDTO);
        public Task<CustomReturnDTO> DeleteSuggestion(int id, DeleteSuggestionDTO deleteSuggestionDTO);
        public IQueryable<GetAllSuggestionsDTO> GetAllSuggestions();
        public Task<AddSuggestionDTO?> GetSuggestion(int id);
    }
}
