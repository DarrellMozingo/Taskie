using System;
using System.Collections.Generic;
using AppUsingTaskieRunner.Domain;

namespace AppUsingTaskieRunner.Services
{
	public interface ICustomerService
	{
		IEnumerable<Customer> GetAllCustomers();
		void Save(IEnumerable<Customer> customers);
	}

	public class CustomerService : ICustomerService
	{
		public IEnumerable<Customer> GetAllCustomers()
		{
			Console.WriteLine("Loading customers...");

			return new[] { new Customer() };
		}

		public void Save(IEnumerable<Customer> customers)
		{
			Console.WriteLine("Saving customers...");
		}
	}
}