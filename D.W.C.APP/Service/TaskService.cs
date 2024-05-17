
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using D.W.C.Lib.D.W.C.Models;

namespace D.W.C.APP.Service
{
    public class TaskService
    {
        private HttpClient httpClient = new HttpClient();

        public async Task<List<WorkItemDetails>> GetTasksAsync()
        {
            var response = await httpClient.GetAsync("https://devworkcalc.azurewebsites.net/api/ItemDet");
            response.EnsureSuccessStatusCode();
            var tasks = await response.Content.ReadFromJsonAsync<List<WorkItemDetails>>(); 
            return tasks;
        }
    }
}
