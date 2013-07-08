using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Neo4jApp.Models
{
    /// <summary>
    /// Entity base class - includes all properties that should be common among all entities
    /// </summary>
    public class Entity
    {
        /// <summary>
        /// Id automatically generated on construction of an object
        /// </summary>
        public Guid Id { get; set; }

        public Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}