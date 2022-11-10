using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenMan.Web.Data;
using TenMan.Web.Data.Entities;
using TenMan.Web.Helpers;
using TenMan.Web.Models;

namespace TenMan.Web.Controllers
{
    public class CommitteesController : Controller
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IImageHelper _imageHelper;
        private readonly MailService _mailService;

        public CommitteesController(
            DataContext context,
            ICombosHelper combosHelper,
            IConverterHelper converterHelper,
            IImageHelper imageHelper,
            MailService mailService
            )
        {
            _context = context;
            _combosHelper = combosHelper;
            _converterHelper = converterHelper;
            _imageHelper = imageHelper;
            _mailService = mailService;
        }
        public async Task<IActionResult> AddCost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var committee = await _context.Committees.FindAsync(id);

            if (committee == null)
                return NotFound();

            var model = new AddCostViewModel
            {
                CommitteeId = committee.Id,
                Fields = _combosHelper.GetComboFields(),
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddCost(AddCostViewModel model)
        {
            if (ModelState.IsValid)
            {
                var cost = new Cost
                {
                    Description = model.Description,
                    Amount = model.Amount,
                    Field = _context.Fields.FindAsync(model.FieldId).Result,
                    Committee = _context.Committees.FindAsync(model.CommitteeId).Result
                };
                _context.Add(cost);
                await _context.SaveChangesAsync();
                return RedirectToAction($"IndexCosts/{model.CommitteeId}");
            }
            return View(model);
        }
        public IActionResult IndexCosts(int? id)
        {
            var committee = _context.Committees
                .Include(c => c.Costs)
                .ThenInclude(c => c.Field)
                .FirstOrDefault(c => c.Id == id);

            if (committee == null)
            {
                return NotFound();
            }

            ListCostsViewModel model = new ListCostsViewModel
            {
                Fields = _context.Fields,
                CommitteeId = committee.Id,
                Costs = committee.Costs
            };
            return View(model);
        }
        public async Task<IActionResult> AddUnit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var committee = await _context.Committees.FindAsync(id);

            if (committee == null)
                return NotFound();

            var model = new UnitViewModel
            {
                CommitteeId = committee.Id,
                Tenants = _combosHelper.GetComboTenants()
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddUnit(UnitViewModel model)
        {
            if (ModelState.IsValid)
            {
                CheckingAccount account = new CheckingAccount
                {
                    Number = model.CommitteeId + model.Id + "000" + model.Number,
                    PreviousBalance = 0,
                    Balance = 0
                };
                _context.CheckingAccounts.Add(account);
                var unit = new Unit
                {
                    Number = model.Number,
                    Floor = model.Floor,
                    Apartment = model.Apartment,
                    SquareMeters = model.SquareMeters,
                    Percentage = model.Percentage,
                    Owner = model.Owner,
                    CheckingAccount = account,
                    Committee = _context.Committees.FindAsync(model.CommitteeId).Result,
                };
                if (model.TenantId != 0)
                {
                    unit.Tenant = _context.Tenants.FindAsync(model.TenantId).Result;
                }
                _context.Add(unit);
                await _context.SaveChangesAsync();
                return RedirectToAction($"Details/{model.CommitteeId}");
            }
            return View(model);
        }
        public async Task<IActionResult> EditUnit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var unit = await _context.Units
                .Include(u => u.CheckingAccount)
            .Include(u => u.Tenant)
                        .ThenInclude(t => t.User)
            .Include(u => u.Committee)
            .Include(u => u.Requests)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (unit == null)
                return NotFound();

            var model = new UnitViewModel
            {
                Apartment = unit.Apartment,
                CheckingAccount = unit.CheckingAccount,
                CommitteeId = unit.Committee.Id,
                Committee = unit.Committee,
                Floor = unit.Floor,
                Number = unit.Number,
                Owner = unit.Owner,
                Percentage = unit.Percentage,
                Requests = unit.Requests,
                SquareMeters = unit.SquareMeters,
                TenantId = (unit.Tenant != null ? unit.Tenant.Id : 0),
                Tenants = _combosHelper.GetComboTenants()
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditUnit(UnitViewModel model)
        {
            if (ModelState.IsValid)
            {
                var unit = new Unit
                {
                    Id = model.Id,
                    Apartment = model.Apartment,
                    CheckingAccount = model.CheckingAccount,
                    Committee = model.Committee,
                    Floor = model.Floor,
                    Number = model.Number,
                    Owner = model.Owner,
                    Percentage = model.Percentage,
                    Tenant = await _context.Tenants.FindAsync(model.TenantId),
                    Requests = model.Requests,
                    SquareMeters = model.SquareMeters
                };

                _context.Units.Update(unit);
                await _context.SaveChangesAsync();

                return RedirectToAction($"Details/{model.CommitteeId}");
            }
            return View(model);
        }
        public async Task<IActionResult> DetailsUnit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unit = await _context.Units
                .Include(u => u.Requests)
                .ThenInclude(r => r.Images)
                .Include(u => u.Requests)
                .ThenInclude(r => r.Speciality)
                .Include(u => u.Requests)
                .ThenInclude(r => r.Worker)
                .ThenInclude(w => w.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (unit == null)
            {
                return NotFound();
            }

            return View(unit);
        }
        public async Task<IActionResult> AddRequest(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var unit = await _context.Units
                .Include(u => u.Requests)
                .ThenInclude(r => r.Statuses)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (unit == null)
                return NotFound();

            var model = new RequestViewModel
            {
                UnitId = unit.Id,
                Specialties = _combosHelper.GetComboSpecialties(),
                StatusTypes = _combosHelper.GetComboStatusTypes(),
                Workers = _combosHelper.GetComboWorkers(),
                StartDate = DateTime.Today.ToUniversalTime(),
                Remarks = "test"
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddRequest(RequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                var request = await _converterHelper.ToRequestAsync(model, true);
                _context.Requests.Add(request);
                await _context.SaveChangesAsync();
                return RedirectToAction($"DetailsUnit/{model.UnitId}");
            }
            return View(model);
        }

        public async Task<IActionResult> EditRequest(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var request = await _context.Requests
            .Include(r => r.Unit)
            .Include(r => r.Images)
            .Include(r => r.Worker)
            .ThenInclude(w => w.User)
            .Include(r => r.Statuses)
            .ThenInclude(s => s.StatusType)
            .Include(r => r.Speciality)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (request == null)
                return NotFound();

            var model = _converterHelper.ToRequestViewModel(request);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditRequest(RequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                var actualStatus = _context.StatusTypes.FirstOrDefault(st => st.Id == model.StatusTypeId);
                model.ActualStatus = actualStatus.Description;

                var request = await _converterHelper.ToRequestAsync(model, false);

                if (model.ActualStatus == "Finalizada")
                {
                    model.EndDate = DateTime.Now;
                }
                Status status = new Status
                {
                    Date = DateTime.Now,
                    Request = request,
                    StatusType = actualStatus
                };

                request.Statuses.Add(status);

                _context.Requests.Update(request);
                await _context.SaveChangesAsync();

                //Envio de email

                _mailService.SendEmailGmail(request.Worker.User.Email, "TenMan - Nuevo trabajo asignado", "Ha sido asignado para resolver una solicitud");

                return RedirectToAction($"DetailsUnit/{model.UnitId}");
            }
            return View(model);
        }
        public async Task<IActionResult> DetailsRequest(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var request = await _context.Requests
            .Include(r => r.Unit)
            .ThenInclude(u => u.Tenant)
            .ThenInclude(t => t.User)
            .Include(r => r.Unit)
            .ThenInclude(u => u.Committee)
            .ThenInclude(c => c.Administrator)
            .ThenInclude(a => a.User)
            .Include(r => r.Images)
            .Include(r => r.Worker)
            .ThenInclude(w => w.User)
            .Include(r => r.Statuses)
            .ThenInclude(s => s.StatusType)
            .Include(r => r.Speciality)
            .FirstOrDefaultAsync(m => m.Id == id);

            if (request == null)
                return NotFound();

            return View(request);
        }
        public async Task<IActionResult> AddImage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var request = await _context.Requests.FindAsync(id.Value);
            if (request == null)
            {
                return NotFound();
            }

            var model = new RequestImageViewModel
            {
                Id = request.Id
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddImage(RequestImageViewModel model)
        {
            if (ModelState.IsValid)
            {
                var path = string.Empty;

                if (model.ImageFile != null)
                {
                    path = await _imageHelper.UploadImageAsync(model.ImageFile);
                }

                var requestImage = new RequestImage
                {
                    ImageUrl = path,
                    Request = await _context.Requests.FindAsync(model.Id)
                };

                _context.RequestImages.Add(requestImage);
                await _context.SaveChangesAsync();
                return RedirectToAction($"{nameof(DetailsRequest)}/{model.Id}");
            }

            return View(model);
        }

        // GET: Committees
        public async Task<IActionResult> Index()
        {
            return View(await _context.Committees.ToListAsync());
        }

        // GET: Committees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var committee = await _context.Committees
                .Include(c => c.Units)
                .ThenInclude(u => u.Requests)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (committee == null)
            {
                return NotFound();
            }

            return View(committee);
        }

        // GET: Committees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Committees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,Neighborhood,Address,Price")] Committee committee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(committee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(committee);
        }

        // GET: Committees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var committee = await _context.Committees.FindAsync(id);
            if (committee == null)
            {
                return NotFound();
            }
            return View(committee);
        }

        // POST: Committees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,Neighborhood,Address,Price")] Committee committee)
        {
            if (id != committee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(committee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommitteeExists(committee.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(committee);
        }

        // GET: Committees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var committee = await _context.Committees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (committee == null)
            {
                return NotFound();
            }

            return View(committee);
        }

        // POST: Committees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var committee = await _context.Committees.FindAsync(id);
            _context.Committees.Remove(committee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommitteeExists(int id)
        {
            return _context.Committees.Any(e => e.Id == id);
        }
        public ActionResult CalculateExpenses(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var committee = _context.Committees
                .Include(c => c.Units)
                .ThenInclude(u => u.CheckingAccount)
                .Include(c => c.Costs)
                .ThenInclude(c => c.Field)
                .Include(c => c.Administrator)
                .ThenInclude(a => a.User)
                .FirstOrDefault(c => c.Id == id);

            if (committee == null)
            {
                return NotFound();
            }
            List<UnitDescriptionLine> unitDescriptionLines = new List<UnitDescriptionLine>();
            List<ExpensesCost> expensesCosts = new List<ExpensesCost>();

            foreach (Cost cost in committee.Costs)
            {
                ExpensesCost ec = new ExpensesCost
                {
                    Amount = cost.Amount,
                    Description = cost.Description,
                    Field = cost.Field,
                };
                expensesCosts.Add(ec);
            }
            Expenses exp = new Expenses
            {
                CommitteeId = committee.Id,
                Committee = committee,
                ExpensesCosts = expensesCosts,
                Month = DateTime.Now.Month,
                Year = DateTime.Now.Year,
                Date = DateTime.Now.ToUniversalTime(),
                Fields = _context.Fields,
                //Notes = new List<string>()
            };
            decimal total = exp.GetTotal();

            foreach (Unit unit in committee.Units)
            {
                decimal expA = ((decimal)unit.Percentage / 100) * total;
                decimal prev = unit.CheckingAccount.Total;
                decimal payment = unit.CheckingAccount.YourPayment;
                decimal pending = unit.CheckingAccount.Total - payment;
                decimal balance = expA + pending;               

                UnitDescriptionLine unitLine = new UnitDescriptionLine
                {
                    Unit = unit,
                    YourPayment = payment,
                    PreviousBalance = prev,
                    PendingBalance = pending,
                    NewUnitTotal = balance,
                    ExpA = expA,
                    Interest = 0
                };
                unitDescriptionLines.Add(unitLine);
                //unit.CheckingAccount.PreviousBalance = prev + unit.CheckingAccount.Balance;
                //unit.CheckingAccount.Balance = balance;
            }
            exp.UnitDescriptionLines = unitDescriptionLines;
            return View(exp);
        }

        [HttpPost]
        public ActionResult CalculateExpenses(Expenses model, List<UnitDescriptionLine> unitLines)
        {
            if (ModelState.IsValid)
            {
                var committee = _context.Committees
                  .Include(c => c.Units)
                  .ThenInclude(u => u.CheckingAccount)
                  .Include(c => c.Costs)
                  .ThenInclude(c => c.Field)
                  .Include(c => c.Administrator)
                  .ThenInclude(a => a.User)
                  .FirstOrDefault(c => c.Id == model.CommitteeId);

                // La razón de crear nuevos costos para expensas es que al querer guardar la expensa en la base de datos,
                // Trata de crear tambien los costos asociados que ya existen.
                List<ExpensesCost> expensesCosts = new List<ExpensesCost>();

                foreach (Cost cost in committee.Costs)
                {
                    ExpensesCost ec = new ExpensesCost
                    {
                        Amount = cost.Amount,
                        Description = cost.Description,
                        Field = cost.Field,
                    };
                    expensesCosts.Add(ec);
                }

                model.ExpensesCosts = expensesCosts;
                decimal total = model.GetTotal();

                List<UnitDescriptionLine> unitDescriptionLines = new List<UnitDescriptionLine>();

                foreach (Unit unit in committee.Units)
                {
                    decimal expA = ((decimal)unit.Percentage / 100) * total;
                    decimal payment = unit.CheckingAccount.YourPayment;
                    decimal pending = unit.CheckingAccount.Total - payment;
                    decimal balance = expA + pending;
                    decimal prev = unit.CheckingAccount.Total;

                    UnitDescriptionLine unitLine = new UnitDescriptionLine
                    {
                        Unit = unit,
                        YourPayment = payment,
                        PreviousBalance = prev,
                        PendingBalance = pending,
                        ExpA = expA,
                        NewUnitTotal = balance,
                        Interest = 0
                    };
                    unitDescriptionLines.Add(unitLine);
                    unit.CheckingAccount.PreviousBalance = prev;
                    unit.CheckingAccount.YourPayment = 0;
                    unit.CheckingAccount.PendingBalance = pending;
                    unit.CheckingAccount.Balance = balance;
                }
                model.UnitDescriptionLines = unitDescriptionLines;
                model.Id = 0;
                _context.Expenses.Add(model);
                _context.SaveChanges();
                return RedirectToAction($"IndexExpenses/{model.CommitteeId}");
            }
            return View(model);
        }

        public IActionResult IndexExpenses(int? id)
        {
            var committee = _context.Committees
                .Include(c => c.Costs)
                .ThenInclude(c => c.Field)
                .Include(c => c.Administrator)
                .ThenInclude(a => a.User)
                .Include(c => c.Expenses)
                .ThenInclude(e => e.ExpensesCosts)
                .FirstOrDefault(c => c.Id == id);

            if (committee == null)
            {
                return NotFound();
            }
            IEnumerable<Expenses> expenses = _context.Expenses.Where(e => e.CommitteeId == committee.Id);
            return View(expenses);
        }
        //public void CalculateExpenses(int? id)
        //{
        //    if (id == null)
        //    {
        //        return;
        //    }
        //    var committee = _context.Committees
        //        .Include(c => c.Units)
        //        .ThenInclude(u => u.CheckingAccount)
        //        .Include(c => c.Costs)
        //        .ThenInclude(c => c.Field)
        //        .Include(c => c.Administrator)
        //        .ThenInclude(a => a.User)
        //        .FirstOrDefault();

        //    if (committee != null)
        //    {
        //        Expenses exp = new Expenses
        //        {
        //            Committee = committee,
        //            Costs = committee.Costs,
        //            Month
        //        };
        //        foreach (Unit unit in committee.Units)
        //        {
        //            decimal balance = unit.SquareMeters * committee.Price;
        //            decimal prev = unit.CheckingAccount.PreviousBalance;

        //            unit.CheckingAccount.PreviousBalance = prev + unit.CheckingAccount.Balance;
        //            unit.CheckingAccount.Balance = balance;
        //        }
        //        _context.Committees.Update(committee);
        //        _context.SaveChanges();
        //        RedirectToAction($"Details/{id}");
        //    }
        //}
        public void CalculateCosts(int? id)
        {
            if (id == null)
            {
                return;
            }
            var committee = _context.Committees
                .Include(c => c.Units)
                .ThenInclude(u => u.CheckingAccount)
                .Include(c => c.Costs)
                .ThenInclude(c => c.Field)
                .FirstOrDefault();

            if (committee != null)
            {
                foreach (Unit unit in committee.Units)
                {
                    decimal balance = unit.SquareMeters * committee.Price;
                    decimal prev = unit.CheckingAccount.PreviousBalance;

                    unit.CheckingAccount.PreviousBalance = prev + unit.CheckingAccount.Balance;
                    unit.CheckingAccount.Balance = balance;
                }
                _context.Committees.Update(committee);
                _context.SaveChanges();
                RedirectToAction($"Details/{id}");
            }
        }
        public async Task<IActionResult> SendExpensesEmail(int? id)
        {
            var expenses = await _context.Expenses
               .Include(e => e.ExpensesCosts)
               .Include(e => e.UnitDescriptionLines)
               .Include(e => e.Committee)
               .ThenInclude(c => c.Units)
               .ThenInclude(u => u.Tenant)
               .ThenInclude(t => t.User)
               .FirstOrDefaultAsync(e => e.Id == id);

            if (expenses == null)
                return NotFound();

            foreach (Unit unit in expenses.Committee.Units)
            {
                var email = unit.Tenant.User.Email;

            }

            return View();
        }
        //protected void ExportExcel()
        //{
        //    Response.Clear();
        //    Response.Buffer = true;
        //    string FileName = "ReporteColectoras" + DateTime.Now + ".xls";
        //    Response.AddHeader("content-disposition", "attachment;filename=" + FileName);
        //    Response.Charset = "";
        //    Response.ContentType = "application/vnd.ms-excel";

        //    StringWriter sw = new StringWriter();
        //    HtmlTextWriter HW = new HtmlTextWriter(sw);


        //    // Read Style file (css) here and add to response 
        //    FileInfo fi = new FileInfo(Server.MapPath("~/css/estilos-grid3.css"));
        //    StringBuilder sb = new StringBuilder();
        //    StreamReader sr = fi.OpenText();
        //    while (sr.Peek() >= 0)
        //    {
        //        sb.Append(sr.ReadLine());
        //    }
        //    sr.Close();
        //    lvColectora.RenderControl(HW);
        //    //GdColectora.RenderControl(HW);
        //    Response.Write("<html><head><style type='text/css'>" + sb.ToString() + "</style></head><body>" + sw.ToString() + "</body></html>");
        //    Response.Flush();
        //    Response.Close();
        //    Response.End();
        //}
        public async Task<IActionResult> ViewExpenses(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenses = await _context.Expenses
                .Include(e => e.ExpensesCosts)
                .Include(e => e.UnitDescriptionLines)
                .Include(e => e.Committee)
                .FirstOrDefaultAsync(e => e.Id == id);

            var committee = _context.Committees
                .Include(c => c.Administrator)
                .ThenInclude(a => a.User)
                .Include(c => c.Units)
                .ThenInclude(u => u.CheckingAccount)
                .Include(c => c.Costs)
                .ThenInclude(c => c.Field)
                .FirstOrDefault();

            expenses.Fields = _context.Fields;


            if (committee == null)
            {
                return NotFound();
            }
            return View(expenses);
        }
        //public async Task<IActionResult> ViewExpenses(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var committee = await _context.Committees
        //        .Include(c => c.Units)
        //        .ThenInclude(u => u.Tenant)
        //        .ThenInclude(t => t.User)
        //        .Include(c => c.Units)
        //        .ThenInclude(u => u.CheckingAccount)
        //        .Include(c => c.Administrator)
        //        .ThenInclude(a => a.User)
        //        .Include(c => c.Costs)
        //        .FirstOrDefaultAsync(c => c.Id == id);

        //    if (committee == null)
        //    {
        //        return NotFound();
        //    }
        //    ExpensesViewModel model = new ExpensesViewModel
        //    {
        //        Committee = committee,
        //        Fields = _context.Fields
        //    };
        //    return View(model);
        //}
        //private async Task<IActionResult> CalculateCosts(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //    var committee = await _context.Committees.Find(id);

        //    if (committee != null)
        //    {
        //        foreach (Unit unit in committee.Units)
        //        {
        //            decimal balance = unit.SquareMeters * committee.Price;
        //            decimal prev = unit.CheckingAccount.PreviousBalance;

        //            unit.CheckingAccount.PreviousBalance = prev + unit.CheckingAccount.Balance;
        //            unit.CheckingAccount.Balance = balance;
        //        }
        //        _context.Committees.Update(committee);
        //        _context.SaveChanges();
        //        //RedirectToAction($"Details/{id}");
        //    }
        //}
    }
}
