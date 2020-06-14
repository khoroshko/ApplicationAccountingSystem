using ApplicationAccountingSystemModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ApplicationAccountingSystemWeb.Models
{
    public class RequisitionViewModel
    {
        public IEnumerable<Requisition> Requisitions { get; set; }
        public SelectList Statuses { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}