using ApplicationAccountingSystemWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ApplicationAccountingSystemModel
{
    public class Requisition
    {
        public int Id { get; set; }

        public string Note { get; set; }

        [Required]
        public string Text { get; set; }

        public int? StatusId { get; set; }

        public Status Status { get; set; }

        [Required]
        public string UserExecutorId { get; set; }

        [Required]
        public DateTime DateCreate { get; set; }

        [Required]
        public string UserCreate { get; set; }

        public DateTime DateChange { get; set; }

        public string UserChange { get; set; }
    }

    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
