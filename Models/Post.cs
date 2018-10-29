using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HangfireEmailSchedule.Models
{
    public class Post {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Text { get; set; }
    }
}
