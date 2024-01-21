namespace NIS.BLCore.Models.CustomerRequestType
{
    public class RequestTypeUpdateModel
    {
        public int Id { get; set; }
        public int? DepartmentId { get; set; }
        public string Name { get; set; }
        public int ExecutionDay { get; set; }
        public string Description { get; set; }
        public int SourceTypeId { get; set; }
        public int? ParentRequestTypeId { get; set; }
    }
}
