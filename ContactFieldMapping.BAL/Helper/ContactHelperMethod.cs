using ContactFieldMapping.BAL.ViewModel;
using ContactFieldMapping.DAL.Model;

namespace ContactFieldMapping.BAL.Helper;

public static class ContactHelperMethod
{
    public static Contact MapAndTransform(object response, MappingViewModel mapping)
    {
        var contact = new Contact
        {
            FirstName = GetValueFromField(response, mapping.SelectedFirstNameField),
            LastName = GetValueFromField(response, mapping.SelectedLastNameField),
            Email = GetValueFromField(response, mapping.SelectedEmailField),
            IsActive = TransformStatus(GetValueFromField(response, mapping.SelectedIsActiveField)),
            JoiningDate = DateTime.Parse(GetValueFromField(response, mapping.SelectedJoiningDateField)),
            MemberType = GetValueFromField(response, mapping.SelectedMemberTypeField),
            JobTitle = GetValueFromField(response, mapping.SelectedJobTitleField),
            IsMember = DetermineMembership(
                GetValueFromField(response, mapping.SelectedIsActiveField),
                GetValueFromField(response, mapping.SelectedMemberTypeField))
        };

        return contact;
    }

    public static string GetValueFromField(object response, string fieldName)
    {
        var prop = response.GetType().GetProperty(fieldName);
        return prop != null ? prop.GetValue(response)?.ToString() : null;
    }

    public static bool DetermineMembership(object status, string memberType)
    {
        var isActive = TransformStatus(status);
        var validTypes = new[] { "Senior Manager", "Escalation Manager", "Sales Manager" };
        return isActive && validTypes.Contains(memberType);
    }

    public static bool TransformStatus(object status) => status is string ? (string)status == "Active" : status is bool && (bool)status;

    public static bool ApplyCustomCriteria(Contact contact, string criteriaField, string operatorType, string criteriaValue)
    {
        var property = contact.GetType().GetProperty(criteriaField);
        if (property == null) return false;

        var value = property.GetValue(contact)?.ToString();

        return ApplyCriteria(value, operatorType, criteriaValue);
    }

    public static bool ApplyCriteria(object value, string operatorType, string criteriaValue)
    {
        if (value != null)
        {
            return operatorType switch
            {
                "Equals" => value.ToString() == criteriaValue,
                "NotEquals" => value.ToString() != criteriaValue,
                "GreaterThan" => DateTime.TryParse(value.ToString(), out var dateValue) && dateValue > DateTime.Parse(criteriaValue),
                "LessThan" => DateTime.TryParse(value.ToString(), out var dateValue) && dateValue < DateTime.Parse(criteriaValue),
                _ => false,
            };
        }
        else
        {
            return false;
        }
    }

    public static bool ApplyCriteria(DateTime joiningDate, DateTime cutoffDate) => joiningDate > cutoffDate;

}
