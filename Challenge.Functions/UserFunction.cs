using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Service.Interfaces;
using Data.Model;
using System.Collections.Generic;

namespace Challenge.Functions
{
    public class UserFunction
    {
        private readonly IUserService _userService;

        public UserFunction(IUserService userService)
        {
            _userService = userService;
        }

        [FunctionName("UserFunction")]
        public async Task<ActionResult<List<User>>> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {

            string responseMessage = string.Empty;
            log.LogInformation("Request triggered to fetch users.");

            try
            {
                var users = await _userService.GetUsersAsync();
                log.LogInformation($"Successfully retrieved {users.Count} users.");

                return new OkObjectResult(users);
            }
            catch (Exception ex)
            {
                log.LogError($"Error in function execution: {ex.Message}");
                responseMessage = $"Error in function execution";

                return new OkObjectResult(responseMessage);
            }
        }
    }
}
