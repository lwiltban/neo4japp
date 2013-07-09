using Neo4jClient;
using Neo4jApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Neo4jApp.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Movies()
        {
            GraphClient client = new GraphClient(new Uri("http://localhost:7474/db/data"));
            client.Connect();

            // get all movies with any name using index
            List<Node<Movie>> list = client.QueryIndex<Movie>("Movie", IndexFor.Node, "Name: *").ToList();
            List<Movie> movies = new List<Movie>();
            foreach (Node<Movie> movieNode in list)
            {
                movies.Add(movieNode.Data);
            }
            return View(movies);
        }

        public ActionResult MovieDetails(string id)
        {
            GraphClient client = new GraphClient(new Uri("http://localhost:7474/db/data"));
            client.Connect();

            Node<Movie> movie = client.QueryIndex<Movie>("Movie", IndexFor.Node, "Id:\"" + id + "\"").FirstOrDefault();

            var directedBy = movie
                .StartCypher("n")
                .Match("n<-[r:DIRECTED]-e")
                .Return<Node<Director>>("e")
                .Results.ToList();

            ViewBag.DirectedBy = directedBy;

            return View(movie.Data);
        }

        public ActionResult Directors()
        {
            GraphClient client = new GraphClient(new Uri("http://localhost:7474/db/data"));
            client.Connect();

            // get all director with any name using index
            List<Node<Director>> list = client.QueryIndex<Director>("Director", IndexFor.Node, "Name: *").ToList();
            List<Director> directors = new List<Director>();
            foreach (Node<Director> directorNode in list)
            {
                directors.Add(directorNode.Data);
            }
            return View(directors);
        }

        public ActionResult DirectorDetails(string id)
        {
            return View();
        }

        public ActionResult Genres()
        {
            GraphClient client = new GraphClient(new Uri("http://localhost:7474/db/data"));
            client.Connect();

            // get all genres with any name using index
            List<Node<Genre>> list = client.QueryIndex<Genre>("Genre", IndexFor.Node, "Name: *").ToList();
            List<Genre> genres = new List<Genre>();
            foreach (Node<Genre> genreNode in list)
            {
                genres.Add(genreNode.Data);
            }
            return View(genres);
        }

        public ActionResult GenreDetails(string id)
        {
            return View();
        }

        public ActionResult SortClasses()
        {
            GraphClient client = new GraphClient(new Uri("http://localhost:7474/db/data"));
            client.Connect();

            // get all movies with any name using index
            List<Node<SortClass>> list = client.QueryIndex<SortClass>("SortClass", IndexFor.Node, "Name: *").ToList();
            List<SortClass> classes = new List<SortClass>();
            foreach (Node<SortClass> classNode in list)
            {
                classes.Add(classNode.Data);
            }
            return View(classes);
        }

        public ActionResult ClassDetails(string id)
        {
            GraphClient client = new GraphClient(new Uri("http://localhost:7474/db/data"));
            client.Connect();

            Node<SortClass> sortClass = client.QueryIndex<SortClass>("SortClass", IndexFor.Node, "Id:\"" + id + "\"").FirstOrDefault();

            var attendedBy = sortClass
                .StartCypher("n")
                .Match("n<-[r:ATTENDED]-e")
                .Return<Node<SortUser>>("e")
                .Results.ToList();

            ViewBag.AttendedBy = attendedBy;

            return View(sortClass.Data);
        }

        public ActionResult SortUsers()
        {
            GraphClient client = new GraphClient(new Uri("http://localhost:7474/db/data"));
            client.Connect();

            // get all director with any name using index
            List<Node<SortUser>> list = client.QueryIndex<SortUser>("SortUser", IndexFor.Node, "Name: *").ToList();
            List<SortUser> sortUsers = new List<SortUser>();
            foreach (Node<SortUser> userNode in list)
            {
                sortUsers.Add(userNode.Data);
            }
            return View(sortUsers);
        }

        public ActionResult UserDetails(string id)
        {
            GraphClient client = new GraphClient(new Uri("http://localhost:7474/db/data"));
            client.Connect();

            Node<SortUser> sortUser = client.QueryIndex<SortUser>("SortUser", IndexFor.Node, "Id:\"" + id + "\"").FirstOrDefault();

            var studentIn = sortUser
                .StartCypher("n")
                .Match("n<-[r:STUDENT]-e")
                .Return<Node<SortClass>>("e")
                .Results.ToList();

            ViewBag.StudentIn = studentIn;

            return View(sortUser.Data);
        }

    }
}
