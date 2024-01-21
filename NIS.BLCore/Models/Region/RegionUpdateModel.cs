namespace NIS.BLCore.Models.Region
{
    public class RegionUpdateModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? RegionPrefix { get; set; }
        public string Description { get; set; }
        public int? EngineerId { get; set; }
    }
}
