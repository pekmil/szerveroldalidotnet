using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventApp.Models;
using EventApp.UnitOfWork;

namespace EventApp.Services {
    public class ApiLogService : IApiLogService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ApiLogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Log(ApiLogEntry entry)
        {
            try
            {
                _unitOfWork.GetRepository<ApiLogEntry>().InsertRange(new []{entry});
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                //TODO: log
            }
        }

        public IQueryable<ApiLogEntry> GetEntries()
        {
            return _unitOfWork.GetRepository<ApiLogEntry>().GetAll();
        }
    }

}