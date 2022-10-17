using SSDB.Shared.Settings;
using System.Threading.Tasks;
using SSDB.Shared.Wrapper;

namespace SSDB.Shared.Managers
{
    public interface IPreferenceManager
    {
        Task SetPreference(IPreference preference);

        Task<IPreference> GetPreference();

        Task<IResult> ChangeLanguageAsync(string languageCode);
    }
}