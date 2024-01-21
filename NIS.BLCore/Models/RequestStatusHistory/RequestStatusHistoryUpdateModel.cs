namespace NIS.BLCore.Models.RequestStatusHistory
{
    public class RequestStatusHistoryUpdateModel
    {
        public int Id { get; set; }
        public int CustomerRequestId { get; set; }
        public int RequestStatusId { get; set; }
        public string Description { get; set; }
    }
}
