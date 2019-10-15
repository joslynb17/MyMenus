using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyMenus.Controllers
{
    public class QueueController : Controller
    {
        static Queue<string> myQueue = new Queue<string>();

        // GET: Queue
        public ActionResult Index()
        {
            ViewBag.MyQueue = myQueue;

            return View();
        }

        //This method takes the existing queue and adds an element to it
        //The element is added at the end, is it is a queue
        public ActionResult AddOne()
        {
            myQueue.Enqueue("New Entry " + (myQueue.Count + 1));

            ViewBag.MyQueue = myQueue;

            return View("Index");
        }

        //This method clears the current queue and adds 2000 new entries into a queue
        public ActionResult AddHuge()
        {
            myQueue.Clear();

            for (int iCount = 1; iCount < 2001; iCount++)
            {
                myQueue.Enqueue("New Entry " + iCount);
            }

            ViewBag.MyQueue = myQueue;

            return View("Index");
        }

        //This method checks if there are items in the queue; if there are, they are displayed
        //on another view page called Display
        public ActionResult Display()
        {
            if (myQueue.Count <= 0)
            {
                ViewBag.Message = "There is nothing in the queue to display.";

                ViewBag.MyQueue = myQueue;

                return View("Index");
            }
            else
            {
                ViewBag.MyQueue = myQueue;

                return View("Display");
            }
        }

        //This method deletes the 10th entry from the queue and shows the new queue
        //with the deleted item
        public ActionResult Delete()
        {
            Queue<string> tempQueue = new Queue<string>();
            string DeleteValue = "New Entry 10";
            int DeleteNumber = 10;
            string DeletePhrase = "New Entry ";
            ViewBag.MyQueue = myQueue;
            int myQueueCount = myQueue.Count();

            if (DeleteNumber > myQueueCount)
            {
                ViewBag.Message = "That entry does not exist.";
            }
            else
            {
                for (int counter = 1; counter < myQueueCount + 1; counter++)
                {
                    if (DeleteValue != DeletePhrase + counter.ToString())
                    {
                        tempQueue.Enqueue(myQueue.Peek());
                        myQueue.Dequeue();
                    }
                    else
                    {
                        myQueue.Dequeue();
                    }
                }
            }
            

            int tempQueueCount = tempQueue.Count();

            for (int counter = 1; counter < tempQueueCount + 1; counter++)
            {
                myQueue.Enqueue(tempQueue.Peek());
                tempQueue.Dequeue();
            }

            return View("Index");
        }

        //Wipes out all contents of the queue
        public ActionResult Clear()
        {
            myQueue.Clear();

            ViewBag.MyQueue = myQueue;

            return View("Index");
        }

        //This method searched for New Entry 10 in the queue
        //it also times how long it takes to find the entry
        //the time elapsed to find the entry is returned
        public ActionResult Search()
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            string SearchValue = "New Entry 10";
            int SearchNumber = 10;
            ViewBag.MyQueue = myQueue;

            if (myQueue.Count() < 0)
            {
                string LessThanZero = "There is nothing in your queue!";

                ViewBag.Message = LessThanZero;
            }
            else if (myQueue.Count() < SearchNumber)
            {
                string LessThanTen = "There aren't enough variables in your queue!";

                ViewBag.Message = LessThanTen;
            }
            else
            {
                sw.Start();

                for (int counter = 0; counter < myQueue.Count(); counter++)
                {

                    if (SearchValue == myQueue.ElementAt(counter))
                    {
                        sw.Stop();
                        TimeSpan ts = sw.Elapsed;
                        ViewBag.Message = "The elapsed time to find New Entry 10 was: " + ts;
                    }
                }
            }

            return View("Index");
        }

        //This method returns the user to the main menu when clicked on
        public ActionResult Return()
        {
            return RedirectToAction("Index", "Home");
        }

        //This method is called when on the Display page and brings the user back to the 
        //Queue section and shows the current queue
        public ActionResult ReturnAgain()
        {
            ViewBag.MyQueue = myQueue;

            return View("Index");
        }
    }
}