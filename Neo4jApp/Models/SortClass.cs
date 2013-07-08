using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Neo4jApp.Models
{
    /// <summary>
    /// Movie entity - Movie belongs to Genre, Movie directed by Director
    /// </summary>
    public class Movie : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}