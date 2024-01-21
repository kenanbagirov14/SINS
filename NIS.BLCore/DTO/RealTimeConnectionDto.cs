namespace NIS.BLCore.DTO
{
    public class RealTimeConnectionDto
    {
    public int Id { get; set; }
      public int UserId { get; set; }
      public string ConnectionId { get; set; }
      public string Description { get; set; }
    
      public virtual UserDto User { get; set; }
  }
}
