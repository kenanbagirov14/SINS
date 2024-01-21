namespace NIS.BLCore.Models.Area
{
    public class AreaCreateModel
    {
        public int RegionId { get; set; }
        public string Name { get; set; }
        public int? AreaPrefix { get; set; }
        public string Description { get; set; }
    }
}
