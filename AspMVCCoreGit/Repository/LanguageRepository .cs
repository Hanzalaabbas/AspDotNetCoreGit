using AspMVCCoreGit.Data;
using AspMVCCoreGit.Models;
using Microsoft.EntityFrameworkCore;

namespace AspMVCCoreGit.Repository
{
    public class LanguageRepository
    {
        private readonly BookStoreContext? _context = null;

        public LanguageRepository(BookStoreContext context) {
            _context = context;
        }
        public async Task<List<LanguageModel>> GetLanguages()
        {
            var languages = await _context.Languages.AsNoTracking().Select(x => new LanguageModel()
            {
                Id =x.Id,
                Name = x.Name,  
                Description = x.Description,
            }).ToListAsync();
            return languages;
        }
    }
}
