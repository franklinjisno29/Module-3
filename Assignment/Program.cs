using Newtonsoft.Json.Linq;
using RestSharp;

string baseUrl = "https://jsonplaceholder.typicode.com";
var client = new RestClient(baseUrl);

GetAll(client);
GetUser(client);
CreateUser(client);
UpdateUser(client);
DeleteUser(client);

//GetAll
static void GetAll(RestClient client)
{
    var getUsersRequest = new RestRequest("posts", Method.Get);
    var getUsersResponse = client.Execute(getUsersRequest);
    Console.WriteLine("get :" + getUsersResponse.Content);
}

//GetUser
static void GetUser(RestClient client)
{
    var getUserRequest = new RestRequest("posts/1", Method.Get);
    var getUserResponse = client.Execute(getUserRequest);
    if (getUserResponse.StatusCode == System.Net.HttpStatusCode.OK)
    {
        // Parse json response contents
        JObject? userjson = JObject.Parse(getUserResponse?.Content);

        // Access the "data" array and its first element
        string? title = userjson?["title"]?.ToString();

        Console.WriteLine("Get title:"+title);
    }
    else
    {
        Console.WriteLine($"Error: {getUserResponse.ErrorMessage}");
    }
}

//Create
static void CreateUser(RestClient client)
{
    var createUserRequest = new RestRequest("posts", Method.Post);
    createUserRequest.AddHeader("Content-Type", "application/json");
    createUserRequest.AddJsonBody(new { userId = "10", title = "Holiday", body = "sunday" });
    var createUserResponse = client.Execute(createUserRequest);
    Console.WriteLine("created" + createUserResponse.Content);
}

//Update
static void UpdateUser(RestClient client)
{
    var updateUserRequest = new RestRequest("users/1", Method.Put);
    updateUserRequest.AddHeader("Content-Type", "application/json");
    updateUserRequest.AddJsonBody(new { userId = "10", title = "Holiday", body = "sunday" });
    var updateUserResponse = client.Execute(updateUserRequest);
    Console.WriteLine("updated" + updateUserResponse.Content);
}

//Delete
static void DeleteUser(RestClient client)
{
    var deleteUserRequest = new RestRequest("users/1", Method.Delete);
    var deleteUserResponse = client.Execute(deleteUserRequest);
    Console.WriteLine("deleted " + deleteUserResponse.ResponseStatus);
}
