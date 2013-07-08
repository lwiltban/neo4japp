using Neo4jClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Neo4jApp.Models.Relationships
{
    /// <summary>
    /// PartOf relationship - A movie is a part of a genre
    /// </summary>
    public class PartOf : Relationship, IRelationshipAllowingSourceNode<Movie>,
    IRelationshipAllowingTargetNode<Genre>
    {
        public static readonly string TypeKey = "PART_OF";

        public string Caption { get; set; }

        public PartOf(NodeReference targetNode)
            : base(targetNode)
        { }

        public override string RelationshipTypeKey
        {
            get { return TypeKey; }
        }
    }
}