using ENoticeBoard.ViewModels;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ENoticeBoard.Controllers
{
    public class RocksController : Controller
    {
        private readonly MyDatabaseEntities _db = new MyDatabaseEntities();

        // GET: Rocks
        //public ActionResult Index()
        //{
        //    //ViewBag.objectSpend = _db.Objects.Sum(x => x.Cost);
        //    //return View(_db.Rocks.OrderByDescending(s=>s.Priority).ToList());
        //}
      

        // POST: Rocks/Create
        // To protect from over-posting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public PartialViewResult Summary(string period, string year)
        {
            if(period==null && year==null)
            {
                period = BreakagesController.CurrentPeriod();
                year = BreakagesController.CurrentYear();
            }

            var rockModel = new RockSummaryViewModel()
            {
                RockWFPs = _db.Vw_RocksWithinFinancialPeriod.Where(x=>x.FinancialPeriod==period && x.FinancialYear==year && x.isDeleted ==false  ).ToList(),
                Periodddl = _db.Vw_RocksWithinFinancialPeriod.Select(x=>new DropDownBoxList()
                {
                    text=x.FinancialPeriod,
                    value=x.FinancialPeriod
                }).Distinct().ToList(),
                Yearddl = _db.Vw_RocksWithinFinancialPeriod.Select(x=>new DropDownBoxList()
                {
                    text=x.FinancialYear,
                    value=x.FinancialYear
                }).Distinct().ToList(),
                SelectedPeriod = period,
                SelectedYear = year
            };
            
            return PartialView(rockModel);
        }


        [HttpPost]
        public JsonResult Create(String subject,int? priority, string dateDue)
        {
            DateTime dueDateInDateTime=DateTime.Parse(dateDue);
        
            if (ModelState.IsValid)
            {
                if (priority != null)
                {
                    var rock = new Rock
                    {
                        Subject = subject,
                        Priority = priority.Value,
                        DateCreated = DateTime.Today,
                        DateDue=  dueDateInDateTime,
                        Done = false,
                        isDeleted = false
                        
                    };
                    _db.Rocks.Add(rock);
                }

                _db.SaveChanges();
              
            }

            return Json("Sucess", JsonRequestBehavior.AllowGet);
        }

        // GET: Rocks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rock rock = _db.Rocks.Find(id);
            if (rock == null)
            {
                return HttpNotFound();
            }

            var model = new RockFormViewModel()
            {
                RockId = rock.RockId,
                Subject = rock.Subject,
                Priority = rock.Priority,
                DateCreated = rock.DateCreated,
                DateDue = rock.DateDue,
                Done = rock.Done
            };
            return PartialView(model);
        }

       

        // POST: Rocks/Edit/5
        // To protect from over-posting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(RockFormViewModel viewModel)
        {
            
            if (ModelState.IsValid)
            {
                Rock rock = _db.Rocks.Find(viewModel.RockId);
                if (rock == null)
                {
                    return HttpNotFound();
                }

                rock.Subject = viewModel.Subject;
                rock.Priority = viewModel.Priority;
                rock.DateDue = viewModel.DateDue;
                rock.Done = viewModel.Done;
                _db.SaveChanges();
            }

            return Json("success",JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult EditinSummary(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rock rock = _db.Rocks.Find(id);
            if (rock == null)
            {
                return HttpNotFound();
            }

            var model = new RockFormViewModel()
            {
                RockId = rock.RockId,
                Subject = rock.Subject,
                Priority = rock.Priority,
                DateDue = rock.DateDue,
                Done = rock.Done
            };
            return PartialView(model);
        }

        // GET: Breakages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Rock rock = _db.Rocks.Find(id);
            if (rock == null)
            {
                return HttpNotFound();
            }
            
            var model = new RockFormViewModel()
            {
                RockId= rock.RockId,
                Subject = rock.Subject,
                Priority = rock.Priority,
                DateCreated = rock.DateCreated,
                DateDue = rock.DateDue,
                Done = rock.Done
            };
            

            return PartialView(model);
        }
        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rock rock = _db.Rocks.Find(id);
            if (rock == null)
            {
                return HttpNotFound();
            }
            return PartialView(rock);
        }

        [HttpPost]
        public JsonResult DeleteConfirmed(int? id)
        {
            Rock rock = _db.Rocks.Find(id);
            if (rock != null)
            {
                rock.isDeleted = true;
            }

            _db.SaveChanges();
            return Json("success", JsonRequestBehavior.AllowGet);
            
        }


        // GET: Rocks/Done/5
        public ActionResult Done(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rock rock = _db.Rocks.Find(id);
            if (rock == null)
            {
                return HttpNotFound();
            }
            return PartialView(rock);
        }

        // POST: Rocks/Done/5
        [HttpPost, ActionName("Done")]
        [ValidateAntiForgeryToken]
        public ActionResult DoneConfirmed(int id)
        {
            var rock = _db.Rocks.FirstOrDefault(x => x.RockId == id);
            if (rock != null)
            {
                rock.Done = true;
            }

            _db.SaveChanges();
            return RedirectToAction("Index","Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }


        public JsonResult GetRocks()
        {

            var rocks = (_db.Rocks.OrderByDescending(s => s.Priority).ToList());
            return new JsonResult{Data = rocks, JsonRequestBehavior = JsonRequestBehavior.AllowGet};
            
        }
    }
}

