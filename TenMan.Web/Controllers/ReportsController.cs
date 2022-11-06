using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenMan.Web.Data;
using TenMan.Web.Data.Entities;
using TenMan.Web.Models;

namespace TenMan.Web.Controllers
{
    public class ReportsController : Controller
    {
        private readonly DataContext _context;

        public ReportsController(DataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> CommitteeReports()
        {
            return View(await _context.Committees.ToListAsync());
        }
        public IActionResult Expenses()
        {
            return View();
        }
        public IActionResult Bar()
        {
            var markList = GetStudentMarkList();
            return View(markList);
        }
        public IActionResult Line()
        {
            var markList = GetStudentMarkList();
            return View(markList);
        }
        public IActionResult Radar()
        {
            var markList = GetStudentMarkList();
            return View(markList);
        }

        public IActionResult Doughnut()
        {
            var scoreList = GetStudentScoreDetails();
            return View(scoreList);
        }
        public IActionResult Polararea()
        {
            var scoreList = GetStudentScoreDetails();
            return View(scoreList);
        }


        public List<Payment> GetPaymentsList()
        {
            var list = _context.Payments
                .Include(p => p.Unit)
                .ToList();

            return list;
        }
        public IActionResult RequestsReport(int id)
        {
            var committee = _context.Committees.Find(id);

            if (committee == null)
                return NotFound();

            var list = _context.Requests
                .Include(r => r.Unit)
                .ThenInclude(u => u.Committee)
                .Where(r => r.Unit.Committee.Id == id)
                .ToList();

            int generatedCant = 0;
            int asignedCant = 0;
            int finishedCant = 0;
            int inProcessCant = 0;

            foreach (Request item in list)
            {
                if (item.ActualStatus == "Finalizada")
                    finishedCant++;

                else if (item.ActualStatus == "Asignada")
                    asignedCant++;

                else if (item.ActualStatus == "Generada")
                    generatedCant++;

                else
                {
                    inProcessCant++;
                }
            }

            var model = new RequestsReportViewModel
            {
                GeneratedCant = generatedCant,
                AsignedCant = asignedCant,
                FinishedCant = finishedCant,
                InProcessCant = inProcessCant,
                Requests = list
            };
            return View(model);
        }
        //Make function in models and get data from database as per your requirement... //Dont do in controller like this
        public List<StudentMarkDetails> GetStudentMarkList()
        {
            var studentmarkList = new List<StudentMarkDetails>()
            {
                new StudentMarkDetails() { id = 1, name = "Juan", Physics = 62,Chemistry=78,Biology=84,Mathematics=96 },
                new StudentMarkDetails() { id = 2, name = "María", Physics = 96,Chemistry=78,Biology=69,Mathematics=88 },
                new StudentMarkDetails() { id = 3, name = "Barby", Physics = 49,Chemistry=85,Biology=63,Mathematics=77 },
                new StudentMarkDetails() { id = 4, name = "Emilia", Physics = 85,Chemistry=56,Biology=78,Mathematics=55 },
                new StudentMarkDetails() { id = 5, name = "Ainhoa", Physics = 74,Chemistry=55,Biology=36,Mathematics=69 },
            };
            return studentmarkList;
        }
        public List<StudentScoreDetails> GetStudentScoreDetails()
        {
            var studentscoreList = new List<StudentScoreDetails>()
            {
                new StudentScoreDetails() { id = 1, name = "Juan", score = 62},
                new StudentScoreDetails() { id = 2, name = "María", score = 96 },
                new StudentScoreDetails() { id = 3, name = "Barby", score = 49 },
                new StudentScoreDetails() { id = 4, name = "Emilia", score = 85 },
                new StudentScoreDetails() { id = 5, name = "Ainhoa", score = 74},
            };
            return studentscoreList;
        }

    }
}
