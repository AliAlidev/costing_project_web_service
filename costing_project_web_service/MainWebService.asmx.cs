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

        [WebMethod]
        public string FillDataTable(string query)
        {
            DataSet ds = DBVariables.fillDataTable(query);
            return JsonConvert.SerializeObject(ds);
        }

        [WebMethod]
        public string ExecuteScaler(string query)
        {
            string res = DBVariables.executescaler(query);
            return JsonConvert.SerializeObject(res);
        }

        [WebMethod]
        public string IsFound(string data)
        {
            string[] values = JsonConvert.DeserializeObject<string[]>(data);
            string value = values[0];
            string column = values[1];
            string table = values[2];
            return JsonConvert.SerializeObject(DBVariables.isFound(value, column, table));
        }

    }
}
