using System; 
using Microsoft.EntityFrameworkCore; 
using Microsoft.AspNetCore.Http; 
using Microsoft.AspNetCore.Mvc; 
using System.Collections.Generic; 
using System.Linq; 
using Belt.Models; 

namespace Belt.Controllers{

    public class ActivityController: Controller{

        private BeltContext _context; 
        public ActivityController(BeltContext context){
            _context = context; 
        }

        [HttpGet]
        [Route("home")]
        public IActionResult home(){
            int? id = HttpContext.Session.GetInt32("ActiveUser");
            if(id != null){
                User active = _context.Users.Single(u => u.UserId == id);
                List<Activity> activities = _context.Activities.OrderByDescending(a => a.created_at).Include(user => user.user).Include(r => r.Joined).ToList();
                ViewBag.activities = activities;
                ViewBag.active = active; 
                return View("index");
            }
            else{
                return Redirect("/");
            }
        }
        [HttpGet]
        [Route("viewevent/{id}")]
        public IActionResult viewevent(int id){
            int? active = HttpContext.Session.GetInt32("ActiveUser");
            if(active != null){
                List<Activity> view = _context.Activities.Where(i => i.ActivityId == id).Include(u => u.user).Include(r => r.Joined).ToList();
                List<Join> joined = _context.Joined.Where(r => r.ActivitiesId == id).Include(u => u.user).ToList();
                // if(view.Count < 1 || joined.Count < 1){
                //     return RedirectToAction("home");
                // }
                User activeuser = _context.Users.Single(u => u.UserId == active);
                ViewBag.joined = joined; 
                ViewBag.activity = view[0];
                ViewBag.active = activeuser;
                return View("view");
            }
            else{
                return Redirect("/");
            }
        }
        [HttpGet]
        [Route("addactivity")]
        public IActionResult addevent(){
            int? id = HttpContext.Session.GetInt32("ActiveUser");
            if(id != null){
                return View("new"); 
            }
            else{
                return Redirect("/");
            }
        }
        [HttpPost]
        [Route("addactivity")]
        public IActionResult addactivity(ActivityViewModel act){
            if(act.Date.ToString().Contains("1/1/0001")){
                ViewBag.dateErr = "Please specify a valid date";
                return View("new");
            }
            if(ModelState.IsValid){
                int? active = HttpContext.Session.GetInt32("ActiveUser");
                TimeSpan ts = act.Time;
                act.Date = act.Date + ts;
                Activity newact = new Activity{
                    Title = act.Title, 
                    Description = act.Description, 
                    Date = act.Date, 
                    Time = act.Time, 
                    Duration = act.Duration,
                    UsersId = (int)active
                };
                _context.Add(newact);
                _context.SaveChanges();
                return Redirect($"viewevent/{newact.ActivityId}");
            }
            else{
                return View("new");
            }
        }
        [HttpGet]
        [Route("deleteactivity/{id}")]
        public IActionResult deleteactivity(int id){
            int? active = HttpContext.Session.GetInt32("ActiveUser");
            if(active == null){
                return Redirect("/");
            }
            else{
                List<Activity> toremove = _context.Activities.Where(i => i.ActivityId== id).Include(u => u.user).Include(r => r.Joined).ToList();
                if(toremove.Count < 1){
                    return Redirect("/home");
                }
                Activity remove = toremove[0];
                if(remove.user.UserId != active){
                    return Redirect("/home");
                }
                else{
                    _context.RemoveRange(_context.Joined.Where(w => w.ActivitiesId == remove.ActivityId));
                    _context.Activities.Remove(remove);
                    _context.SaveChanges(); 
                    return Redirect("/home");
                }
            }
        }
        [HttpGet]
        [Route("join/{id}")]
        public IActionResult join(int id){
            int? active = HttpContext.Session.GetInt32("ActiveUser");
            if(active != null){
                List<Activity> tojoin = _context.Activities.Where(i => i.ActivityId == id).Include(r => r.Joined).ToList();
                // if(tojoin.Count < 1){
                //     return RedirectToAction("home");
                // }
                Activity join = tojoin[0];
                // foreach(var each in join.Joined){
                //     if(each.ActivitiesId == id && each.UsersId == active){
                //         return RedirectToAction("home");
                //     }
                // }
                List<User> joining = _context.Users.Where(i => i.UserId == active).Include(a => a.Joined).ThenInclude(j => j.activity).ToList();
                foreach(var each in joining){
                    if(each.Joined.Count < 1){
                        break;
                    }
                    else{
                        bool check = true; 
                        foreach(var one in each.Joined){
                            System.Console.WriteLine(join.Time + ":" + one.activity.Time.Add(one.activity.Duration) + ":" + one.activity.Time);
                            if(join.Time >= one.activity.Time && join.Time <= (one.activity.Time.Add(one.activity.Duration))){
                                check = false; 
                            }
                            if(join.Time.Add(join.Duration) >= one.activity.Time && join.Time.Add(join.Duration) <= one.activity.Time.Add(one.activity.Duration)){
                                check = false;
                            }
                        }
                        if(check == false){
                            System.Console.WriteLine("************************8888");
                            return RedirectToAction("home");
                        }
                    }
                }
                Join newjoin = new Join{
                    UsersId = (int)active, 
                    ActivitiesId = id
                };
                _context.Joined.Add(newjoin);
                _context.SaveChanges();
                return RedirectToAction("home");
            }
            else{
                return Redirect("/");
            }
        }

        [HttpGet]
        [Route("leave/{id}")]
        public IActionResult leave(int id){
            int? active = HttpContext.Session.GetInt32("ActiveUser");
            if(active != null){
                List<Join> join = _context.Joined.Where(i => i.ActivitiesId == id).Include(u => u.user).ToList();
                if(join.Count < 1){
                    return RedirectToAction("home");
                }
                foreach(var each in join){
                    if(each.UsersId == active){
                        _context.Joined.Remove(each);
                        _context.SaveChanges();
                        return RedirectToAction("home");
                    }
                }
                return RedirectToAction("home");
            }
            else{
                return Redirect("/");
            }
        }
    }
}