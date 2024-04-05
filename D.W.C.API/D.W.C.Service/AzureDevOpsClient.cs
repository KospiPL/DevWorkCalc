using D.W.C.Lib.D.W.C.Models;
using DevWorkCalc.D.W.C.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace D.W.C.Lib
{
    public class AzureDevOpsClient
    {
        private readonly HttpClient _httpClient;
        private readonly AzureDevOpsSettings _settings;
        private readonly string _baseUrl;

        public AzureDevOpsClient(HttpClient httpClient, IOptions<AzureDevOpsSettings> settings)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
            _baseUrl = $"https://dev.azure.com/{_settings.Organization}/{_settings.Project}/{_settings.Team}/";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($":{_settings.PersonalAccessToken}")));
        }

        public async Task<IterationsListdto> GetLatestIterationIdAsync()
        {
            var url = $"{_baseUrl}_apis/work/teamsettings/iterations?api-version=7.2-preview.1";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            var o = JsonConvert.DeserializeObject<IterationsListdto>(jsonString);
            return o;
        }

        public async Task<WorkItemsListDto> GetWorkItemsFromSprintAsync(string iterationId)
        {
            if (string.IsNullOrWhiteSpace(iterationId))
            {
                throw new ArgumentException("Iteration ID cannot be null or empty.", nameof(iterationId));
            }

            var url = $"{_baseUrl}_apis/work/teamsettings/iterations/{iterationId}/workitems?api-version=7.2-preview.1";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            var workItemsListDto = JsonConvert.DeserializeObject<WorkItemsListDto>(jsonString);

            workItemsListDto.WorkItemRelations.ForEach(relation => relation.SprintId = iterationId);

            return workItemsListDto;
        }


        public async Task<WorkDetailsDto> GetWorkItemDetailsExtendedAsync(int workItemId)
        {
            var url = $"https://dev.azure.com/gearcodegit/GC.BAT/_apis/wit/workitems?ids={workItemId}&api-version=7.2-preview.3";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            var o = JsonConvert.DeserializeObject<WorkDetailsDto>(jsonString);
            return o;
        }

        public async Task UpdateWorkItemAsync(int workItemId, string jsonPatchDocument)
        {
            if (string.IsNullOrWhiteSpace(jsonPatchDocument))
            {
                throw new ArgumentException("JSON Patch Document cannot be null or empty.", nameof(jsonPatchDocument));
            }

            var url = $"{_baseUrl}_apis/wit/workitems/{workItemId}?api-version=7.2-preview.3";
            var content = new StringContent(jsonPatchDocument, System.Text.Encoding.UTF8, "application/json-patch+json");
            var response = await _httpClient.PatchAsync(url, content);
            response.EnsureSuccessStatusCode();
        }
        public async Task<WorkItemHistoryListDto> GetWorkItemHistoryAsync(int workItemId)
        {
            var url = $"https://dev.azure.com/gearcodegit/GC.BAT/_apis/wit/workItems/{workItemId}/updates?api-version=6.0";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            var o = JsonConvert.DeserializeObject<WorkItemHistoryListDto>(jsonString);
            return o;
        }
        //Dodawanie i modyfikacje i usuwanie poszczegulnego zadania
        public async Task<WorkItemDetailsDto> CreateWorkItemAsync(string workItemType, Dictionary<string, object> fields)
        {
            var url = $"{_baseUrl}_apis/wit/workitems/${workItemType}?api-version=7.2-preview.3";

            var operations = fields.Select(field => new
            {
                op = "add",
                path = $"/fields/{field.Key}",
                value = field.Value
            }).ToList();

            var content = new StringContent(JsonConvert.SerializeObject(operations), System.Text.Encoding.UTF8, "application/json-patch+json");
            var response = await _httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<WorkItemDetailsDto>(jsonString);
        }
        public async Task DeleteWorkItemAsync(int workItemId)
        {
            var url = $"{_baseUrl}_apis/wit/workitems/{workItemId}?api-version=7.2-preview.3";
            var response = await _httpClient.DeleteAsync(url);
            response.EnsureSuccessStatusCode();
        }
    }
}