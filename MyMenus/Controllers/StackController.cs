using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyMenus.Controllers
{
    public class StackController : Controller
    {
        // GET: Stack

        static Stack<string> myStack = new Stack<string>();

        public ActionResult Index()
        {
            ViewBag.MyStack = myStack;

            return View();
        }

        //This method adds one new entry to the existing stack
        //The new entry is added to the beginning because it's a stack
        public ActionResult AddOne()
        {
            myStack.Push("New Entry " + (myStack.Count + 1));

            ViewBag.MyStack = myStack;

            return View("Index");
        }

        //Method that clears the data structure and adds 2,000 items in the data structure
        public ActionResult AddHuge()
        {
            //Clear previous stack (even if empty)
            myStack.Clear();

            //add 2000 items to the stack
            for (int iCount = 1; iCount < 2001; iCount++)
            {
                myStack.Push("New Entry " + iCount);
            }

            ViewBag.MyStack = myStack;

            return View("Index");
        }

        //This method checks to see if the stack is empty
        //If the stack is empty, the user sees something that says there is no stack
        //If there is a stack, it directs the user to another View page with all the stack
        public ActionResult Display()
        {
            if (myStack.Count <= 0)
            {
                ViewBag.Message = "There is nothing in the stack to display.";

                ViewBag.MyStack = myStack;

                return View("Index");
            }
            else
            {
                ViewBag.MyStack = myStack;

                return View("Display");
            }
        }

        public ActionResult Delete()
        {
            Stack<string> tempStack = new Stack<string>();
            string DeleteValue = "New Entry 10";
            int DeleteNumber = 10;
            string DeletePhrase = "New Entry ";
            ViewBag.MyStack = myStack;
            int myStackCount = myStack.Count();

            //Checks for if the entry exists in the stack
            if (DeleteNumber > myStackCount)
            {
                ViewBag.Message = "That entry does not exist";
            }
            else
            {
                for (int counter = myStackCount; counter > 1; counter--)
                {
                    if (DeleteValue != DeletePhrase + counter.ToString())
                    {
                        tempStack.Push(myStack.Peek());
                        myStack.Pop();
                    }
                    else
                    {
                        myStack.Pop();
                    }
                }
            }
            
            int tempStackCount = tempStack.Count();

            for (int counter = 0; counter < tempStackCount; counter++)
            {
                myStack.Push(tempStack.Peek());
                tempStack.Pop();
            }

            return View("Index");
        }

        //Wipes out all contents of the stack
        public ActionResult Clear()
        {
            myStack.Clear();

            ViewBag.MyStack = myStack;

            return View("Index");
        }

        //This method times how long it takes to find a certain entry within the stack
        //THe user sees how long it took to find the desired value
        public ActionResult Search()
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            string SearchValue = "New Entry 10";
            int SearchNumber = 10;
            ViewBag.MyStack = myStack;

            if (myStack.Count() < 0)
            {
                string LessThanZero = "There is nothing in your stack!";

                ViewBag.Message = LessThanZero;
            }
            else if (myStack.Count() < SearchNumber)
            {
                string LessThanTen = "There aren't enough variables in your stack!";

                ViewBag.Message = LessThanTen;
            }
            else
            {
                sw.Start();

                for (int counter = 0; counter < myStack.Count(); counter++)
                {

                    if (SearchValue == myStack.ElementAt(counter))
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

        //This method is called on the display page and returns the user to the stack page
        //the current stack is shown on the page
        public ActionResult ReturnAgain()
        {
            ViewBag.MyStack = myStack;

            return View("Index");
        }
    }
}