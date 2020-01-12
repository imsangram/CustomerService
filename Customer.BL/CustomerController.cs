using AutoMapper;
using Customer.Dto;
using Customer.EFRepository;
using System;
using System.Collections.Generic;

namespace Customer.BL
{
    public class CustomerController
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public List<CustomerDto> Search(string searchName = "")
        {
            var list = _customerRepository.Search(searchName);
            var mapper = Mapper.GetMapperConfiguration(); 
            return mapper.Map<List<Entity.Customer>, List<CustomerDto>>(list);
        }

        public CustomerDto Get(int id)
        {
            var cust = _customerRepository.Get(id);
            var mapper = Mapper.GetMapperConfiguration();
            return mapper.Map<Entity.Customer, CustomerDto>(cust);
        }

        public int Add(CustomerCreateDto newCustomer)
        {
            var mapper = Mapper.GetMapperConfiguration();
            Entity.Customer cust = mapper.Map<CustomerCreateDto, Entity.Customer>(newCustomer);
            return _customerRepository.Add(cust);
        }

        public bool Update(CustomerDto customer)
        {
            var mapper = Mapper.GetMapperConfiguration();
            Entity.Customer cust = mapper.Map<CustomerDto, Entity.Customer>(customer);
            return _customerRepository.Update(cust);
        }

        public bool Delete(int customerId)
        {
            return _customerRepository.Remove(customerId);
        }
    }
}
