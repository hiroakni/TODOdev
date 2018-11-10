using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TodoApp.Models;

namespace TodoApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private AppContext db = new AppContext();

        // GET: Home
        public ActionResult Index()
        {
            int userId = (int)Session["AuthUserId"];
            var user = db.Users.Where(u => u.Id == userId).First();

            return View(user.Todoes.ToArray());
        }

        public ActionResult ViewTransactionInfo()
        {
            int userId = (int)Session["AuthUserId"];
            var user = db.Users.Where(u => u.Id == userId).First();

            return View(user.TransactionInfos.ToArray());
        }
        // GET: Home/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Todo todo = db.Todoes.Find(id);
            if (todo == null)
            {
                return HttpNotFound();
            }
            // 改行コードを画面表示用に置き換える
            string text = todo.Detail.Replace("\r\n","<br>");
            ViewBag.viewDetailTxt = text;

            return View(todo);
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Detail,Pass")] Todo todo)
        {
            if (ModelState.IsValid)
            {
                int userId = (int)Session["AuthUserId"];
                var user = db.Users.Where(u => u.Id == userId).FirstOrDefault();
                if (user != null)
                {
                    todo.User = user;
                }

                db.Todoes.Add(todo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(todo);
        }

        // GET: Home/CreateTransactionInfo
        public ActionResult CreateTransactionInfo()
        {
            return View();
        }

        // POST: Home/CreateTransactionInfo
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTransactionInfo([Bind(Include = "Id,TransactionTitle,ToushouCode,PurchaseFund,PurchaseCount,PurchaseDate")] TransactionInfo transactionInfo)
        {
            if (ModelState.IsValid)
            {
                int userId = (int)Session["AuthUserId"];
                var user = db.Users.Where(u => u.Id == userId).FirstOrDefault();
                if (user != null)
                {
                    transactionInfo.User = user;
                }
                db.TransactionInfos.Add(transactionInfo);
                db.SaveChanges();
                return RedirectToAction("ViewTransactionInfo");
            }

            return View(transactionInfo);
        }
        // GET: Home/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Todo todo = db.Todoes.Find(id);
            if (todo == null)
            {
                return HttpNotFound();
            }
            return View(todo);
        }
        // POST: Home/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Detail,Pass")] Todo todo)
        {
            if (ModelState.IsValid)
            {
                // Passwordの入力チェックを実施する
                using (var db = new AppContext())
                {
                    // 対象となるデータの情報をDBから取得する
                    var user = db.Todoes.Find(todo.Id);

                    var password = db.Todoes
                        .Where(u => user.Pass == todo.Pass && user.Id == todo.Id)
                        .FirstOrDefault();

                    if (password == null)
                    {
                        // 認証NG
                        ViewBag.errMessage = "パスワードが違います。";
                        return View(todo);
                    }
                }

                db.Entry(todo).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(todo);
        }
        // GET: Home/EditTransactionInfo
        public ActionResult EditTransactionInfo(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransactionInfo transactionInfo = db.TransactionInfos.Find(id);
            if (transactionInfo == null)
            {
                return HttpNotFound();
            }
            return View(transactionInfo);
        }

        // POST: Home/EditTransactionInfo
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTransactionInfo([Bind(Include = "Id,TransactionTitle,ToushouCode,PurchaseFund,TransactionFund,PurchaseCount,TransactionCount,PurchaseDate,TransactionDate")] TransactionInfo transactionInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transactionInfo).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("ViewTransactionInfo");
            }
            return View(transactionInfo);
        }
        // GET: Home/EditTransactionSellInfo
        public ActionResult EditTransactionSellInfo(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransactionInfo transactionInfo = db.TransactionInfos.Find(id);
            if (transactionInfo == null)
            {
                return HttpNotFound();
            }
            return View(transactionInfo);
        }

        // POST: Home/EditTransactionSellInfo
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTransactionSellInfo([Bind(Include = "Id,TransactionTitle,ToushouCode,PurchaseFund,TransactionFund,PurchaseCount,TransactionCount,PurchaseDate,TransactionDate")] TransactionInfo transactionInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transactionInfo).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("ViewTransactionInfo");
            }
            return View(transactionInfo);
        }
        // GET: Home/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Todo todo = db.Todoes.Find(id);
            if (todo == null)
            {
                return HttpNotFound();
            }
            return View(todo);
        }

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed([Bind(Include = "Id,Title,Detail,Pass")] Todo todo)
        {
            var userDetail = db.Todoes.Find(todo.Id);
            if (ModelState.IsValid)
            {
                // Passwordの入力チェックを実施する
                using (var db = new AppContext())
                {
                    // 対象となるデータの情報をDBから取得する
                    var user = db.Todoes.Find(todo.Id);

                    var password = db.Todoes
                        .Where(u => user.Pass == todo.Pass && user.Id == todo.Id)
                        .FirstOrDefault();

                    if (password == null)
                    {
                        // 認証NG
                        ViewBag.errMessage = "パスワードが違います。";
                        return View(userDetail);
                    }
                }

                db.Todoes.Remove(userDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(todo);
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
