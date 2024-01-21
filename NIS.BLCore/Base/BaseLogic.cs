using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using log4net;
using NIS.BLCore.Mapping;
using NIS.BLCore.Models.Area;
using NIS.BLCore.Models.Core;
using NIS.DALCore.UnitOfWork;
using System.Web;
using System.Security.Claims;
using Newtonsoft.Json;
using NIS.UtilsCore;
using NIS.UtilsCore.Enums;
using NIS.BLCore.Extensions;

namespace NIS.BLCore.Base
{
    public abstract class BaseLogic<TView, TAdd, TFind, TUpdate, TRemove> where TView : class where TAdd : class where TFind : class where TUpdate : class where TRemove : class
    {
        #region Properties

        public static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //public int? GeneralUserId { get; set; }

        #endregion

        #region Parameters 
        public UnitOfWork Uow { get; set; }
        public LogicResult<TView> Result { get; set; }
        public LogicResult<ICollection<TView>> ResultAll { get; set; }
        public int GeneralUserId;
        public ClaimsPrincipal User;

        #endregion

        #region Constructor

        protected BaseLogic( ClaimsPrincipal user)
        {
            Uow = new UnitOfWork();
            Result = new LogicResult<TView>();
            ResultAll = new LogicResult<ICollection<TView>>();
            User = user;
            GeneralUserId = User.GetUserId();
        }

        #endregion

        #region Operations

        #region Get
        public LogicResult<ICollection<TView>> GetAll(Filter parameters)
        {
            // Log request parameters
            
            var json = JsonConvert.SerializeObject(parameters);
            Logger.Info(
               "Executing process started with this parameters: " + json
              + $"{Environment.NewLine}"
              + " - Executed class : " + this.GetType()
              + $"{Environment.NewLine}"
              + " - Executed method : GetAll");
            // Check if the result is success
            if (!ResultAll.IsSuccess)
                return ResultAll;
            try
            {
                GetAllAbstract(parameters);
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                    ex = ex.InnerException;
                //Log error
                Logger.Error("Error occured: ", ex);

                //Set error output
                ResultAll.ErrorList.Add(new Error()
                {
                    Type = OperationResultCode.Exception,
                    Code = "SystemError",
                    Text = ex.Message
                });
            }
            return ResultAll;
        }

        public LogicResult<TView> GetById(int id)
        {
            // Log request parameters
            var json = JsonConvert.SerializeObject(id);
            Logger.Info(
               "Executing process started with this parameters: " + json
              + $"{Environment.NewLine}"
              + " - Executed class : " + this.GetType()
              + $"{Environment.NewLine}"
              + " - Executed method : GetById");
            // Check if the result is success
            if (!Result.IsSuccess)
                return Result;
            try
            {

            }
            catch (global::System.Exception)
            {

                throw;
            }
            {
                GetByIdAbstract(id);
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                    ex = ex.InnerException;
                //Log error
                Logger.Error("Error occured: ", ex);

                //Set error output
                Result.ErrorList.Add(new Error()
                {
                    Type = OperationResultCode.Exception,
                    Code = "SystemError",
                    Text = ex.Message
                });
            }
            return Result;
        }
        #endregion

        #region Find
        public LogicResult<TView> Find(TFind parameter)
        {
            // Log request parameters
            //var json = new JavaScriptSerializer().Serialize(parameter);
            //Logger.Info(
            //   "Executing process started with this parameters: " + json
            //  + $"{Environment.NewLine}"
            //  + " - Executed class : " + this.GetType()
            //  + $"{Environment.NewLine}"
            //  + " - Executed method : Find");
            // Check if the result is success
            if (!Result.IsSuccess)
                return Result;
            try
             {
                FindAbstract(parameter);
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                    ex = ex.InnerException;
                //Log error
                Logger.Error("Error occured: ", ex);

                //Set error output
                Result.ErrorList.Add(new Error()
                {
                    Type = OperationResultCode.Exception,
                    Code = "SystemError",
                    Text = ex.Message
                });
            }
            return Result;
        }

        public LogicResult<ICollection<TView>> FindAll(TFind parameter)
        {
            // Log request parameters
            //var json = new JavaScriptSerializer().Serialize(parameter);
            //Logger.Info(
            //   "Executing process started with this parameters: " + json
            //  + $"{Environment.NewLine}"
            //  + " - Executed class : " + this.GetType()
            //  + $"{Environment.NewLine}"
            //  + " - Executed method : FindAll");
            //// Check if the result is success
            if (!ResultAll.IsSuccess)
                return ResultAll;
            try
            {
                FindAllAbstract(parameter);
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                    ex = ex.InnerException;
                //Log error
                Logger.Error("Error occured: ", ex);

                //Set error output
                ResultAll.ErrorList.Add(new Error()
                {
                    Type = OperationResultCode.Exception,
                    Code = "SystemError",
                    Text = ex.Message
                });
            }
            return ResultAll;
        }

        #endregion

