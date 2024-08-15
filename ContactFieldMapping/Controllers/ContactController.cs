using ContactFieldMapping.BAL.IService;
using ContactFieldMapping.BAL.Service;
using ContactFieldMapping.BAL.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ContactFieldMapping.Controllers;

public class ContactController(IContactService contactService) : Controller
{
    private readonly IContactService _contactService = contactService;

    private readonly List<string> _apiFields = ["FirstName", "LastName", "EmailAddress", "Status", "JoiningDate", "MemberType", "JobTitle"];

    public async Task<IActionResult> Index()
    {
        var contacts = await _contactService.ListContactsAsync();
        return View(contacts);
    }

    [HttpGet]
    public IActionResult LoadData()
    {
        var viewModel = new MappingViewModel
        {
            ApiFields = _apiFields
        };
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> LoadData(MappingViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            viewModel.ApiFields = _apiFields;
            return View(viewModel);
        }

        var isSuccess = await _contactService.LoadDataAsync(viewModel);

        if (!isSuccess)
        {
            TempData["ErrorMessage"] = "An error occurred while processing your request. Please try again later.";
            viewModel.ApiFields = _apiFields;
            return View(viewModel);
        }

        return RedirectToAction(nameof(Index));
    }
}
