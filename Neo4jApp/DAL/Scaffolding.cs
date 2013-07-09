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
            // Movies
            SortClass swI = new SortClass() { Name = "TiddlyWinks", Description = "How to waste one's time" };

            var tiddlywinks = client.Create(swI,
                new IRelationshipAllowingParticipantNode<SortClass>[0],
                new[]
                {
                    new IndexEntry("SortClass")
                    {
                        { "Name", swI.Name },
                        { "Description", swI.Description },
                        { "Id", swI.Id.ToString() }
                    }
                });

            SortClass swIV = new SortClass() { Name = "Neo4j", Description = "Graph databases for everyone" };

            var neo4j = client.Create(swIV,
                new IRelationshipAllowingParticipantNode<SortClass>[0],
                new[]
                {
                    new IndexEntry("SortClass")
                    {
                        { "Name", swIV.Name },
                        { "Description", swIV.Description },
                        { "Id", swIV.Id.ToString() }
                    }
                });


            // SortUsers

            SortUser joeUser = new SortUser() { Name = "Joe User" };

            var joeUserNode = client.Create(joeUser,
                new IRelationshipAllowingParticipantNode<SortUser>[0],
                new[]
                {
                    new IndexEntry("SortUser")
                    {
                        { "Name", joeUser.Name },
                        { "Id", joeUser.Id.ToString() }
                    }
                });

            SortUser wiltbank = new SortUser() { Name = "Lee Wiltbank" };

            var leeWiltbank = client.Create(wiltbank,
                new IRelationshipAllowingParticipantNode<SortUser>[0],
                new[]
                {
                    new IndexEntry("SortUser")
                    {
                        { "Name", wiltbank.Name },
                        { "Id", wiltbank.Id.ToString() }
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

        private void CreateMoviesNodesRelationshipsIndexes()
        {
            // Create Indexes
            client.CreateIndex("Movie", new IndexConfiguration() { Provider = IndexProvider.lucene, Type = IndexType.fulltext }, IndexFor.Node); // full text node index
            client.CreateIndex("Director", new IndexConfiguration() { Provider = IndexProvider.lucene, Type = IndexType.exact }, IndexFor.Node); // exact node index
            client.CreateIndex("Genre", new IndexConfiguration() { Provider = IndexProvider.lucene, Type = IndexType.fulltext }, IndexFor.Node); // full text node index

            // Create Entities
            // Movies
            Movie swI = new Movie() { Name = "Star Wars: Episode I - The Phantom Menace", Description = "Begins the story of Anakin Skywalker" };

            var starWarsEpisodeI = client.Create(swI,
                new IRelationshipAllowingParticipantNode<Movie>[0],
                new []
                {
                    new IndexEntry("Movie")
                    {
                        { "Name", swI.Name },
                        { "Description", swI.Description },
                        { "Id", swI.Id.ToString() }
                    }
                });

            Movie swIV = new Movie() { Name = "Star Wars: Episode IV - A New Hope", Description = "First Starwars movie to debut on the big screen" };

            var starWarsEpisodeIV = client.Create(swIV,
                new IRelationshipAllowingParticipantNode<Movie>[0],
                new []
                {
                    new IndexEntry("Movie")
                    {
                        { "Name", swIV.Name },
                        { "Description", swIV.Description },
                        { "Id", swIV.Id.ToString() }
                    }
                });

            Movie indy = new Movie() { Name = "Indiana Jones and the Temple of Doom", Description = "Second movie in the original Indiana Jones trilogy" };

            var indianaJonesTempleOfDoom = client.Create(indy,
                new IRelationshipAllowingParticipantNode<Movie>[0],
                new[]
                {
                    new IndexEntry("Movie")
                    {
                        { "Name", indy.Name },
                        { "Description", indy.Description },
                        { "Id", indy.Id.ToString() }
                    }
                });

            Movie jp = new Movie() { Name = "Jurassic Park", Description = "First Jurassic park movie" };

            var jurassicPark = client.Create(jp,
                new IRelationshipAllowingParticipantNode<Movie>[0],
                new[]
                {
                    new IndexEntry("Movie")
                    {
                        { "Name", jp.Name },
                        { "Description", jp.Description },
                        { "Id", jp.Id.ToString() }
                    }
                });

            Movie et = new Movie() { Name = "ET", Description = "ET phone home" };

            var ET = client.Create(et,
                new IRelationshipAllowingParticipantNode<Movie>[0],
                new[]
                {
                    new IndexEntry("Movie")
                    {
                        { "Name", et.Name },
                        { "Description", et.Description },
                        { "Id", et.Id.ToString() }
                    }
                });

            // Directors

            Director lucas = new Director() { Name = "George Lucas" };

            var georgeLucas = client.Create(lucas,
                new IRelationshipAllowingParticipantNode<Director>[0],
                new[]
                {
                    new IndexEntry("Director")
                    {
                        { "Name", lucas.Name },
                        { "Id", lucas.Id.ToString() }
                    }
                });

            Director spielberg = new Director() { Name = "Steven Spielberg" };

            var stevenSpielberg = client.Create(spielberg,
                new IRelationshipAllowingParticipantNode<Director>[0],
                new[]
                {
                    new IndexEntry("Director")
                    {
                        { "Name", spielberg.Name },
                        { "Id", spielberg.Id.ToString() }
                    }
                });

            // Genres
            Genre sf = new Genre() { Name = "Science Fiction" };

            var sciFi = client.Create(sf,
                new IRelationshipAllowingParticipantNode<Genre>[0],
                new[]
                {
                    new IndexEntry("Genre")
                    {
                        { "Name", sf.Name },
                        { "Id", sf.Id.ToString() }
                    }
                });

            Genre adv = new Genre() { Name = "Adventure" };

            var adventure = client.Create(adv,
                new IRelationshipAllowingParticipantNode<Genre>[0],
                new[]
                {
                    new IndexEntry("Genre")
                    {
                        { "Name", adv.Name },
                        { "Id", adv.Id.ToString() }
                    }
                });

            // Create Relationships
            client.CreateRelationship(starWarsEpisodeI, new PartOf(sciFi));
            client.CreateRelationship(starWarsEpisodeIV, new PartOf(sciFi));

            client.CreateRelationship(indianaJonesTempleOfDoom, new PartOf(adventure));
            
            client.CreateRelationship(jurassicPark, new PartOf(sciFi));
            client.CreateRelationship(jurassicPark, new PartOf(adventure));

            client.CreateRelationship(ET, new PartOf(sciFi));

            client.CreateRelationship(georgeLucas, new Directed(starWarsEpisodeI, new Payload() { Comment = "George Lucas' second Star Wars trilogy" }));
            client.CreateRelationship(georgeLucas, new Directed(starWarsEpisodeIV, new Payload() { Comment = "First Starwars movie that George Lucas directed" }));
            client.CreateRelationship(georgeLucas, new Directed(indianaJonesTempleOfDoom, new Payload() { Comment = "Lucas collaborated with Spielberg while filming" }));

            client.CreateRelationship(stevenSpielberg, new Directed(jurassicPark, new Payload() { Comment = "Huge box office success" }));
            client.CreateRelationship(stevenSpielberg, new Directed(ET, new Payload() { Comment = "One of Spielberg's most successful movies" }));
        }
    }
}