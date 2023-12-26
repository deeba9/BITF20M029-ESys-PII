using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SIS_2_.Data;
using SIS_2_.Models.Domain;
using SIS_2_.Models;
using Microsoft.EntityFrameworkCore;
using SIS_2_.Models.ViewModels;

namespace SIS_2_.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentDataContext context;
        public StudentController(StudentDataContext context)
        {
            this.context = context;
        }

        public IActionResult List()
        {
            IQueryable<Student> studentsQuery = context.Students;

          
            var students = studentsQuery.ToList();

            var viewModel = new StudentListViewModel
            {
                Students = students,
               
            };
            var currentDate = DateTime.Now;
            var utcTimestamp = currentDate.ToUniversalTime();
            var log = new ActivityLog
            {
                UserId = "admin",
                Timestamp = utcTimestamp,
                ActionType = "ViewDashboard", // Example action type
            };
            context.ActivityLogs.Add(log);
            context.SaveChanges();
            return View(viewModel);
        }

        public IActionResult List2()
        {
            IQueryable<Student> studentsQuery = context.Students;


            var students = studentsQuery.ToList();

            var viewModel = new StudentListViewModel
            {
                Students = students,

            };

            return View(viewModel);
        }

     
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            // Populate the ViewBag with interests for the dropdown
            ViewBag.AvailableInterests = new SelectList(context.Interests, "Id", "InterestName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StudentInputModel inputModel)
        {
            if (ModelState.IsValid)
            {
                // Check if the interest already exists
                var existingInterest = context.Interests.FirstOrDefault(i => i.InterestName == inputModel.InterestName);

                if (existingInterest == null)
                {
                    // Interest does not exist, create a new one
                    var newInterest = new Interest { InterestName = inputModel.InterestName };
                    context.Interests.Add(newInterest);
                    context.SaveChanges();

                    // Update the existingInterest reference with the newly created interest
                    existingInterest = newInterest;
                }

                // Continue with saving the student
                var student = new Student
                {
                    FullName = inputModel.FullName,
                    RollNumber = inputModel.RollNumber,
                    DateOfBirth = inputModel.DateOfBirth,
                    City = inputModel.City,
                    DegreeTitle = inputModel.DegreeTitle,
                    Department = inputModel.Department,
                    Gender = inputModel.Gender,
                    Email = inputModel.Email,
                    Subject = inputModel.Subject,
                    StartDate = inputModel.StartDate,
                    EndDate = inputModel.EndDate,
                    InterestName = existingInterest.InterestName  // Update the Interest navigation property
                };

                context.Students.Add(student);
                context.SaveChanges();
                var currentDate = DateTime.Now;
                var utcTimestamp = currentDate.ToUniversalTime();
                var log = new ActivityLog
                {
                    UserId = "admin",
                    Timestamp = utcTimestamp,
                    ActionType = "AddStudent", // Example action type
                };
                context.ActivityLogs.Add(log);
                context.SaveChanges();

                return RedirectToAction(nameof(List));
            }

            // Repopulate the ViewBag with interests in case of validation errors
            ViewBag.AvailableInterests = new SelectList(context.Interests, "Id", "InterestName");

            return RedirectToAction(nameof(List));
        }

        public IActionResult Delete(int  id)
        {
            var student = context.Students.FirstOrDefault(x => x.Id == id);
            if(student != null)
            {
                var currentDate = DateTime.Now;
                var utcTimestamp = currentDate.ToUniversalTime();
                var log = new ActivityLog
                {
                    UserId = "admin",
                    Timestamp = utcTimestamp,
                    ActionType = "DeleteStudent", // Example action type
                };
                context.ActivityLogs.Add(log);
                context.SaveChanges();
                context.Students.Remove(student);
                context.SaveChanges();
            }
            return RedirectToAction(nameof(List));
        }

        public IActionResult View(int id)
        {
            var student = context.Students.FirstOrDefault(x => x.Id == id);
            if(student != null)
            {
                var currentDate = DateTime.Now;
                var utcTimestamp = currentDate.ToUniversalTime();
                var log = new ActivityLog
                {
                    UserId = "admin",
                    Timestamp = utcTimestamp,
                    ActionType = "ViewStudent", // Example action type
                };
                context.ActivityLogs.Add(log);
                context.SaveChanges();
                return View("View", student);
            }
            return View();
        }

        public IActionResult View2(int id)
        {
            var student = context.Students.FirstOrDefault(x => x.Id == id);
            if (student != null)
            {
                var currentDate = DateTime.Now;
                var utcTimestamp = currentDate.ToUniversalTime();
                var log = new ActivityLog
                {
                    UserId = "admin",
                    Timestamp = utcTimestamp,
                    ActionType = "ViewStudent", // Example action type
                };
                context.ActivityLogs.Add(log);
                context.SaveChanges();
                return View("View2", student);
            }
            return View();
        }
        public ActionResult Edit(int id)
        {
            var student = context.Students.FirstOrDefault(s => s.Id == id);

            if (student != null)
            {
                
                return View("Edit", student);
            }

            return View();
        }
        [HttpPost]
        public ActionResult Edit(StudentInputModel model)
        {
            if (ModelState.IsValid)
            {
                // Find the existing student in the list
                var existingStudent = context.Students.FirstOrDefault(s => s.Id == model.Id);

                if (existingStudent != null)
                {
                    // Update only the changed properties
                    existingStudent.FullName = model.FullName;
                    existingStudent.RollNumber = model.RollNumber;
                    existingStudent.Gender = model.Gender;
                    existingStudent.Email = model.Email;
                    existingStudent.City = model.City;
                    existingStudent.InterestName = model.InterestName;
                    var interest = context.Interests.FirstOrDefault(s => s.InterestName == model.InterestName);
                    if (interest == null)
                    {
                        var newInterest = new Interest { InterestName = model.InterestName };
                        context.Interests.Add(newInterest);
                    }
                    existingStudent.Subject = model.Subject;
                    existingStudent.DateOfBirth = model.DateOfBirth;
                    existingStudent.Department = model.Department;
                    existingStudent.DegreeTitle = model.DegreeTitle;
                    existingStudent.StartDate = model.StartDate;
                    existingStudent.EndDate = model.EndDate;
                    var currentDate = DateTime.Now;
                    var utcTimestamp = currentDate.ToUniversalTime();
                    var log = new ActivityLog
                    {
                        UserId = "admin",
                        Timestamp = utcTimestamp,
                        ActionType = "UpdatedStudent", // Example action type
                    };
                    context.ActivityLogs.Add(log);
                    context.SaveChanges();
                    // Call SaveChanges only after making all updates
                    context.SaveChanges();

                    return RedirectToAction("List");
                }
            }

            // If ModelState is not valid, redisplay the Edit view with validation errors
            return View("Edit", model);
        }


    }
}
