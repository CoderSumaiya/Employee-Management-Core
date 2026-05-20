namespace MvcCore_employeeProject.Models.ViewModels
{
    public class ManageUserRoleViewModel
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public List<RoleSelection> Roles { get; set; } = new();
    }
}
