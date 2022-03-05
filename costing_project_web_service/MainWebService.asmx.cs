using Newtonsoft.Json;
using oti_cost;
using System.Data;
using System.Web.Services;

namespace costing_project_web_service
{
    /// <summary>
    /// Summary description for MainWebService
    /// </summary>
    [WebService(Namespace = "http://oti-costing.sy/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class MainWebService : System.Web.Services.WebService
    {

        [WebMethod]
        public string ShowActiveCenter()
        {
            DataTable dt = DBVariables.showactivecenter();
            return JsonConvert.SerializeObject(dt);
        }

        [WebMethod]
        public string ExecuteNQ(string qry)
        {
            response res = DBVariables.executenq(qry);
            return JsonConvert.SerializeObject(res);
        }

    }
}
