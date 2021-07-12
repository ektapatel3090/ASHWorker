using AshWorker.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AshWorker.Models
{
    public class Worker
    {

        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }


        [StringLength(100)]
        public string Address1 { get; set; }


        public SalaryType salaryType { get; set; }

        public WorkerType workerType { get; set; }

        [Required]
        [RegularExpression(@"^\d+\.\d{0,2}$")]
        [Range(0, 999.99)]
        public decimal Salary { get; set; }

        [Required]
        public bool CanHaveExpenses { get; set; }

        [Required]
        [RegularExpression(@"^\d+\.\d{0,2}$")]
        [Range(0, 99999999.99)]
        public decimal? MaxExpenseAmount { get; set; }
    }
}
