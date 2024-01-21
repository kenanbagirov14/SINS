using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using NIS.BLCore.Logics;
using NIS.BLCore.Models.Core;
using NIS.BLCore.Models.RequestStatusHistory;

namespace NIS.Service.Controllers
{
    public class RequestStatusHistoryController : ApiController
    {
        #region Properties

        private readonly RequestStatusHistoryLogic _requestStatusHistoryLogic;

        #endregion

        #region Constructor

        public RequestStatusHistoryController()
        {
            _requestStatusHistoryLogic = new RequestStatusHistoryLogic(User);
        }

        #endregion

        #region RequestStatusHistory Get

        public IEnumerable<RequestStatusHistoryViewModel> GetAll([FromUri]Filter filter)
        {
            return  _requestStatusHistoryLogic.GetAll(filter);
        }

        public async Task<IEnumerable<RequestStatusHistoryViewModel>> GetAllAsync([FromUri]Filter filter)
        {
            return await _requestStatusHistoryLogic.GetAllAsync(filter);
        }

        public async Task<RequestStatusHistoryViewModel> GetByIdAsync(int id)
        {
            return await _requestStatusHistoryLogic.GetByIdAsync(id);
        }

        public RequestStatusHistoryViewModel GetById(int id)
        {
            return  _requestStatusHistoryLogic.GetById(id);
        }

        #endregion

        #region RequestStatusHistory Find

        [HttpPost]
        public RequestStatusHistoryViewModel Find(RequestStatusHistoryFindModel parameter)
        {
            return _requestStatusHistoryLogic.Find(parameter);
        }

        [HttpPost]
        public async Task<RequestStatusHistoryViewModel> FindAsync(RequestStatusHistoryFindModel parameter)
        {
            return await _requestStatusHistoryLogic.FindAsync(parameter);
        }

        [HttpPost]
        public ICollection<RequestStatusHistoryViewModel> FindAll(RequestStatusHistoryFindModel parameter)
        {
            return _requestStatusHistoryLogic.FindAll(parameter);
        }

        [HttpPost]
        public async Task<ICollection<RequestStatusHistoryViewModel>> FindAllAsync(RequestStatusHistoryFindModel parameter)
        {
            return await _requestStatusHistoryLogic.FindAllAsync(parameter);
        }

        #endregion

        #region RequestStatusHistory Add

        [HttpPost]
        public RequestStatusHistoryViewModel Add(RequestStatusHistoryCreateModel entity)
        {
            return _requestStatusHistoryLogic.Add(entity);
        }

        [HttpPost]
        public async Task<RequestStatusHistoryViewModel> AddAsync(RequestStatusHistoryCreateModel entity)
        {
            return await _requestStatusHistoryLogic.AddAsync(entity);
        }

        [HttpPost]
        public async Task<List<RequestStatusHistoryViewModel>> AddRAngeAsync(List<RequestStatusHistoryCreateModel> entities)
        {
            return await _requestStatusHistoryLogic.AddRangeAsync(entities);
        }

        #endregion

        #region RequestStatusHistory Update

        [HttpPost]
        public RequestStatusHistoryUpdateModel Update(RequestStatusHistoryUpdateModel entity)
        {
            return _requestStatusHistoryLogic.Update(entity);
        }

        [HttpPost]
        public async Task<RequestStatusHistoryUpdateModel> UpdateAsync(RequestStatusHistoryUpdateModel entity)
        {
            return await _requestStatusHistoryLogic.UpdateAsync(entity);
        }
        #endregion

        #region RequestStatusHistory Remove



        #endregion
    }
}
