using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Neo4jApp.Models
{
    /// <summary>
    /// Genre entity - Genre classifies a movie
    /// </summary>
    public class Genre : Entity
    {
        public string Name { get; set; }
    }
}