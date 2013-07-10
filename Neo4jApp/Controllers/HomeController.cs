using Neo4jClient;
using Neo4jApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Neo4jClient.Cypher;

namespace Neo4jApp.Controllers
{
    public class HomeController : Controller
    {

        GraphClient m_Client;
        GraphClient Client
        {
            get
            {
                if (m_Client == null)
                {
                    m_Client = new GraphClient(new Uri("http://localhost:7474/db/data"));
                    m_Client.Connect();
                }
                return m_Client;
            }
        }
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult All()
        {
            var query = Client.Cypher.Start("n", "node(*)").Return<Node<Entity>>("n");

            var list = query.Results;
            List<Entity> classes = new List<Entity>();
            foreach (Node<Entity> node in list)
            {
                classes.Add(node.Data);
            }
            return View(classes);
        }

        public ActionResult SortClasses()
        {
            var query = Client.Cypher.Start(new { foo = Node.ByIndexQuery("SortClass", "name: *") }).Return<Node<SortClass>>("*");

            var list = query.Results; 
            List<SortClass> classes = new List<SortClass>();
            foreach (Node<SortClass> classNode in list)
            {
                classes.Add(classNode.Data);
            }
            return View(classes);
        }

        public ActionResult ClassDetails(string id)
        {
            var sortClass = Client.Cypher.Start(new { foo = Node.ByIndexQuery("SortClass", "id: " + id) }).Return<Node<SortClass>>("*").Results.FirstOrDefault();

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
            // get all director with any name using index
            var query = Client.Cypher.Start(new { foo = Node.ByIndexQuery("SortUser", "name: *") }).Return<Node<SortUser>>("*");
            var list = query.Results;

            List<SortUser> sortUsers = new List<SortUser>();
            foreach (Node<SortUser> userNode in list)
            {
                sortUsers.Add(userNode.Data);
            }
            return View(sortUsers);
        }

        public ActionResult UserDetails(string id)
        {
            var sortUser = Client.Cypher.Start(new { foo = Node.ByIndexQuery("SortUser", "id: "+id) }).Return<Node<SortUser>>("*").Results.FirstOrDefault();

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
