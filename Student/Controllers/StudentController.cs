using Student.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Student.Controllers
{
    public class StudentController : Controller
    {
        private StudentBL obj = new StudentBL();
         
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            var result = obj.GetAllStudent(page, pageSize);
            
            return View(result);
        }

        [HttpGet]
        public ActionResult AddStudent()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddStudent(Student.Models.Student std)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    StudentBL student = new StudentBL();
                    bool effacetedId = student.AddStudent(std);
                    if (effacetedId)
                    {
                        TempData["SUCCESS"] = "Student  added successfully";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["ERROR"] = "Student  added FAILD";
                        return RedirectToAction("Index");
                    }
                }
            }
            catch
            {
                return View();
            }
            return View();
        }
        public ActionResult EditStudent(int id)
        {
            StudentBL std = new StudentBL();

            return View(std.GetById().FirstOrDefault(x => x.ID == id));
        }

        [HttpPost]
        public ActionResult EditStudent(Student.Models.Student obj)
        {
            try
            {
                StudentBL std = new StudentBL();

               bool EffectedId =  std.UpdateStudent(obj);
                if (EffectedId) {
                    TempData["SUCCESS"] = "Update Record successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ERROR"] = "Update Record Faild !";
                    return RedirectToAction("AddStudent");
                }
            }
            catch(Exception ex)
            {
                throw new Exception();
            }
        }
        public ActionResult DeleteStd(int Id)
        {
            try
            {
                StudentBL std = new StudentBL();
                if (std.DeleteStudent(Id))
                {
                    TempData["SUCCESS"] = "Student details deleted successfully";
                    return RedirectToAction("index");
                }
                else
                {
                    TempData["ERROR"] = "Faild TO Delete Data  !";
                    return RedirectToAction("index");
                }
            }
            catch(Exception ex)
            {
                throw new Exception();
            }
        }
    }
}