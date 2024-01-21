namespace NIS.BLCore.Models.RequestStatusHistory
{
    public class RequestStatusHistoryCreateModel
    {
        public int CustomerRequestId { get; set; }
        public int RequestStatusId { get; set; }
        public string Description { get; set; }

        public bool runSignalR { get; set; } = true; //TODO
    }
}
