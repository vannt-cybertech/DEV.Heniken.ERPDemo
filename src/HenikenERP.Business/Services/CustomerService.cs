using System;
using System.Collections.Generic;
using HenikenERP.Core.Entities;
using HenikenERP.Data.Context;
using HenikenERP.Data.UnitOfWork;
using HenikenERP.Common.Helpers;

namespace HenikenERP.Business.Services
{
    /// <summary>
    /// Customer service for customer management
    /// </summary>
    public class CustomerService
    {
        private readonly UnitOfWork _unitOfWork;
        
        public CustomerService()
        {
            var context = new DatabaseContext();
            _unitOfWork = new UnitOfWork(context);
        }
        
        public CustomerService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public IEnumerable<Customer> GetAllCustomers()
        {
            return _unitOfWork.Customers.GetAll();
        }
        
        public Customer GetCustomerById(string id)
        {
            return _unitOfWork.Customers.GetById(id);
        }
        
        public bool CreateCustomer(Customer customer)
        {
            try
            {
                if (customer == null || string.IsNullOrWhiteSpace(customer.Customer_ID))
                {
                    return false;
                }
                
                if (_unitOfWork.Customers.Exists(customer.Customer_ID))
                {
                    return false;
                }
                
                _unitOfWork.Customers.Add(customer);
                _unitOfWork.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError("Error creating customer", ex);
                return false;
            }
        }
        
        public bool UpdateCustomer(Customer customer)
        {
            try
            {
                if (customer == null || string.IsNullOrWhiteSpace(customer.Customer_ID))
                {
                    return false;
                }
                
                _unitOfWork.Customers.Update(customer);
                _unitOfWork.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError("Error updating customer", ex);
                return false;
            }
        }
        
        public bool DeleteCustomer(string id)
        {
            try
            {
                _unitOfWork.Customers.DeleteById(id);
                _unitOfWork.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError("Error deleting customer", ex);
                return false;
            }
        }
    }
}

