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
    }
}
