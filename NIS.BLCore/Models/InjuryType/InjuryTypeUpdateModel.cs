namespace NIS.BLCore.Models.InjuryType
{
    public class InjuryTypeUpdateModel
    {
        public int Id { get; set; }
        public int? DepartmentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ExecutionDay { get; set; }
    }
}
