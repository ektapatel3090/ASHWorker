using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AshWorker.Models;
using AshWorker.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace AshWorker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : ControllerBase
    {

        private readonly IWorkerRepository _workerRepository;

       public WorkerController(IWorkerRepository workerRepository)
        {
            _workerRepository = workerRepository;
        }


        [HttpGet]
        
        public async Task<IActionResult> GetWorkers()
        {
            List<Worker> list = new List<Worker>();

            try
            {
                list = await _workerRepository.GetWorkers();
            }
            catch (Exception ex)
            {
                var errorMessage = $"{nameof(WorkerController)}: Get Workers failed. {ex.GetType().Name}-{ex.Message} Stack trace: {ex.StackTrace}";
                Log.Error(errorMessage);
                throw;
            }
            if (list == null || list.Count == 0)
            {
                var detail = $"No Workers found.";
                var errorResponse = new ErrorResponse
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = detail
                };
                Log.Error($"{nameof(WorkerController)} --> Method {nameof(GetWorkers)}() " + detail);
                return StatusCode(StatusCodes.Status404NotFound, errorResponse);
            }
            Log.Information($"APIService.{nameof(WorkerController)}.{nameof(GetWorkers)} : Got all workers!");

            return Ok(list);
        }



        /// <summary>
        /// Creates Worker
        /// </summary>
        [HttpPost]

        public async Task<IActionResult> CreateWorker([FromBody]Worker request)
        {
            string paramDetails = $"{nameof(WorkerController)}.{nameof(CreateWorker)}().";
            Log.Information($"Begin. {paramDetails}");
            Worker worker = new Worker();

            try
            {
                
                var isValidRequest = true; //validate all the inputs 
                if (isValidRequest) 
                {
                    worker = await _workerRepository.CreateWorker(request);
                }
                else
                {
                    var error = $"One or more input parameters are invalid.";

                    var errorResponse = new ErrorResponse
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = error
                    };
                    Log.Error($"{nameof(WorkerController)}.{nameof(CreateWorker)} : " + error);
                    return StatusCode(StatusCodes.Status400BadRequest, errorResponse);
                }
            }
            catch (Exception ex)
            {
                var errorMessage = $"{paramDetails} : Create Worker failed. {ex.GetType().Name}-{ex.Message} Stack trace: {ex.StackTrace}";
                Log.Error(errorMessage);
                throw;
            }

            Log.Information($"{paramDetails} : Worker creation completed.");
            return Created("", worker);
        }
    }
}