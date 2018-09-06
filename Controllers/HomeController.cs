using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using mvcUploadFile.Data;
using mvcUploadFile.Interfaces;
using mvcUploadFile.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace mvcUploadFile.Controllers {
    public class HomeController : Controller {
        private readonly INoteRepository _noteRepository;
        public HomeController (INoteRepository noteRepository) {
            _noteRepository = noteRepository;
        }

        public IActionResult Index () {
            return View ();
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile (IFormFile file) {
            await _noteRepository.AddNote (new Note {
                Id = Guid.NewGuid ().ToString (),
                    Body = "newNote.Body",
                    UpdatedOn = DateTime.Now,
                    UserId = 1
            });

            if (file == null || file.Length == 0) {
                return Content ("file not selected");
            }

            if (Path.GetExtension (file.FileName) != ".csv") {
                return Content ("file is not of the correct extension");
            }

            List<CustomerModel> customers = new List<CustomerModel> ();
            using (var reader = new StreamReader (file.OpenReadStream ())) {
                while (reader.Peek () >= 0) {
                    var result = await reader.ReadLineAsync ();
                    customers.Add (new CustomerModel {
                        Id = Convert.ToInt32 (result.Split (';') [0]),
                            Name = result.Split (';') [1]
                    });
                }
            }

            return View ("Index", customers);
        }

        public IActionResult About () {
            ViewData["Message"] = "Your application description page.";

            return View ();
        }

        public IActionResult Contact () {
            ViewData["Message"] = "Your contact page.";

            return View ();
        }

        public IActionResult Privacy () {
            return View ();
        }

        [ResponseCache (Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error () {
            return View (new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}