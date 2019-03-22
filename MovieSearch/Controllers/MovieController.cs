using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace MovieSearch.Controllers
{
    public class MovieController : Controller
    {
        // GET: Movie
        string JSONresponseFromAPI;
        public ActionResult ShowMovie(String moviename)
        {
            //Debug.WriteLine("Movie Name:" + moviename);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.omdbapi.com/?s="+moviename+"&apikey=79c243d3");
            WebResponse response = request.GetResponse();
            JObject parsing=null;
            using (Stream responseStream = response.GetResponseStream())
            {
                    StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.UTF8);
                    JSONresponseFromAPI = reader.ReadToEnd();
                
                try
                {

                    parsing = JObject.Parse(JSONresponseFromAPI);
                    ViewBag.Title=parsing["Search"][0]["Title"].ToString();
                    ViewBag.Year = parsing["Search"][0]["Year"].ToString();
                    ViewBag.ID = parsing["Search"][0]["imdbID"].ToString();
                    ViewBag.Type = parsing["Search"][0]["Type"].ToString();
                    ViewBag.Poster = parsing["Search"][0]["Poster"].ToString();

                }
                catch (Exception e)
                {
                    Debug.WriteLine("Error");
                    ViewBag.message = "Looks like there's a spelling error!";
                }
            }
            //Debug.WriteLine("Debug Log:" + JSONresponseFromAPI);

            return View();
        }
    }
}