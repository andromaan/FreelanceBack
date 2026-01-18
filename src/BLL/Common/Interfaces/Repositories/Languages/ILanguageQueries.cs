using Domain.Models.Languages;

namespace BLL.Common.Interfaces.Repositories.Languages;

public interface ILanguageQueries : IQueries<Language, int>, IUniqueQuery<Language, int>
{
}