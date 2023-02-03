using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework.Internal;
using RestSharp;
using RestSharp.Authenticators;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using TestProjectSoftUni.Models;

public class ApiTestsGitHub
{
    private RestClient _client;
    private string _username = "iochkolovski";
    private string _password = "null";

    private static bool isValidJson(object json)
    {
        try
        {
            JObject.Parse((string)json);
            return true;
        }
        catch (JsonReaderException ex)
        {
            Trace.WriteLine(ex);
            return false;
        }
    }

    [SetUp]
    public void SetUp()
    {
        this._client = new RestClient("https://api.github.com");
        this._client.Authenticator = new HttpBasicAuthenticator($"{_username}", $"{_password}");
    }

    [Test]
    public async Task Test_GitHubAPIRequest()
    {
        var request = new RestRequest("/repos/iochkolovski/Nunit-summator/issues");
        var response = await this._client.ExecuteAsync(request);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public async Task Test_GitHubAPIRequestReturnsValidBody()
    {
        var issues = await GetIssue();
        Assert.That(isValidJson(issues), Is.True);
        Assert.That(issues.Any(), Is.True);
        foreach (var issue in issues)
        {
            Assert.That(issue.Id > 0);
            Assert.That(issue.Number > 0);
            Assert.IsNotEmpty(issue.Body);
        }
    }

    [Test]
    public async Task Test_GitHubAPIParameterizedRequestReturnsValidBody()
    {
        var issue = await GetIssue(1);
        Assert.That(isValidJson(issue), Is.True);
        Assert.That(issue.Id > 0);
        Assert.That(issue.Number > 0);
        Assert.IsNotEmpty(issue.Body);
    }

    private async Task<List<Issue>> GetIssue()
    {
        var request = new RestRequest($"/repos/iochkolovski/Nunit-summator/issues/");
        var response = _client.Execute(request);
        var issue = JsonConvert.DeserializeObject<List<Issue>>(response.Content);
        return issue;
    }

    private async Task<Issue> GetIssue(int issueNumber)
    {
        var request = new RestRequest($"/repos/iochkolovski/Nunit-summator/issues/{issueNumber}");
        var response = _client.Execute(request);
        var issue = JsonConvert.DeserializeObject<Issue>(response.Content);
        return issue;
    }
    private async Task PostIssue()
    {
        var request = new RestRequest($"/repos/iochkolovski/Nunit-summator/issues/", Method.Post);
        var response = _client.Execute(request);
        var issue = JsonConvert.DeserializeObject<List<Issue>>(response.Content);
    }
}
