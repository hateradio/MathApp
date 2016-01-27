using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MathApp.Controllers
{
    [Route("api/[controller]")]
    public class MathController : Controller
    {
        // GET: api/values
        // localhost:49546/api/math/subtract/3,2.3,13,32
        [HttpGet]
        public string Get()
        {
            return "use /add, /subtract, /multiply, /divide";
        }

        [HttpGet("add/{numbers}")]
        public double Sum(string numbers)
        {
            return Reduce(numbers, (acc, x) => acc + x);
        }

        [HttpGet("subtract/{numbers}")]
        public double subtract(string numbers)
        {
            return Reduce(numbers, (acc, x) => acc - x);
        }

        [HttpGet("multiply/{numbers}")]
        public double Multiply(string numbers)
        {
            return Reduce(numbers, (acc, x) => acc * x);
        }

        [HttpGet("divide/{numbers}")]
        public double Divide(string numbers)
        {
            return Reduce(numbers, (acc, x) => acc / x);
        }

        private Double Reduce(string numbers, Func<Double, Double, Double> func)
        {
            IEnumerable<Double> ns = numbers.Split(',').Select(x => Double.Parse(x));
            return ns.Skip(1).Aggregate(ns.First(), func);
        }

        [HttpGet("prime/{max}")]
        public IEnumerable<long> Prime(int max)
        {
            var nonprimes = new bool[max + 1];

            for (long i = 2; i <= max; i++)
            {
                if (nonprimes[i] == false)
                {
                    for (var j = i * i; j <= max; j += i)
                    {
                        nonprimes[j] = true;
                    }

                    yield return i;
                }
            }
        }

        // localhost:49546/api/math/fibonacci/50
        [HttpGet("fibonacci/{number}")]
        public Int64 Fibonacci(Int64 number)
        {
            return Fibonacci(number, 0, 1);
        }

        // Tail recursive fibonacci lookup
        // Eg:
        // F(0) = 0
        // F(1) = 1
        // F(2) = 1
        // F(5) = 5
        // F(10) = 55
        private Int64 Fibonacci(Int64 number, Int64 prev = 0, Int64 next = 1)
        {
            if (number == 0) return prev;
            if (number == 1) return next;
            else return Fibonacci(number - 1, next, prev + next);
        }

    }
}
