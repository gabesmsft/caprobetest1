﻿using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace BadPerf.Controllers
{
    public class BadPerfController : Controller
    {
        private readonly ILogger _logger;

        public BadPerfController(ILogger<BadPerfController> logger)
        {
            _logger = logger;
        }

        public IActionResult Slow()
        {
            _logger.LogInformation("Entering Slow Controller");
            Thread.Sleep(20000);
            _logger.LogInformation("Exiting Slow Controller");
            return View();
        }

        public IActionResult HighCPU()
        {
            _logger.LogInformation("Entering HighCPU Controller");
            long num = 300000000000;
            bool isPrime = true;
            for (int i = 0; i <= num; i++)
            {
                for (int j = 2; j <= num; j++)
                {
                    if (i != j && i % j == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                isPrime = true;
            }

            _logger.LogInformation("Exiting HighCPU Controller");

            return View();
        }

        public IActionResult HighMemory()
        {
            _logger.LogInformation("Entering HighMemory Controller");
			
            int[] intarry = new int[5000000000000];
            for (int i = 0; i < intarry.Length; i++)
            {
                intarry[i] = i;
            }

            _logger.LogInformation("Exiting HighMemory Controller");

            return View();
        }
    }
}
