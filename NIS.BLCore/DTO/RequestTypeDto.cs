namespace NIS.BLCore.DTO
{
    public class RequestTypeDto
    {
        public int Id { get; set; }
        public int? DepartmentId { get; set; }
        public int? ParentRequestTypeId { get; set; }
        public string Name { get; set; }
        public int ExecutionDay { get; set; }
        public int? CustomerRequestTypeId { get; set; }
        public string Description { get; set; }
        public string Alias { get; set; }
    }
}