        #region Add
        public LogicResult<TView> Add(TAdd parameter)
        {
            // Log request parameters
            var json = JsonConvert.SerializeObject(parameter);
            Logger.Info(
               "Executing process started with this parameters: " + json
              + $"{Environment.NewLine}"
              + " - Executed class : " + this.GetType()
              + $"{Environment.NewLine}"
              + " - Executed method : add");
            // Check if the result is success
            if (!Result.IsSuccess)
                return Result;
            try
            {
                AddAbstract(parameter);
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                    ex = ex.InnerException;
                //Log error
                Logger.Error("Error occured: ", ex);

                //Set error output
                Result.ErrorList.Add(new Error()
                {
                    Type = OperationResultCode.Exception,
                    Code = "SystemError",
                    Text = ex.Message
                });
            }
            return Result;
        }
        public LogicResult<TView> AddRange(List<TAdd> parameter)
        {
            // Log request parameters
            var json = JsonConvert.SerializeObject(parameter);
            Logger.Info(
               "Executing process started with this parameters: " + json
              + $"{Environment.NewLine}"
              + " - Executed class : " + this.GetType()
              + $"{Environment.NewLine}"
              + " - Executed method : add");
            // Check if the result is success
            if (!Result.IsSuccess)
                return Result;
            try
            {
                AddRangeAbstract(parameter);
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                    ex = ex.InnerException;
                //Log error
                Logger.Error("Error occured: ", ex);

                //Set error output
                Result.ErrorList.Add(new Error()
                {
                    Type = OperationResultCode.Exception,
                    Code = "SystemError",
                    Text = ex.Message
                });
            }
            return Result;
        }

        #endregion

        #region Update
        public LogicResult<TView> Update(TUpdate parameter)
        {
            // Log request parameters
            var json = JsonConvert.SerializeObject(parameter);
            Logger.Info(
              "Executing process started with this parameters: " + json
              + $"{Environment.NewLine}"
              + " - Executed class : " + this.GetType()
              + $"{Environment.NewLine}"
              + " - Executed method : Update");
            // Check if the result is success
            if (!Result.IsSuccess)
                return Result;
            try
            {
                UpdateAbstract(parameter);
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                    ex = ex.InnerException;
                //Log error
                Logger.Error("Error occured: ", ex);

                //Set error output
                Result.ErrorList.Add(new Error()
                {
                    Type = OperationResultCode.Exception,
                    Code = "SystemError",
                    Text = ex.Message
                });
            }
            return Result;
        }

        public LogicResult<ICollection<TView>> UpdateRange(TUpdate parameter)
        {
            // Log request parameters
            var json = JsonConvert.SerializeObject(parameter);
            Logger.Info(
              "Executing process started with this parameters: " + json
              + $"{Environment.NewLine}"
              + " - Executed class : " + this.GetType()
              + $"{Environment.NewLine}"
              + " - Executed method : UpdateRange");
            // Check if the result is success
            if (!ResultAll.IsSuccess)
                return ResultAll;
            try
            {
                UpdateRangeAbstract(parameter);
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                    ex = ex.InnerException;
                //Log error
                Logger.Error("Error occured: ", ex);

                //Set error output
                Result.ErrorList.Add(new Error()
                {
                    Type = OperationResultCode.Exception,
                    Code = "SystemError",
                    Text = ex.Message
                });
            }
            return ResultAll;
        }

        #endregion

        #region Remove

        public LogicResult<TView> Remove(TRemove parameter)
        {
            // Log request parameters
            var json = JsonConvert.SerializeObject(parameter);
            Logger.Info(
              "Executing process started with this parameters: " + json
              + $"{Environment.NewLine}"
              + " - Executed class : " + this.GetType()
              + $"{Environment.NewLine}"
              + " - Executed method : Remove");
            // Check if the result is success
            if (!Result.IsSuccess)
                return Result;
            try
            {
                RemoveAbstract(parameter);
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                    ex = ex.InnerException;
                //Log error
                Logger.Error("Error occured: ", ex);

                //Set error output
                Result.ErrorList.Add(new Error()
                {
                    Type = OperationResultCode.Exception,
                    Code = "SystemError",
                    Text = ex.Message
                });
            }
            return Result;
        }

        #endregion
        #region Dispose
        public void Dispose()
        {
            Uow.Dispose();
        }
        #endregion
        #endregion

        #region Abstract Methods

        protected abstract void GetAllAbstract(Filter filter);
        protected abstract void GetByIdAbstract(int id);
        protected abstract void FindAbstract(TFind parameter);
        protected abstract void FindAllAbstract(TFind parameter);
        protected abstract void AddAbstract(TAdd parameter);
        protected abstract void AddRangeAbstract(List<TAdd> parameters);
        protected abstract void UpdateAbstract(TUpdate parameter);
        protected abstract void UpdateRangeAbstract(TUpdate parameter);
        protected abstract void RemoveAbstract(TRemove parameter);

        #endregion

    }
}
