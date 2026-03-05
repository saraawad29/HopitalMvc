namespace HopitalMvcSqlite.Models.ViewModels;

public class DepartmentStatsVm
{
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; } = "";
    public int DoctorsCount { get; set; }
    public int ConsultationsCount { get; set; }
}