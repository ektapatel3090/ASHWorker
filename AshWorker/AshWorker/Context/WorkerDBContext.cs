using AshWorker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AshWorker.Context
{
    public class WorkerDBContext : DbContext
    {
        public WorkerDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Worker> Workers { get; set; }

        public DbSet<WorkerType> WorkerTypes { get; set; }
        public DbSet<SalaryType> SalaryType { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Worker>().HasData(
                new Worker() { Id = 1, FirstName = "John", LastName = "Doe", Address1 = "123 abc Dr", workerType = new WorkerType() { Id = 1, WorkerTypeDesc = "Manager" }, salaryType = new SalaryType() { Id = 1, SalaryTypeDesc = "Salaried" }, Salary = 30000, CanHaveExpenses = true, MaxExpenseAmount = 1000 },
                new Worker() { Id = 2, FirstName = "Jane", LastName = "Smith", Address1 = "567 pqr Dr", workerType = new WorkerType() { Id = 2, WorkerTypeDesc = "Supervisor" }, salaryType = new SalaryType() { Id = 1, SalaryTypeDesc = "Salaried" }, Salary = 20000, CanHaveExpenses = false, MaxExpenseAmount = null },
                new Worker() { Id = 3, FirstName = "Mark", LastName = "Simpson", Address1 = "890 xyz Dr", workerType = new WorkerType() { Id = 3, WorkerTypeDesc = "Employee" }, salaryType = new SalaryType() { Id = 2, SalaryTypeDesc = "Hourly" }, Salary = 50, CanHaveExpenses = false, MaxExpenseAmount = null });
        }
    }


}
