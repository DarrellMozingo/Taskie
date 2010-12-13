using System;
using System.Linq;
using StandardApp.Services;
using Taskie;

namespace StandardApp.Tasks
{
	[TaskDescription("Checks for invalid credit cards.")]
	public class ProcessCustomersTask : ITask
	{
		private readonly ICustomerService _customerService;

		public ProcessCustomersTask(ICustomerService customerService)
		{
			_customerService = customerService;
		}

		public void Run()
		{
			var allCustomers = _customerService.GetAllCustomers();
			var invalidCustomers = allCustomers.Where(customer => customer.HasInvalidCreditCard());

			_customerService.Save(invalidCustomers);

			Console.WriteLine("Done processing customers.");
			Console.ReadLine();
		}
	}
}