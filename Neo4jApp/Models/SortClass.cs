using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Neo4jApp.Models
{
    /// <summary>
    /// Movie entity - Movie belongs to Genre, Movie directed by Director
    /// </summary>
    public class SortClass : Entity
    {
        public string name { get; set; }
        public string description { get; set; }
    }
}