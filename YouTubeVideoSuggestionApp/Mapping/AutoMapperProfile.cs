using AutoMapper;
using YouTubeVideoSuggestionApp.DTOs;
using YouTubeVideoSuggestionApp.Models;

namespace YouTubeVideoSuggestionApp.Mapping
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AddSuggestionDTO, Suggestion>();
            CreateMap<Suggestion, AddSuggestionDTO>();
            CreateMap<Suggestion, GetAllSuggestionsDTO>();
        }
    }
}
