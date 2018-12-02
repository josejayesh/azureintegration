using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.Azure;
using ConsoleApp1.Entites;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var cloudStorage = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnection"));
            CloudTableClient tableClient = cloudStorage.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("customers");
            table.CreateIfNotExists();
            /*createCustomer(table, new Customer("Cust1", "cust1@localhost.local"));
            createCustomer(table, new Customer("Cust2", "cust2@localhost.local"));
            createCustomer(table, new Customer("Cust3", "cust3@localhost.local"));
            createCustomer(table, new Customer("Delete", "delete@localhost.local"));
            getCustomer(table, "USA", "cust4@localhost.local");
            //getAllCustomer(table);
            var update = returnCustomer(table,"USA","cust1@localhost.local");
            update.Name = "Customer1";
            updateCustomer(table, update)
            var delete = returnCustomer(table, "USA", "delete@localhost.local");
            deleteCustomer(table, delete);*/
            TableBatchOperation batch = new TableBatchOperation();
            var cus10 = new Customer("Cust10", "cust10@localhost.local");
            var cus11 = new Customer("Cust11", "cust11@localhost.local");
            var cus12 = new Customer("Cust12", "cust12@localhost.local");
            var cus13 = new Customer("Cust13", "cust13@localhost.local");
            var cus14 = new Customer("Cust14", "cust14@localhost.local");
            var cus15 = new Customer("Cust15", "cust15@localhost.local");
            batch.Insert(cus10);
            batch.Insert(cus11);
            batch.Insert(cus12);
            batch.Insert(cus13);
            batch.Insert(cus14);
            batch.Insert(cus15);
            table.ExecuteBatch(batch);
            getAllCustomer(table);
            Console.ReadKey();

        }
        /*
         * Inserting to storage table
         */
        static void createCustomer(CloudTable table, Customer customer)
        {
            TableOperation insert = TableOperation.Insert(customer);
            table.Execute(insert);
        }
        /*
         * Data retrieval for a single record from storage Table  
         */
         static void getCustomer(CloudTable table, string partitionKey, string rowKey)
        {
            TableOperation retrive = TableOperation.Retrieve<Customer>(partitionKey, rowKey);
            var res = table.Execute(retrive);
            Console.WriteLine(((Customer)res.Result).Name);

        }
        /*
         * Retrive all records from storage table
         */
         static void getAllCustomer(CloudTable table)
        {
            TableQuery<Customer> query = new TableQuery<Customer>().
                Where(TableQuery.GenerateFilterCondition("PartitionKey",QueryComparisons.Equal,"USA"));
            foreach(Customer customer in table.ExecuteQuery(query))
            {
                Console.WriteLine(customer.Name);

            }
        }
        /*
         * Return the customer
         */
        static Customer returnCustomer(CloudTable table, string partitionKey, string rowKey)
        {
            TableOperation retrive = TableOperation.Retrieve<Customer>(partitionKey, rowKey);
            var res = table.Execute(retrive);
            return (Customer)res.Result;

        }
        /*
         * Update Customer
         * 
         */
         static void updateCustomer(CloudTable table,Customer customer)
        {
            TableOperation update = TableOperation.Replace(customer);
            table.Execute(update);
        }
        /*
         * Delete Customer
         * 
         */
         static void deleteCustomer(CloudTable table,Customer customer)
        {
            TableOperation delete = TableOperation.Delete(customer);
            table.Execute(delete);

        }
         
    }
}

