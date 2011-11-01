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
            string options = context.Request["options"];
            //input = "test";
            //pattern = @"\w";

            context.Response.Clear();

            if (!string.IsNullOrEmpty(pattern))
            {

                RegexOptions regOptions = new RegexOptions();

                foreach (string op in options.Split(','))
                {
                    switch (op)
                    {
                        case "IgnoreCase":
                            regOptions |= RegexOptions.IgnoreCase;
                            break;
                        case "CheckCult":
                            regOptions |= RegexOptions.CultureInvariant;
                            break;
                        case "ExplicitCapture":
                            regOptions |= RegexOptions.ExplicitCapture;
                            break;
                        case "IgnorePatterWhiteSpace":
                            regOptions |= RegexOptions.IgnorePatternWhitespace;
                            break;
                        case "RightToLeft":
                            regOptions |= RegexOptions.RightToLeft;
                            break;
                        case "SingleLine":
                            regOptions |= RegexOptions.Singleline;
                            break;
                    }
                }
               
                Regex regex = new Regex(pattern,regOptions);
                List<string> resultList = new List<string>();

                foreach (Match item in regex.Matches(input))
                {
                    resultList.Add(item.Value);
                }
                string json = JsonConvert.SerializeObject(resultList);


                context.Response.StatusCode = 200;
                context.Response.ContentType = "application/json";
                if (context.Request["callback"]!= null)
                {
                    context.Response.Write(context.Request["callback"] + "(" + json + ")");
                }
                else
                {
                    context.Response.Write(json);
                }
                
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