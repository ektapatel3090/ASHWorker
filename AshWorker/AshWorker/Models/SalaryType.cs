﻿using AshWorker.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AshWorker.Models
{
    public class SalaryType
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string SalaryTypeDesc { get; set; }

    }
}
