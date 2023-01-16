using app_testlar.Data;
using app_testlar.Models;
using app_testlar.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace app_testlar.Controllers
{
    public class employescontroler : Controller
    {
        private readonly mvcdb mvcdb;

        public employescontroler(mvcdb mvcdb)
        {
            this.mvcdb = mvcdb;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await mvcdb.employes.ToListAsync();
            return View(employees);
        }

        [HttpGet]
        public async Task<IActionResult> bebas()
        {
            var employees = await mvcdb.employes.ToListAsync();
            return View(employees);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(addemployeviewmodel addemployeRequest)
        {
            var employe = new employe()
            {
                Id = Guid.NewGuid(),
                Name = addemployeRequest.Name,
                Email = addemployeRequest.Email,
                Salary = addemployeRequest.Salary,
                Total = addemployeRequest.Total,
            };

            await mvcdb.employes.AddAsync(employe);
            await mvcdb.SaveChangesAsync();
            return RedirectToAction("Add");

        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var employee = await mvcdb.employes.FirstOrDefaultAsync(x => x.Id == id);
            if (employee != null)
            {
                var viewModel = new UpdateEmployeViewModel()
                {
                    Id = Guid.NewGuid(),
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    Total = employee.Total
                };
                return await Task.Run(()=> View("View",viewModel));
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeViewModel model)
        {
            var employee = await mvcdb.employes.FindAsync(model.Id);
            if (employee != null)
            {
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Salary = model.Salary;
                employee.Total = model.Total;

                await mvcdb.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete (UpdateEmployeViewModel model)
        {
            var employee = await mvcdb.employes.FindAsync(model.Id);
            if (employee != null)
            {
                mvcdb.employes.Remove(employee);
                await mvcdb.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
