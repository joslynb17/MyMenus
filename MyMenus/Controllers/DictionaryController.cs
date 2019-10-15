using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyMenus.Controllers
{
    public class DictionaryController : Controller
    {
        static Dictionary<string, int> myDict = new Dictionary<string, int>();

        // GET: Dictionary
        public ActionResult Index()
        {
            ViewBag.MyDictionary = myDict;

            return View();
        }

        //This method adds one new entry to the dictionary
        public ActionResult AddOne()
        {
            myDict.Add("New Entry " + (myDict.Count + 1), myDict.Count + 1);

            ViewBag.MyDictionary = myDict;

            return View("Index");
        }

        //This method clears the previous dictionary and adds 2000 new entries
        public ActionResult AddHuge()
        {
            myDict.Clear();

            for (int iCount = 1; iCount < 2001; iCount++)
            {
                myDict.Add("New Entry " + iCount, iCount);
            }

            ViewBag.MyDictionary = myDict;

            return View("Index");
        }

        //This method checks to see if there is anything in the dictinoary
        //If nothing in the dictionary, no display
        //If something in the dictionary, displays on a different page
        public ActionResult Display()
        {
            if (myDict.Count() <= 0)
            {
                ViewBag.Message = "There is nothing in the dictionary to display.";

                ViewBag.MyDictionary = myDict;

                return View("Index");
            }
            else
            {
                ViewBag.MyDictionary = myDict;

                return View("Display");
            }
        }

        //This method deletes New Entry 10 from the dictionary
        public ActionResult Delete()
        {
            string DeleteKey = "New Entry 10";

            if (myDict.ContainsKey(DeleteKey) == false)
            {
                ViewBag.Message = "That key is not in the dictionary.";
            }

            myDict.Remove(DeleteKey);



            ViewBag.MyDictionary = myDict;

            return View("Index");
        }

        //Wipes out all contents of the dictionary
        public ActionResult Clear()
        {
            myDict.Clear();

            ViewBag.MyDictionary = myDict;

            return View("Index");
        }

        //This method searches for New Entry 10 and times how long it takes
        //to find that entry
        public ActionResult Search()
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            string SearchKey = "New Entry 10";
            //int SearchNumber = 10;
            ViewBag.MyDictionary = myDict;
            sw.Start();

            bool doesContain = myDict.ContainsKey(SearchKey);

            if (doesContain == true)
            {
                sw.Stop();
                TimeSpan ts = sw.Elapsed;
                ViewBag.Message = "The elapsed time to find New Entry 10 was: " + ts;
            }
            else
            {
                ViewBag.Message = "The required entry is not in this dictionary.";
            }

            return View("Index");
        }

        //This method returns the user to the main menu when clicked on
        public ActionResult Return()
        {
            return RedirectToAction("Index", "Home");
        }

        //This method is called from the display page and returns the user to the dictionary
        //page and shows the dictionary
        public ActionResult ReturnAgain()
        {
            ViewBag.MyDictionary = myDict;

            return View("Index");
        }
    }
}