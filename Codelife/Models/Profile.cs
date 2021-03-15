using Codelife.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Codelife.Models
{
    public class Profile
    {
        public int authorId { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string profile { get; set; }
        public DateTime? dateRegistered { get; set; }
        public List<Post> Posts { get; set; }
    }

}