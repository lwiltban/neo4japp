using Neo4jClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Neo4jApp.Models.Relationships
{
    /// <summary>
    /// Directed relationship - A movie is a part of a genre
    /// </summary>
    public class Attended : Relationship<Payloads.Payload>, IRelationshipAllowingSourceNode<SortUser>,
    IRelationshipAllowingTargetNode<SortClass>
    {
        public static readonly string TypeKey = "ATTENDED";

        public string Caption { get; set; }

        public Attended(NodeReference targetNode, Payloads.Payload payload)
            : base(targetNode, payload)
        { }

        public override string RelationshipTypeKey
        {
            get { return TypeKey; }
        }
    }
}