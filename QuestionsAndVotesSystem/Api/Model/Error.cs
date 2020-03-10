using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuestionsAndVotesSystem.Api.Model
{
    public class Error
    {
        public string Title { get; set; }

        public string Message { get; set; }
        public bool Link { get; set; }
        public string URL { get; set; }
        public string Img { get; set; }
        public Error()
        {
            Title = "Error";
            Message = "You Have No Access To This Page";
            Link = true;
            URL = "../index";


        }
        public Error(int Type)
        {
            Title = "Error";
            Link = true;
            URL = "../index";
            if (Type == 1)
            {
                Message = "Page Not Found";
            }
            else if (Type == 2)
            {
                Message = "No Item To Display";
            }
            else if (Type == 3)
            {
                Message = "Cann't Delete This Item It Have Other Data Refere to it";
            }
            else if (Type == 4)
            {
                Message = "this page Not found or Not for this device";
            }

            else if (Type == 6)
            {
                URL = "../home";
                Title = "";
                Img = "../assets/img/Error.jpg";
            }

            else
            {
                Message = "You Have No Access To This Page";

            }







        }

    }
}