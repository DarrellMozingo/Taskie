using System.Collections.Generic;
using StandardSampleApplication.Domain;

namespace StandardSampleApplication.Services
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
			yield return new Customer();
			yield return new Customer();
			yield return new Customer();
			yield return new Customer();
		}

		public void Save(IEnumerable<Customer> customers)
		{
		}
	}
}