using AspNetCore.ReportingServices.ReportProcessing.ReportObjectModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TenMan.Web.Data;
using TenMan.Web.Data.Entities;
using TenMan.Web.Data.Entities.Settings;
using TenMan.Web.Helpers;
using TenMan.Web.Models;
using TenMan.Web.Models.Settings;
using TenMan.Web.Services;
using TenMan.Web.Services.ExpensesServices;
using Field = TenMan.Web.Data.Entities.Field;

namespace TenMan.Web.Controllers
{
    public class CommitteesController : Controller
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IImageHelper _imageHelper;
        private readonly IDefaultFieldsHelper _defaultFieldsHelper;
        private readonly IDefaultCategoriesHelper _defaultCategoriesHelper;
        private readonly IExpenseSummaryBuilder _expenseSummaryBuilder;
        private readonly IViewRenderService _viewRenderService;
        private readonly MailService _mailService;

        public CommitteesController(
            DataContext context,
            ICombosHelper combosHelper,
            IConverterHelper converterHelper,
            IImageHelper imageHelper,
            IDefaultFieldsHelper defaultFieldsHelper,
            IDefaultCategoriesHelper defaultCategoriesHelper,
            IExpenseSummaryBuilder expenseSummaryBuilder,
            IViewRenderService viewRenderService,
            MailService mailService
            )
        {
            _context = context;
            _combosHelper = combosHelper;
            _converterHelper = converterHelper;
            _imageHelper = imageHelper;
            _defaultCategoriesHelper = defaultCategoriesHelper;
            _defaultFieldsHelper = defaultFieldsHelper;
            _expenseSummaryBuilder = expenseSummaryBuilder;
            _viewRenderService = viewRenderService;
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
                var cost = new FixedCost
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
        /*
         public IActionResult IndexCosts(int? id)
        {
            var committee = _context.Committees
                .Include(c => c.Expenses)
                .ThenInclude(e => e.ExpensesCosts)
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
                Costs = committee.Expenses.
            };
            return View(model);
        }*/
        public async Task<IActionResult> AddExpenseCategory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var committee = await _context.Committees.FindAsync(id);

            if (committee == null)
                return NotFound();

            var model = new CategoryViewModel
            {
                CommitteeId = committee.Id,
                Letras = new List<string> { "A", "B", "C", "D" }
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddExpenseCategory(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var category = new Category
                {
                    Letra = model.Letra,
                    Description = model.Description,
                    Committee = _context.Committees.FindAsync(model.CommitteeId).Result,
                };
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction($"Details/{model.CommitteeId}");
            }
            return View(model);
        }

        public async Task<IActionResult> AddField(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var committee = await _context.Committees.FindAsync(id);

            if (committee == null)
                return NotFound();

            var model = new Field
            {
                CommitteeId = committee.Id,
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddField(Field model)
        {
            if (ModelState.IsValid)
            {
                var field = new Field
                {
                    Number = model.Number,
                    Description = model.Description,
                    Committee = _context.Committees.FindAsync(model.CommitteeId).Result
                };
                _context.Add(field);
                await _context.SaveChangesAsync();
                return RedirectToAction($"Details/{model.CommitteeId}");
            }
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

            var categories = _context.Categories
           .Where(cat => cat.Committee.Id == committee.Id)
           .ToList();

            List<CategoryPercent> catPercents = new List<CategoryPercent>();

            foreach (var cat in committee.Categories)
            {
                CategoryPercent cp = new CategoryPercent();
                cp.Category = cat;
                cp.CategoryId = cat.Id;
                cp.Percent = 0;
                catPercents.Add(cp);
            }

            var model = new UnitViewModel
            {
                CommitteeId = committee.Id,
                Tenants = _combosHelper.GetComboTenants(),
                Categories = categories,
                CategoriesPercents = catPercents
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
                    Percentage = model.Percentage, // Esto debería ser una lista 
                    CategoriesPercents = new List<CategoryPercent>(),
                    Owner = model.Owner,
                    CheckingAccount = account,
                    Committee = _context.Committees.FindAsync(model.CommitteeId).Result,
                };

                if (model.TenantId != 0)
                {
                    unit.Tenant = _context.Tenants.FindAsync(model.TenantId).Result;
                }
                foreach (var item in model.CategoriesPercents)
                {
                    if (item.Percent.HasValue && item.Percent > 0)
                    {
                        unit.CategoriesPercents.Add(new CategoryPercent
                        {
                            CategoryId = item.CategoryId,
                            Percent = item.Percent.Value
                        });
                    }
                }
                _context.Add(unit);
                await _context.SaveChangesAsync();
                return RedirectToAction($"Details/{model.CommitteeId}");
            }
            return View(model);
        }

        public async Task<IActionResult> Balance(int? id)
        {
            var committee = _context.Committees
         .Where(c => c.Id == id)
         .Select(c => new BalanceIndexViewModel
         {
             CommitteeName = c.Description,
             UnitsBalances = c.Units.Select(u => new BalanceViewModel
             {
                 UnitId = u.Id,
                 UnitNumber = u.Number,
                 OwnerName = u.Owner,

                 AccountNumber = u.CheckingAccount.Number,
                 PreviousBalance = u.CheckingAccount.PreviousBalance,
                 YourPayment = u.CheckingAccount.YourPayment,
                 PendingBalance = u.CheckingAccount.PendingBalance,
                 Balance = u.CheckingAccount.Balance
             }).ToList()
         })
         .FirstOrDefault();

            if (committee == null)
                return NotFound();

            return View(committee);
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
            .Include(u => u.Committee.Categories)
            .Include(u => u.CategoriesPercents)
            .ThenInclude(cp => cp.Category)

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
                Percentage = unit.Percentage, //Esto debería ser una lista
                CategoriesPercents = unit.CategoriesPercents,
                Categories = unit.Committee.Categories,
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
                var unit = await _context.Units
                .Include(u => u.CheckingAccount)
                .Include(u => u.Tenant)
                    .ThenInclude(t => t.User)
                .Include(u => u.Committee)
                .Include(u => u.Requests)
                .Include(u => u.Committee.Categories)
            .Include(u => u.CategoriesPercents)
                    .ThenInclude(cp => cp.Category)
               .FirstOrDefaultAsync(u => u.Id == model.Id);

                foreach (var cat in model.CategoriesPercents)
                {
                    var cp = unit.CategoriesPercents
                        .FirstOrDefault(x => x.CategoryId == cat.CategoryId);

                    if (cp == null)
                    {
                        unit.CategoriesPercents.Add(new CategoryPercent
                        {
                            CategoryId = cat.CategoryId,
                            Percent = cat.Percent
                        });
                    }
                    else
                    {
                        cp.Percent = cat.Percent;
                    }
                }

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

                _mailService.SendEmailGmail(request.Worker.User.Email, "TenMan - Nuevo trabajo asignado", "Ha sido asignado para resolver una solicitud", "");

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
            string name = User.Identity.Name;
            //int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (User.IsInRole("Administrator"))
            {
                return View(await _context.Committees
                    .Where(c => c.Administrator.User.UserName == name)
                    .ToListAsync());
            }
            else if (User.IsInRole("SuperAdmin"))
            {
                return View(await _context.Committees
                    .ToListAsync());
            }
            return Forbid();
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
                .Include(c => c.Fields)
                .Include(c => c.Categories)
                .Include(c => c.Administrator)
                .ThenInclude(a => a.User)
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
            CommitteeViewModel model = new CommitteeViewModel
            {
                Administrators = _combosHelper.GetComboAdministrators(),
            };
            return View(model);
        }

        // POST: Committees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CommitteeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                var administrator = new Administrator();

                if (User.IsInRole("SuperAdmin"))
                    administrator = await _context.Administrators.FindAsync(model.AdministratorId);

                else if (User.IsInRole("Administrador"))
                {
                    administrator = await _context.Administrators.FindAsync(userId);
                }

                Committee committee = new Committee
                {
                    Fields = _defaultFieldsHelper.GetRubrosDefault(),
                    Categories = _defaultCategoriesHelper.GetCategoriesDefault(),
                    Address = model.Address,
                    Administrator = administrator,
                    CUIT = model.CUIT,
                    Description = model.Description,
                    Neighborhood = model.Neighborhood,
                    SuterhKey = model.SuterhKey
                };
                _context.Add(committee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
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
        public ActionResult ExpenseSummary(int id)
        {
            // Muestra el resumen actual, todavía no se calculó nada.
            var esvm = _expenseSummaryBuilder.Build(id); //Cambiar todo por la id del consorcio y luego llamar a la expensa que esta abierta o esto de calcular expensas ponerlo en el index de Expensas.
            //GenerateSettlement(esvm); // Deberiamos llamar este metodo cuando se cierra la expensa
            return View(esvm);
        }
        [HttpPost]
        public void CalculateExpenses(int expenseId)
        {
            var expense = _context.Expenses
                .Include(e => e.ExpensesCosts)
                .Include(e => e.Committee)
                    .ThenInclude(c => c.Units)
                        .ThenInclude(u => u.CategoriesPercents)
                .Include(e => e.Committee)
                    .ThenInclude(c => c.Units)
                        .ThenInclude(u => u.CheckingAccount)
                .FirstOrDefault(e => e.Id == expenseId);

            if (expense == null)
                throw new Exception("Expensa no encontrada");

            // Totales por categoría
            var totalsByCategory = expense.ExpensesCosts
                .GroupBy(ec => ec.CategoryId)
                .Select(g => new
                {
                    CategoryId = g.Key,
                    Total = g.Sum(x => x.Amount)
                })
                .ToList();

            // Recorrer unidades
            foreach (var unit in expense.Committee.Units)
            {
                decimal totalForUnit = 0;

                foreach (var categoryTotal in totalsByCategory)
                {
                    var percent = unit.CategoriesPercents
                        .FirstOrDefault(cp => cp.CategoryId == categoryTotal.CategoryId);

                    if (percent != null)
                    {
                        var amount = categoryTotal.Total * ((decimal)percent.Percent / 100m);
                        totalForUnit += amount;
                    }
                }

                // Actualizar cuenta corriente
                var account = unit.CheckingAccount;

                account.PreviousBalance = account.Balance;
                account.PendingBalance += totalForUnit;
                account.Balance = account.PreviousBalance + account.PendingBalance;
            }

            ExpenseSummaryViewModel esvm = new ExpenseSummaryViewModel {
                Expense = expense,
                Committee = expense.Committee,
            };
            GenerateSettlement(esvm);
            _context.SaveChanges();
        }
        // Guardar Snapshot HTML de la expensa 
        public async Task GenerateSettlement(ExpenseSummaryViewModel viewModel)
        {

            var html = await _viewRenderService.RenderToStringAsync(
                "Expenses/SettlementTemplate",
                viewModel);

            var period = new ExpensePeriod
            {
                CommitteeId = viewModel.Committee.Id,
                Year = viewModel.Expense.Year,
                Month = viewModel.Expense.Month,
                Date = viewModel.Expense.Date,
                Current = false,
                HtmlSnapshot = html
            };

            _context.ExpensesPeriods.Add(period);
            await _context.SaveChangesAsync();
        }
        /*
        public ActionResult CalculateExpenses(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var committee = _context.Committees
                .Include(c => c.Units)
                .ThenInclude(u => u.CheckingAccount)
                .Include(c => c.Fields)
                .Include(c => c.Expenses)
                .ThenInclude(c => c.ExpensesCosts)
                .Include(c => c.Administrator)
                .ThenInclude(a => a.User)
                .FirstOrDefault(c => c.Id == id);

            if (committee == null)
            {
                return NotFound();
            }
            List<UnitDescriptionLine> unitDescriptionLines = new List<UnitDescriptionLine>();
            List<ExpensesCost> expensesCosts = new List<ExpensesCost>();

            foreach (FixedCost cost in committee.FixedCosts)
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
                  .Include(c => c.Fields)
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
        }*/
        public IActionResult IndexExpenses(int? id)
        {
            var committee = _context.Committees
                .Include(c => c.Fields)
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
        /*
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
        }*/

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
                .Include(c => c.Fields)
                .Include(c => c.Expenses)
                .FirstOrDefault();

            expenses.Fields = _context.Fields;

            if (committee == null)
            {
                return NotFound();
            }
            return View(expenses);
        }
        public IActionResult ExpenseSummarySettings(int id)
        {
            var settings = _context.ExpenseSummarySettings
                .FirstOrDefault(s => s.CommitteeId == id);

            var vm = settings == null
                ? new ExpenseSummarySettingsViewModel
                {
                    CommitteeId = id,
                    HeaderAlignment = "Center",
                    ShowCategoryTotals = true,
                    ShowFieldTotals = true,
                    ShowUnitDetail = true,
                    ShowFixedCostsDetail = true
                }
                : new ExpenseSummarySettingsViewModel
                {
                    CommitteeId = settings.CommitteeId,
                    //HeaderAlignment = settings.HeaderAlignment,
                    //ShowCategoryTotals = settings.ShowCategoryTotals,
                    //ShowFieldTotals = settings.ShowFieldTotals,
                    //ShowUnitDetail = settings.ShowUnitDetail,
                    //ShowFixedCostsDetail = settings.ShowFixedCostsDetail
                };

            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveSummarySettings(ExpenseSummarySettingsViewModel model)
        {
            if (!ModelState.IsValid)
                return View("ExpenseSummarySettings", model);

            var settings = _context.ExpenseSummarySettings
                .FirstOrDefault(s => s.CommitteeId == model.CommitteeId);

            if (settings == null)
            {
                settings = new ExpenseSummarySettings
                {
                    CommitteeId = model.CommitteeId
                };

                _context.ExpenseSummarySettings.Add(settings);
            }

            /*
            settings.HeaderAlignment = model.HeaderAlignment;
            settings.ShowCategoryTotals = model.ShowCategoryTotals;
            settings.ShowFieldTotals = model.ShowFieldTotals;
            settings.ShowUnitDetail = model.ShowUnitDetail;
            settings.ShowFixedCostsDetail = model.ShowFixedCostsDetail;*/

            _context.SaveChanges();

            return RedirectToAction("Details", new { id = model.CommitteeId });
        }
        public async Task<IActionResult> ExportPDF(int? id)
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
                .Include(c => c.Fields)
                .Include(c => c.Expenses)
                .FirstOrDefault();

            expenses.Fields = _context.Fields;

            if (committee == null)
            {
                return NotFound();
            }
            return new Rotativa.AspNetCore.ViewAsPdf("ViewExpenses", expenses)
            {
                FileName = "Expensas" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".pdf"
            };
        }
        public async Task<IActionResult> ExportPDFForMail(int? id, string pdfFile)
        {
            if (id == null)
            {
                return NotFound();
            }
            var expenses = await _context.Expenses
               .Include(e => e.ExpensesCosts)
               .Include(e => e.UnitDescriptionLines)
               .Include(e => e.Committee)
               .ThenInclude(c => c.Units)
               .ThenInclude(u => u.Tenant)
               .ThenInclude(t => t.User)
               .FirstOrDefaultAsync(e => e.Id == id);

            var committee = await _context.Committees
                .Include(c => c.Administrator)
                .ThenInclude(a => a.User)
                .Include(c => c.Units)
                .ThenInclude(u => u.CheckingAccount)
                .Include(c => c.Fields)
                .Include(c => c.Expenses)
                .FirstOrDefaultAsync();

            expenses.Fields = _context.Fields;

            return new Rotativa.AspNetCore.ViewAsPdf("ViewExpenses", expenses)
            {
                FileName = pdfFile,
                SaveOnServerPath = pdfFile
            };
        }
        public async Task<IActionResult> SendExpensesEmail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            string pdfFile = @"C:\Store\Expensas" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".pdf";

            await ExportPDFForMail(id, pdfFile);

            var expenses = await _context.Expenses
               .Include(e => e.ExpensesCosts)
               .Include(e => e.UnitDescriptionLines)
               .Include(e => e.Committee)
               .ThenInclude(c => c.Units)
               .ThenInclude(u => u.Tenant)
               .ThenInclude(t => t.User)
               .FirstOrDefaultAsync(e => e.Id == id);

            var committee = _context.Committees
                .Include(c => c.Administrator)
                .ThenInclude(a => a.User)
                .Include(c => c.Units)
                .ThenInclude(u => u.CheckingAccount)
                .Include(c => c.Fields)
                .FirstOrDefault();

            if (expenses == null)
                return NotFound();

            expenses.Fields = _context.Fields;

            foreach (Unit unit in expenses.Committee.Units)
            {
                if (unit.Tenant != null)
                {
                    var email = unit.Tenant.User.Email;
                    if (email == "karitovr80@gmail.com")
                        _mailService.SendEmailGmail(email, @"Expensas " + expenses.Month + expenses.Year, "Estimados Propietarios. \nAdjuntamos las expensas de este mes.\nSaludos Cordiales", pdfFile);
                }
            }
            return View();
        }
    }
}
