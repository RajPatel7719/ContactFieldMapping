using ContactFieldMapping.BAL.Helper;
using ContactFieldMapping.BAL.IService;
using ContactFieldMapping.BAL.ViewModel;
using ContactFieldMapping.DAL.IRepository;
using ContactFieldMapping.DAL.Model;
using Microsoft.Extensions.Logging;

namespace ContactFieldMapping.BAL.Service;

public class ContactService(IRepository<Contact> contactRepository, ILogger<ContactService> logger) : IContactService
{
    private readonly IRepository<Contact> _contactRepository = contactRepository;
    private readonly ILogger<ContactService> _logger = logger;

    public async Task<bool> LoadDataAsync(MappingViewModel viewModel)
    {
        var apiResponses1 = LoadApiData1();
        var apiResponses2 = LoadApiData2();
        try
        {
            foreach (var response in apiResponses1)
            {
                var contact = ContactHelperMethod.MapAndTransform(response, viewModel);
                if (ContactHelperMethod.ApplyCriteria(contact.JoiningDate, viewModel.CutoffDate) &&
                    (!viewModel.IsCriteriaSelected || ContactHelperMethod.ApplyCustomCriteria(contact, viewModel.CriteriaField, viewModel.CriteriaOperator, viewModel.CriteriaValue)))
                {
                    await _contactRepository.AddAsync(contact);
                }
            }

            foreach (var response in apiResponses2)
            {
                var contact = ContactHelperMethod.MapAndTransform(response, viewModel);
                if (ContactHelperMethod.ApplyCriteria(contact.JoiningDate, viewModel.CutoffDate) &&
                    (!viewModel.IsCriteriaSelected || ContactHelperMethod.ApplyCustomCriteria(contact, viewModel.CriteriaField, viewModel.CriteriaOperator, viewModel.CriteriaValue)))
                {
                    await _contactRepository.AddAsync(contact);
                }
            }

            await _contactRepository.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while loading data.");
            return false;
        }
    }

    public async Task<IEnumerable<Contact>> ListContactsAsync()
    {
        return await _contactRepository.GetAllAsync();
    }

    private static List<ApiResponse1ViewMode> LoadApiData1()
    {
        var apiData1 = new List<ApiResponse1ViewMode>
        {
            new() { FirstName = "Joseph", LastName = "Watt", EmailAddress = "josephw@somedomain.com", Status = "Active", JoiningDate = DateTime.Parse("2024-07-17T19:21:22.37453+00:00"), MemberType = "Developer" },
            new() { FirstName = "Sally", LastName = "Cole", EmailAddress = "sallyc@somedomain.com", Status = "Active", JoiningDate = DateTime.Parse("2024-07-19T19:21:22.37453+00:00"), MemberType = "HR" },
            new() { FirstName = "Kevin", LastName = "Baker", EmailAddress = "kevin@somedomain.com", Status = "Inactive", JoiningDate = DateTime.Parse("2024-07-18T19:21:22.37453+00:00"), MemberType = "Manager" },
            new() { FirstName = "Seth", LastName = "Polasky", EmailAddress = "sethp@somedomain.com", Status = "Active", JoiningDate = DateTime.Parse("2024-07-19T19:21:22.37453+00:00"), MemberType = "Senior Manager" }
        };
        return apiData1;
    }

    private static List<ApiResponse2ViewModel> LoadApiData2()
    {
        var apiData2 = new List<ApiResponse2ViewModel>
        {
            new() { FirstName = "Sally", LastName = "Cole", EmailAddress = "sallyc@somedomain.com", Status = true, JoiningDate = DateTime.Parse("2024-07-18"), MemberType = "Staff", JobTitle = "Staff Coordinator" },
            new() { FirstName = "Sally", LastName = "Cole", EmailAddress = "sallyc@somedomain.com", Status = false, JoiningDate = DateTime.Parse("2024-07-19"), MemberType = "CEO", JobTitle = "Director" },
            new() { FirstName = "Kevin", LastName = "Baker", EmailAddress = "kevin@somedomain.com", Status = true, JoiningDate = DateTime.Parse("2024-07-17"), MemberType = "Manager", JobTitle = "Escalation Manager" },
            new() { FirstName = "Amanda", LastName = "B", EmailAddress = "amandab@somedomain.com", Status = true, JoiningDate = DateTime.Parse("2024-07-19"), MemberType = "Senior Manager", JobTitle = "Sales Manager" }
        };
        return apiData2;
    }
}
