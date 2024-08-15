using ContactFieldMapping.BAL.ViewModel;
using ContactFieldMapping.DAL.Model;

namespace ContactFieldMapping.BAL.IService
{
    public interface IContactService
    {
        Task<IEnumerable<Contact>> ListContactsAsync();
        Task<bool> LoadDataAsync(MappingViewModel viewModel);
    }
}