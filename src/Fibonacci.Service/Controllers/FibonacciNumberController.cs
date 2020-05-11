using System;
using Fibonacci.Models;
using Fibonacci.Service.Services;
using Fibonacci.Validation;
using Infrastructure.BackgroundJob;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Fibonacci.Service.Controllers
{
	[ApiController]
	[Route("fibonacciSequences/{sequenceId}/numbers")]
	public class FibonacciNumberController : ControllerBase
	{
		private readonly IFibonacciNumberService numberService;
		private readonly IBackgroundJobDirector backgroundJobDirector;
		private readonly IFibonacciNumberValidator numberValidator;
		private readonly ILogger<FibonacciNumberController> logger;

		public FibonacciNumberController(
			IFibonacciNumberService numberService,
			IBackgroundJobDirector backgroundJobDirector,
			IFibonacciNumberValidator numberValidator,
			ILogger<FibonacciNumberController> logger)
		{
			this.numberService = numberService;
			this.backgroundJobDirector = backgroundJobDirector;
			this.numberValidator = numberValidator;
			this.logger = logger;
		}

		[HttpPost]
		public IActionResult Post(Guid sequenceId, [FromBody] FibonacciNumber number)
		{
			numberValidator.EnsureValid(number);
			
			backgroundJobDirector.Enqueue(() => numberService.PublishNextAsync(sequenceId, number));
			logger.LogInformation("Background calculation of the next number for sequence {sequenceId} is started", sequenceId);
			
			return Accepted();
		}
	}
}