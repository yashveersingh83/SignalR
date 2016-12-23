using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Antlr.Runtime.Misc;
using Infragistics.Web.Mvc;
using Microsoft.Ajax.Utilities;
using SignalR.Models;
using SignalrDemo;

namespace SignalR.Controllers
{
    public class InfoViewModel
    {
        public InformationRequest InformationRequest { get; set; }
        public GridModel GridModel { get; set; }
    }
    public class InformationRequestController : Controller
    {
        private SignalRDbContext db = new SignalRDbContext();

        // GET: /InformationRequest/
        public ActionResult Index()
        {
            //InformationRequestHub.NotifyInformationRequestToClient();
            return View();
        }

        // GET: /InformationRequest/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InformationRequest informationrequest = db.InformationRequests.Find(id);
            if (informationrequest == null)
            {
                return HttpNotFound();
            }
            return View(informationrequest);
        }


        [HttpGet]
        public JsonResult GetAllInformationRequest()
        {
            var data = db.InformationRequests.ToList();
            var model = new InfoViewModel();
            model.GridModel = InfoGrid(data.Count,0,5,"");
            model.GridModel.DataSource = data.AsQueryable();
            return model.GridModel.GetData();
        }

        // GET: /InformationRequest/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /InformationRequest/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create( InformationRequest informationrequest)
        {
            //if (ModelState.IsValid)
            {
                db.InformationRequests.Add(informationrequest);
                db.SaveChanges();
                InformationRequestHub.NotifyInformationRequestToClient();
                return RedirectToAction("GetAllInformationRequest");
            }

            
        }

        // GET: /InformationRequest/Edit/5
        public ActionResult Edit(int? id)
        {
            return View();
        }

        // POST: /InformationRequest/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public void Edit([Bind(Include="Id,Description,Status")] InformationRequest informationrequest)
        {
          //  if (ModelState.IsValid)
           // {
                db.Entry(informationrequest).State = EntityState.Modified;
                db.SaveChanges();
                InformationRequestHub.NotifyUpdatedInformationRequestToClient(informationrequest);
              //  return RedirectToAction("Index");
          //  }
           // return View(informationrequest);
        }

        // GET: /InformationRequest/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InformationRequest informationrequest = db.InformationRequests.Find(id);
            if (informationrequest == null)
            {
                return HttpNotFound();
            }
            return View(informationrequest);
        }

        // POST: /InformationRequest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InformationRequest informationrequest = db.InformationRequests.Find(id);
            db.InformationRequests.Remove(informationrequest);
            db.SaveChanges();
            InformationRequestHub.NotifyInformationRequestToClient();
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

        public static GridModel InfoGrid(int totalRecords, int currentPageIndex, int pageSize, string dataSourceUrl)
        {
            var columns = new List<GridColumn>
            {
                new GridColumn {Key = "Id", HeaderText = "", DataType = "int", Width = "0%", Hidden = true},
                new GridColumn {Key = "Status", HeaderText = "Status",  Width = "30%"},
                new GridColumn {Key = "Description", HeaderText = "Description", Width = "35%"},
               
                
            };

            var sortingSettings = new ListStack<ColumnSortingSetting>
            {
                new ColumnSortingSetting {ColumnKey = "Status", AllowSorting = true},
                new ColumnSortingSetting {ColumnKey = "Description", AllowSorting = true}
            };
            //var toolTipSettings = new ListStack<ColumnTooltipsSetting>
            //{
            //    new ColumnTooltipsSetting {AllowTooltips = true, ColumnKey = "Comments"}
            //};
            var model = DataGridHelper.GetGridModel(columns)
                 .AddRemotePagingSorting(dataSourceUrl, pageSize, currentPageIndex, totalRecords, totalRecords > 0 ? sortingSettings : null)
                .AddLocalSorting(sortingSettings)
                .AddMultiColumnHeaders()
                  .AddMultiRowSelection();

        ////    model.RowTemplate = "<td></td>" +
        //                        "<td><input type='hidden' name='id' class='id' value='${Id}' /></td>" +
        //                        "<td id='Status'>${Status}</td>" +
        //                        "<td class='text-left' id='Description'>${Description}</td>";
                                  

            ;
           // model.Features.Add(new GridTooltips { ColumnSettings = toolTipSettings });
            return model;
        }
       
    
    }
}
