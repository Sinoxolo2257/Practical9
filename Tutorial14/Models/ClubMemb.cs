using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tutorial14.Models
{
    public class ClubMemb
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string ClubName{get; set; }
        public int Age { get; set; }
        public decimal Fee { get; set; }
    }
}