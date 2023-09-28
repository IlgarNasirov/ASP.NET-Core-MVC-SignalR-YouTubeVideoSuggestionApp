using AutoMapper;
using Microsoft.EntityFrameworkCore;
using YouTubeVideoSuggestionApp.DTOs;
using YouTubeVideoSuggestionApp.IRepositories;
using YouTubeVideoSuggestionApp.Models;

namespace YouTubeVideoSuggestionApp.Repositories
{
    public class SuggestionRepository:ISuggestionRepository
    {
        private readonly YouTubeVideoSuggestionDbContext _db;
        private readonly IMapper _mapper;
        public SuggestionRepository(YouTubeVideoSuggestionDbContext db, IMapper mapper)
        {
            _db = db;            
            _mapper = mapper;
        }
        public async Task AddSuggestion(AddSuggestionDTO addSuggestionDTO)
        {
            addSuggestionDTO.Password = BCrypt.Net.BCrypt.HashPassword(addSuggestionDTO.Password);
            await _db.Suggestions.AddAsync(_mapper.Map<Suggestion>(addSuggestionDTO));
            await _db.SaveChangesAsync();
        }

        public async Task<CustomReturnDTO>UpdateSuggestion(int id, AddSuggestionDTO addSuggestionDTO)
        {
            var suggestion = await _db.Suggestions.Where(s => s.Id == id && s.Status == true).FirstOrDefaultAsync();
            if(suggestion!=null)
            {
                if(BCrypt.Net.BCrypt.Verify(addSuggestionDTO.Password, suggestion.Password))
                {
                    suggestion.YouTubeUrl = addSuggestionDTO.YouTubeUrl;
                    suggestion.Username = addSuggestionDTO.Username;
                    suggestion.ShortDescription = addSuggestionDTO.ShortDescription;
                    suggestion.LastModified = DateTime.Now;
                    await _db.SaveChangesAsync();
                    return new CustomReturnDTO { Type=true, Message="The suggestion updated successfully!"};
                }
                return new CustomReturnDTO { Type = false, Message = "Please enter the correct password." };
            }
            return new CustomReturnDTO { Type=false};
        }

        public async Task<CustomReturnDTO>DeleteSuggestion(int id, DeleteSuggestionDTO deleteSuggestionDTO)
        {
            var suggestion = await _db.Suggestions.Where(s => s.Id == id && s.Status == true).FirstOrDefaultAsync();
            if (suggestion != null)
            {
                if (BCrypt.Net.BCrypt.Verify(deleteSuggestionDTO.Password, suggestion.Password))
                {
                    suggestion.Status=false;
                    await _db.SaveChangesAsync();
                    return new CustomReturnDTO { Type = true, Message = "The suggestion deleted successfully!" };
                }
                return new CustomReturnDTO { Type = false, Message = "Please enter the correct password." };
            }
            return new CustomReturnDTO { Type = false};
        }

        public IQueryable<GetAllSuggestionsDTO> GetAllSuggestions()
        {
            return _db.Suggestions.Where(s => s.Status == true).OrderByDescending(s => s.LastModified).Select(s => _mapper.Map<GetAllSuggestionsDTO>(s));
        }

        public async Task<AddSuggestionDTO?>GetSuggestion(int id)
        {
            var suggestion = await _db.Suggestions.Where(s => s.Id == id && s.Status == true).FirstOrDefaultAsync();
            if (suggestion == null)
                return null;
            suggestion.Password = string.Empty;
            return _mapper.Map<AddSuggestionDTO>(suggestion);
        }
    }
}
