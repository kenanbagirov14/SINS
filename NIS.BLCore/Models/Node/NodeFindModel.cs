namespace NIS.BLCore.Models.Node
{
    public class NodeFindModel
    {
        public int? Id { get; set; }
        public int? AreaId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }
    }
}
