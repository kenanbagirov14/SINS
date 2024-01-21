using NIS.BLCore.DTO;

namespace NIS.BLCore.Models.Node
{
    public class NodeViewModel
    {
        public int Id { get; set; }
        public int? AreaId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? TotalCount { get; set; }

        public AreaDto Area { get; set; }
    }
}
