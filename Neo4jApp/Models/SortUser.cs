using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Neo4jApp.Models
{
    /// <summary>
    /// Director Entity - A director directs a movie
    /// </summary>
    public class SortUser : Entity
    {
        public string name { get; set; }
    }
}