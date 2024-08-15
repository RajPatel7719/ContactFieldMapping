using System.ComponentModel.DataAnnotations;

namespace ContactFieldMapping.BAL.ViewModel;

public class MappingViewModel
{

    [Required(ErrorMessage = "First Name field is required.")]
    public string SelectedFirstNameField { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last Name field is required.")]
    public string SelectedLastNameField { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email field is required.")]
    public string SelectedEmailField { get; set; } = string.Empty;

    [Required(ErrorMessage = "Is Active field is required.")]
    public string SelectedIsActiveField { get; set; } = string.Empty;

    [Required(ErrorMessage = "Joining Date field is required.")]
    public string SelectedJoiningDateField { get; set; } = string.Empty;

    [Required(ErrorMessage = "Member Type field is required.")]
    public string SelectedMemberTypeField { get; set; } = string.Empty;

    [Required(ErrorMessage = "Job Title field is required.")]
    public string SelectedJobTitleField { get; set; } = string.Empty;

    public string? TransformationField { get; set; }
    public string? TransformationType { get; set; }
    public string? CriteriaField { get; set; }
    public string? CriteriaOperator { get; set; }
    public string? CriteriaValue { get; set; }
    public List<string> ApiFields { get; set; } = [];
    public DateTime CutoffDate { get; set; }

    public bool IsCriteriaSelected => !string.IsNullOrEmpty(CriteriaField) &&
                                     !string.IsNullOrEmpty(CriteriaOperator) &&
                                     !string.IsNullOrEmpty(CriteriaValue);
}
