using AspMVCCoreGit.Models;

namespace AspMVCCoreGit.Repository
{
    public interface ILanguageRepository
    {
        Task<List<LanguageModel>> GetLanguages();
    }
}