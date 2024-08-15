namespace ContactFieldMapping.BAL.ViewModel;

public class ApiResponse2ViewModel
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
    public bool Status { get; set; }
    public DateTime JoiningDate { get; set; }
    public string MemberType { get; set; } = string.Empty;
    public string JobTitle { get; set; } = string.Empty;
}
