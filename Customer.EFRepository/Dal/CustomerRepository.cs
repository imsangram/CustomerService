using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Customer.EFRepository.Dal
{
    public class CustomerRepository : ICustomerRepository
    {
        private CustomerDBContext _context;
        public CustomerRepository(CustomerDBContext context)
        {
            _context = context;
        }

        public List<Entity.Customer> Search(string searchText)
        {
            if (!string.IsNullOrEmpty(searchText))
            {
                return _context.Set<Entity.Customer>().Where(BuildLikeExpression(searchText)).ToList();
            }
            else
            {
               return  _context.Customers.ToList();
            }

        }

        public Entity.Customer Get(int id)
        {
            return _context.Customers.FirstOrDefault(x => x.Id == id);
        }

        public int Add(Entity.Customer customer)
        {
            if (customer.Id == 0)
            {
                // to support In memory database primary key constraint
                customer.Id = _context.Customers.Select(x => x.Id).Max() + 1;

                _context.Customers.Add(customer);
                _context.SaveChanges();
            }
            return customer.Id;
        }

        public bool Update(Entity.Customer customer)
        {
            var cust = _context.Customers.FirstOrDefault(x => x.Id == customer.Id);
            if (cust != null)
            {
                cust.FirstName = customer.FirstName;
                cust.LastName = customer.LastName;
                cust.DateOfBirth = customer.DateOfBirth;

                _context.Customers.Update(cust);
                _context.SaveChanges();
                return true;
            }
            return false;  
        }

        public bool Remove(int id)
        {
            var cust = _context.Customers.FirstOrDefault(x => x.Id == id);
            if(cust != null)
            {
                _context.Customers.Remove(cust);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        protected Expression<Func<Entity.Customer, bool>> BuildLikeExpression(string searchText)
        {
            var likeSearch = $"%{searchText}%";
            return t => EF.Functions.Like(t.FirstName, likeSearch)
                        || EF.Functions.Like(t.LastName, likeSearch);
        }
    }
}
