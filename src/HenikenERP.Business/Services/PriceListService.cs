using System;
using System.Collections.Generic;
using HenikenERP.Core.Entities;
using HenikenERP.Data.Context;
using HenikenERP.Data.Repositories;
using HenikenERP.Data.UnitOfWork;
using HenikenERP.Common.Helpers;

namespace HenikenERP.Business.Services
{
    /// <summary>
    /// Price list service for price management
    /// </summary>
    public class PriceListService
    {
        private readonly UnitOfWork _unitOfWork;
        
        public PriceListService()
        {
            var context = new DatabaseContext();
            _unitOfWork = new UnitOfWork(context);
        }
        
        public PriceListService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        /// <summary>
        /// Get active price for product and tier
        /// </summary>
        public decimal? GetActivePrice(string productId, string tierId, DateTime date)
        {
            try
            {
                var priceRepository = _unitOfWork.PriceLists as PriceListRepository;
                var price = priceRepository?.GetActivePrice(productId, tierId, date);
                return price?.Unit_Price;
            }
            catch (Exception ex)
            {
                Logger.LogError("Error getting active price", ex);
                return null;
            }
        }
        
        public IEnumerable<PriceList> GetAllPrices()
        {
            return _unitOfWork.PriceLists.GetAll();
        }
        
        public PriceList GetPriceById(string id)
        {
            return _unitOfWork.PriceLists.GetById(id);
        }
        
        public bool CreatePrice(PriceList price)
        {
            try
            {
                if (price == null || string.IsNullOrWhiteSpace(price.Price_ID))
                {
                    return false;
                }
                
                _unitOfWork.PriceLists.Add(price);
                _unitOfWork.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError("Error creating price", ex);
                return false;
            }
        }
        
        public bool UpdatePrice(PriceList price)
        {
            try
            {
                _unitOfWork.PriceLists.Update(price);
                _unitOfWork.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError("Error updating price", ex);
                return false;
            }
        }
        
        public bool DeletePrice(string id)
        {
            try
            {
                _unitOfWork.PriceLists.DeleteById(id);
                _unitOfWork.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError("Error deleting price", ex);
                return false;
            }
        }
    }
}

