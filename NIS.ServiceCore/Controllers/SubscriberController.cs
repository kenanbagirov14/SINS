using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using NIS.BLCore.Logics;
using NIS.BLCore.Models.Subscriber;
using NIS.BLCore.Models.Core;

namespace NIS.ServiceCore.Controllers
{
    
    public class SubscriberController : ApiController
    {
        #region Properties

        private readonly SubscriberLogic _subscriberLogic;

        #endregion

        #region Constructor

        public SubscriberController()
        {
            _subscriberLogic = new SubscriberLogic(User);
        }

        #endregion

        #region Subscriber Get

        public IEnumerable<SubscriberViewModel> GetAll([FromUri]Filter filter)
        {
            return  _subscriberLogic.GetAll(filter);
        }

        public async Task<IEnumerable<SubscriberViewModel>> GetAllAsync([FromUri]Filter filter)
        {
            return await _subscriberLogic.GetAllAsync(filter);
        }

        public async Task<SubscriberViewModel> GetByIdAsync(int id)
        {
            return await _subscriberLogic.GetByIdAsync(id);
        }

        public SubscriberViewModel GetById(int id)
        {
            return  _subscriberLogic.GetById(id);
        }

        #endregion

        #region Subscriber Find

        [HttpPost]
        public SubscriberViewModel Find(SubscriberFindModel parameter)
        {
            return _subscriberLogic.Find(parameter);
        }

        [HttpPost]
        public async Task<SubscriberViewModel> FindAsync(SubscriberFindModel parameter)
        {
            return await _subscriberLogic.FindAsync(parameter);
        }

        [HttpPost]
        public ICollection<SubscriberViewModel> FindAll(SubscriberFindModel parameter)
        {
            return _subscriberLogic.FindAll(parameter);
        }

        [HttpPost]
        public async Task<ICollection<SubscriberViewModel>> FindAllAsync(SubscriberFindModel parameter)
        {
            return await _subscriberLogic.FindAllAsync(parameter);
        }

        #endregion

        #region Subscriber Add

        [HttpPost]
        public SubscriberViewModel Add(SubscriberCreateModel entity)
        {
            return _subscriberLogic.Add(entity);
        }

        [HttpPost]
        public async Task<SubscriberViewModel> AddAsync(SubscriberCreateModel entity)
        {
            return await _subscriberLogic.AddAsync(entity);
        }

        [HttpPost]
        public async Task<List<SubscriberViewModel>> AddRangeAsync(List<SubscriberCreateModel> entities)
        {
            return await _subscriberLogic.AddRangeAsync(entities);
        }

        #endregion

        #region Subscriber Update

        [HttpPost]
        public SubscriberViewModel Update(SubscriberUpdateModel entity)
        {
            return _subscriberLogic.Update(entity);
        }

        [HttpPost]
        public async Task<SubscriberViewModel> UpdateAsync(SubscriberUpdateModel entity)
        {
            return await _subscriberLogic.UpdateAsync(entity);
        }
        #endregion

        #region Subscriber Remove



        #endregion
    }
}
