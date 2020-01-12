using System.Collections.Generic;

namespace Customer.EFRepository
{
    public interface ICustomerRepository
    {
        List<Entity.Customer> Search(string searchText);

        Entity.Customer Get(int id);

        int Add(Entity.Customer customer);

        bool Update(Entity.Customer customer);

        bool Remove(int id);
    }
}
