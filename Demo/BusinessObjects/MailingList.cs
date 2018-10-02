using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessObjects
{
    public class MailingList
    {
        private List<Customer> _list = new List<Customer>();

        public void Add(Customer newCustomer)
        {
            _list.Add(newCustomer);
        }

        public Customer Find(int id)
        {
            foreach (Customer c in _list)
            {
                if (id == c.ID)
                {
                    return c;
                }
            }

            return null;

        }

        public void Delete(int id)
        {
            Customer c = this.Find(id);
            if (c != null)
            {
                _list.Remove(c);
            }

        }

        public List<int> Matrics
        {
            get
            {
               List<int> res = new List<int>();
               foreach(Customer p in _list)
                   res.Add(p.ID);
                return res;
            }
           
        }
    }
}
