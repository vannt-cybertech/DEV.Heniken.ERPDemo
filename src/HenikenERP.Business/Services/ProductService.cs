using System;
using System.Collections.Generic;
using HenikenERP.Core.Entities;
using HenikenERP.Core.Interfaces;
using HenikenERP.Data.Context;
using HenikenERP.Data.UnitOfWork;
using HenikenERP.Common.Helpers;

namespace HenikenERP.Business.Services
{
    /// <summary>
    /// Product service for product management
    /// </summary>
    public class ProductService
    {
        private readonly UnitOfWork _unitOfWork;
        
        public ProductService()
        {
            var context = new DatabaseContext();
            _unitOfWork = new UnitOfWork(context);
        }
        
        public ProductService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public IEnumerable<Product> GetAllProducts()
        {
            return _unitOfWork.Products.GetAll();
        }
        
        public Product GetProductById(string id)
        {
            return _unitOfWork.Products.GetById(id);
        }
        
        public bool CreateProduct(Product product)
        {
            try
            {
                if (product == null || string.IsNullOrWhiteSpace(product.Product_ID))
                {
                    return false;
                }
                
                if (_unitOfWork.Products.Exists(product.Product_ID))
                {
                    return false;
                }
                
                _unitOfWork.Products.Add(product);
                _unitOfWork.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError("Error creating product", ex);
                return false;
            }
        }
        
        public bool UpdateProduct(Product product)
        {
            try
            {
                if (product == null || string.IsNullOrWhiteSpace(product.Product_ID))
                {
                    return false;
                }
                
                if (!_unitOfWork.Products.Exists(product.Product_ID))
                {
                    return false;
                }
                
                _unitOfWork.Products.Update(product);
                _unitOfWork.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError("Error updating product", ex);
                return false;
            }
        }
        
        public bool DeleteProduct(string id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    return false;
                }
                
                _unitOfWork.Products.DeleteById(id);
                _unitOfWork.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError("Error deleting product", ex);
                return false;
            }
        }
    }
}

