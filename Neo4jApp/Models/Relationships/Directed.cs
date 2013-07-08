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
    public class Directed : Relationship<Payloads.Payload>, IRelationshipAllowingSourceNode<Director>,
    IRelationshipAllowingTargetNode<Movie>
    {
        public static readonly string TypeKey = "DIRECTED";

        public string Caption { get; set; }

        public Directed(NodeReference targetNode, Payloads.Payload payload)
            : base(targetNode, payload)
        { }

        public override string RelationshipTypeKey
        {
            get { return TypeKey; }
        }
    }
}