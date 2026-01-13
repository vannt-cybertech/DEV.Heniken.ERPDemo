using System.Collections.Generic;
using System.Linq;
using HenikenERP.Core.Entities;
using HenikenERP.Data.Context;
using HenikenERP.Data.UnitOfWork;

namespace HenikenERP.Business.Services
{
    /// <summary>
    /// Customer tier service
    /// </summary>
    public class CustomerTierService
    {
        private readonly UnitOfWork _unitOfWork;
        
        public CustomerTierService()
        {
            var context = new DatabaseContext();
            _unitOfWork = new UnitOfWork(context);
        }
        
        public IEnumerable<CustomerTier> GetAllTiers()
        {
            return _unitOfWork.CustomerTiers.GetAll();
        }
        
        public CustomerTier GetTierById(string id)
        {
            return _unitOfWork.CustomerTiers.GetById(id);
        }
        
        public bool CreateTier(CustomerTier tier)
        {
            try
            {
                _unitOfWork.CustomerTiers.Add(tier);
                _unitOfWork.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public bool UpdateTier(CustomerTier tier)
        {
            try
            {
                _unitOfWork.CustomerTiers.Update(tier);
                _unitOfWork.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public bool DeleteTier(string id)
        {
            try
            {
                _unitOfWork.CustomerTiers.DeleteById(id);
                _unitOfWork.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public int GetCustomerCountByTier(string tierId)
        {
            try
            {
                return _unitOfWork.Customers.Find(c => c.Tier_ID == tierId).Count();
            }
            catch
            {
                return 0;
            }
        }
    }
}

