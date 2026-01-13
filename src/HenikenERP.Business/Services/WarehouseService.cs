using System.Collections.Generic;
using HenikenERP.Core.Entities;
using HenikenERP.Data.Context;
using HenikenERP.Data.UnitOfWork;

namespace HenikenERP.Business.Services
{
    /// <summary>
    /// Warehouse service
    /// </summary>
    public class WarehouseService
    {
        private readonly UnitOfWork _unitOfWork;
        
        public WarehouseService()
        {
            var context = new DatabaseContext();
            _unitOfWork = new UnitOfWork(context);
        }
        
        public IEnumerable<Warehouse> GetAllWarehouses()
        {
            return _unitOfWork.Warehouses.GetAll();
        }
        
        public Warehouse GetWarehouseById(string id)
        {
            return _unitOfWork.Warehouses.GetById(id);
        }
        
        public bool CreateWarehouse(Warehouse warehouse)
        {
            try
            {
                _unitOfWork.Warehouses.Add(warehouse);
                _unitOfWork.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public bool UpdateWarehouse(Warehouse warehouse)
        {
            try
            {
                _unitOfWork.Warehouses.Update(warehouse);
                _unitOfWork.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public bool DeleteWarehouse(string id)
        {
            try
            {
                _unitOfWork.Warehouses.DeleteById(id);
                _unitOfWork.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

