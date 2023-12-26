using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIS_2_.Data;
using SIS_2_.Models;
using SIS_2_.Models.Domain;
using System.Diagnostics;

namespace SIS_2_.Controllers
{
    public class HomeController : Controller
    {
        private readonly StudentDataContext _context;

        public HomeController(StudentDataContext _context)
        {
            this._context = _context;
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Index()
        {
            var top5Interests = _context.Students
            .GroupBy(s => s.InterestName)
            .OrderByDescending(g => g.Count())
            .Take(5)
            .Select(g => g.Key)
            .ToList();

            // Bottom 5 interests
            var bottom5Interests = _context.Students
                .GroupBy(s => s.InterestName)
                .OrderBy(g => g.Count())
                .Take(5)
                .Select(g => g.Key)
                .ToList();

            // Distinct interests count
            var distinctInterestsCount = _context.Students.Select(s => s.InterestName).Distinct().Count();
            var count = _context.Students.Count();
            // Pass the data to the view
            ViewBag.Top5Interests = top5Interests;
            ViewBag.Bottom5Interests = bottom5Interests;
            ViewBag.DistinctInterestsCount = distinctInterestsCount;
            ViewBag.Count = count;
            var current = DateTime.Now;
            var currentDate = new DateOnly(current.Year, current.Month, current.Day);

            var isCurrentlyStudying = _context.Students.Count(s => s.StartDate <= currentDate && s.EndDate >= currentDate);
            var isRecentlyEnrolled = _context.Students.Count(s => s.StartDate <= currentDate && s.EndDate >= currentDate.AddMonths(1));
            var isAboutToGraduate = _context.Students.Count(s => s.StartDate <= currentDate.AddMonths(2) && s.EndDate >= currentDate.AddMonths(1));
            var isGraduated = _context.Students.Count(s => s.EndDate < currentDate);

            ViewBag.CurrentlyStudyingCount = (isCurrentlyStudying);
            ViewBag.RecentlyEnrolledCount =(isRecentlyEnrolled);
            ViewBag.AboutToGraduateCount =(isAboutToGraduate);
            ViewBag.GraduatedCount = (isGraduated);
            var startDate = DateTime.UtcNow.AddDays(-30);
            var submissionData = _context.ActivityLogs
                .Where(s => s.ActionType == "AddStudent" && s.Timestamp >= startDate)
                .GroupBy(s => s.Timestamp.Date)
                .Select(group => new
                {
                    Date = group.Key,
                    Count = group.Count(),
                })
                .OrderBy(data => data.Date)
                .ToList();
            ViewBag.SubmissionChart = submissionData;

            var chartData = _context.ActivityLogs.ToList();
            ViewBag.ChartData = chartData;
            return View();

        }
       
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
