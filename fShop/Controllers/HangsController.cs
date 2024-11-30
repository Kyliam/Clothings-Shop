using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using fShop.Models;
using PagedList;
namespace fShop.Controllers
{
    public class HangsController : Controller
    {
        private fShopDB db = new fShopDB();

        // GET: Hangs
        public ActionResult Index(string sort, string searchStr, string currentFilter ,int? page)
        {
            //Bien sap xep
            ViewBag.currentSort = sort;
            ViewBag.SapTheoTen = String.IsNullOrEmpty(sort)?"name_desc":"";
            ViewBag.SapTheoGia = sort == "Gia" ? "gia_desc" : "Gia"; 
            //lay danh sach hang
            var hangs = db.Hangs.Select(p => p);

            if (searchStr != null)
            {
                page = 1;
            }
            else
            {
                searchStr = currentFilter;
            }
            ViewBag.CurrentFilter=searchStr;
            //tim kiem
            if(!String.IsNullOrEmpty(searchStr))
            {
                hangs = hangs.Where(p=>p.TenHang.Contains(searchStr));
            }
           
            //sap xep
            switch(sort){
                case "name_desc":
                    hangs = hangs.OrderByDescending(s=>s.TenHang);
                    break;
                case "Gia":
                    hangs = hangs.OrderBy(s => s.Gia);
                    break;
                case "gia_desc":
                    hangs = hangs.OrderByDescending(s => s.Gia);
                    break;
                default:
                    hangs = hangs.OrderBy(s => s.TenHang);
                    break;

            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(hangs.ToPagedList(pageNumber,pageSize));
        }

        // GET: Hangs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hang hang = db.Hangs.Find(id);
            if (hang == null)
            {
                return HttpNotFound();
            }
            return View(hang);
        }

        // GET: Hangs/Create
        public ActionResult Create()
        {
            ViewBag.MaNCC = new SelectList(db.Nha_CC, "MaNCC", "TenNCC");
            return View();
        }

        // POST: Hangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHang,MaNCC,TenHang,Gia,LuongCo,MoTa,ChietKhau,HinhAnh")] Hang hang)
        {
            if (ModelState.IsValid)
            {
                db.Hangs.Add(hang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaNCC = new SelectList(db.Nha_CC, "MaNCC", "TenNCC", hang.MaNCC);
            return View(hang);
        }

        // GET: Hangs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hang hang = db.Hangs.Find(id);
            if (hang == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNCC = new SelectList(db.Nha_CC, "MaNCC", "TenNCC", hang.MaNCC);
            return View(hang);
        }

        // POST: Hangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHang,MaNCC,TenHang,Gia,LuongCo,MoTa,ChietKhau,HinhAnh")] Hang hang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaNCC = new SelectList(db.Nha_CC, "MaNCC", "TenNCC", hang.MaNCC);
            return View(hang);
        }

        // GET: Hangs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hang hang = db.Hangs.Find(id);
            if (hang == null)
            {
                return HttpNotFound();
            }
            return View(hang);
        }

        // POST: Hangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Hang hang = db.Hangs.Find(id);
            db.Hangs.Remove(hang);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
