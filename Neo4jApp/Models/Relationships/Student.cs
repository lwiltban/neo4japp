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
    public class Student : Relationship<Payloads.Payload>, IRelationshipAllowingSourceNode<SortClass>,
    IRelationshipAllowingTargetNode<SortUser>
    {
        public static readonly string TypeKey = "STUDENT";

        public string Caption { get; set; }

        public Student(NodeReference targetNode, Payloads.Payload payload)
            : base(targetNode, payload)
        { }

        public override string RelationshipTypeKey
        {
            get { return TypeKey; }
        }
    }
}