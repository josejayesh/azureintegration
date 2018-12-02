using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Entites
{
    class Customer : TableEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public Customer(string name, string email)
        {
            this.Name = name;
            this.Email = email;
            this.PartitionKey = "USA";
            this.RowKey = email;
        }
        public Customer()
        {

        }
    }
}

