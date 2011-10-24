using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using Newtonsoft.Json;

namespace Web_Reg_Exp
{
    /// <summary>
    /// Summary description for RegExp
    /// </summary>
    public class RegExp : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string input = context.Request["input"];
            string pattern = context.Request["pattern"];

            //input = "test";
            //pattern = @"\w";

            context.Response.Clear();

            if (!string.IsNullOrEmpty(pattern))
            {

                RegexOptions test = new RegexOptions();

               
                Regex regex = new Regex(pattern);
                List<string> resultList = new List<string>();

                foreach (Match item in regex.Matches(input))
                {
                    resultList.Add(item.Value);
                }
                string json = JsonConvert.SerializeObject(resultList);


                context.Response.StatusCode = 200;
                context.Response.ContentType = "application/json";
                context.Response.Write(json);
            }
            else
            {
                context.Response.StatusCode = 400;
                context.Response.ContentType = "plain/text";
                context.Response.Write("pattern is null");
            }
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}