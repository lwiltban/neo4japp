using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Neo4jClient;
using Neo4jApp.Models;
using Neo4jApp.Models.Relationships;
using Neo4jApp.Models.Payloads;

namespace Neo4jApp.DAL
{
    /// <summary>
    /// Class to provide sample data for the graph
    /// </summary>
    public class Scaffolding
    {
        GraphClient client;

        public Scaffolding()
        {
            client = new GraphClient(new Uri("http://localhost:7474/db/data"));

            client.Connect();

            CreateNodesRelationshipsIndexes();  
        }

        private void CreateNodesRelationshipsIndexes()
        {
            // Create Indexes
            client.CreateIndex("SortClass", new IndexConfiguration() { Provider = IndexProvider.lucene, Type = IndexType.fulltext }, IndexFor.Node); // full text node index
            client.CreateIndex("SortUser", new IndexConfiguration() { Provider = IndexProvider.lucene, Type = IndexType.exact }, IndexFor.Node); // exact node index

            // Create Entities
            // Classes
            SortClass swI = new SortClass() { name = "TiddlyWinks", description = "How to waste one's time" };

            var tiddlywinks = client.Create(swI,
                new IRelationshipAllowingParticipantNode<SortClass>[0],
                new[]
                {
                    new IndexEntry("SortClass")
                    {
                        { "name", swI.name },
                        { "description", swI.description },
                        { "id", swI.id.ToString() }
                    }
                });

            SortClass swIV = new SortClass() { name = "Neo4j", description = "Graph databases for everyone" };

            var neo4j = client.Create(swIV,
                new IRelationshipAllowingParticipantNode<SortClass>[0],
                new[]
                {
                    new IndexEntry("SortClass")
                    {
                        { "name", swIV.name },
                        { "description", swIV.description },
                        { "id", swIV.id.ToString() }
                    }
                });


            // SortUsers

            SortUser joeUser = new SortUser() { name = "Joe User" };

            var joeUserNode = client.Create(joeUser,
                new IRelationshipAllowingParticipantNode<SortUser>[0],
                new[]
                {
                    new IndexEntry("SortUser")
                    {
                        { "name", joeUser.name },
                        { "id", joeUser.id.ToString() }
                    }
                });

            SortUser wiltbank = new SortUser() { name = "Lee Wiltbank" };

            var leeWiltbank = client.Create(wiltbank,
                new IRelationshipAllowingParticipantNode<SortUser>[0],
                new[]
                {
                    new IndexEntry("SortUser")
                    {
                        { "name", wiltbank.name },
                        { "id", wiltbank.id.ToString() }
                    }
                });


            // Create Relationships
            client.CreateRelationship(joeUserNode, new Attended(tiddlywinks, new Payload() { Comment = "Trying out the new class" }));
            client.CreateRelationship(joeUserNode, new Attended(neo4j, new Payload() { Comment = "Best of the best" }));

            client.CreateRelationship(leeWiltbank, new Attended(neo4j, new Payload() { Comment = "Better then best" }));

            client.CreateRelationship(tiddlywinks, new Student(joeUserNode, new Payload() { Comment = "Trying out the new student" }));

            client.CreateRelationship(neo4j, new Student(joeUserNode, new Payload() { Comment = "Trying out the new student" }));
            client.CreateRelationship(neo4j, new Student(leeWiltbank, new Payload() { Comment = "Best of the best" }));

        }
    }
}