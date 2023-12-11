using Newtonsoft.Json.Linq;
using RestSharp;

//reqres.in API
string baseUrl = "https://reqres.in/api/";
var client = new RestClient(baseUrl);
////Get
//var getUserRequest = new RestRequest("users/2", Method.Get);
//var getUserResponse = client.Execute(getUserRequest);
//Console.WriteLine(getUserResponse.Content);
////Create
//var createUserRequest = new RestRequest("users", Method.Post);
//createUserRequest.AddParameter("name", "John Doe");
//createUserRequest.AddParameter("job", "Software Developer");
//var createUserResponse = client.Execute(createUserRequest);
//Console.WriteLine("created"+createUserResponse.Content);
////Update
//var updateUserRequest = new RestRequest("users/2", Method.Put);
//updateUserRequest.AddParameter("name", "John Doe");
//updateUserRequest.AddParameter("job", "Software Developer");
//var updateUserResponse = client.Execute(updateUserRequest);
//Console.WriteLine("updated"+updateUserResponse.Content);
////Delete
//var deleteUserRequest = new RestRequest("users/2", Method.Delete);
//var deleteUserResponse = client.Execute(deleteUserRequest);
//Console.WriteLine("deleted "+deleteUserResponse.ResponseStatus);

////GetAll
//var getUserRequest = new RestRequest("users", Method.Get);
//getUserRequest.AddQueryParameter("page", "1"); //Adding query parameter
//var getUserResponse = client.Execute(getUserRequest);
//Console.WriteLine(getUserResponse.Content);
////Create
//var createUserRequest = new RestRequest("users", Method.Post);
//createUserRequest.AddHeader("Content-Type", "application/json");
//createUserRequest.AddJsonBody(new { name = "John Doe", job = "Software Developer" });
//var createUserResponse = client.Execute(createUserRequest);
//Console.WriteLine("created" + createUserResponse.Content);
////Update
//var updateUserRequest = new RestRequest("users/2", Method.Put);
//updateUserRequest.AddHeader("Content-Type", "application/json");
//updateUserRequest.AddJsonBody(new { name = "John Doe", job = "Software Developer" });
//var updateUserResponse = client.Execute(updateUserRequest);
//Console.WriteLine("updated" + updateUserResponse.Content);
////Delete
//var deleteUserRequest = new RestRequest("users/2", Method.Delete);
//var deleteUserResponse = client.Execute(deleteUserRequest);
//Console.WriteLine("deleted " + deleteUserResponse.ResponseStatus);

GetUsers(client);
GetUser(client);
CreateUser(client);
UpdateUser(client);
DeleteUser(client);

//GetAll
static void GetUsers(RestClient client)
{
    var getUsersRequest = new RestRequest("users", Method.Get);
    getUsersRequest.AddQueryParameter("page", "1"); //Adding query parameter
    var getUsersResponse = client.Execute(getUsersRequest);
    Console.WriteLine("get :"+getUsersResponse.Content);
}

//GetUser
static void GetUser(RestClient client)
{
    var getUserRequest = new RestRequest("users", Method.Get);
    getUserRequest.AddQueryParameter("page", "1"); // Adding query parameter
    var getUserResponse = client.Execute(getUserRequest);
    if (getUserResponse.StatusCode == System.Net.HttpStatusCode.OK)
    {
        // Parse json response content
        JObject? userjson = JObject.Parse(getUserResponse?.Content);
        string? page = userjson?["page"]?.ToString();

        // Access the "data" array and its first element
        string? userName = userjson?["data"]?[1]?["first_name"]?.ToString();
        string? userLastName = userjson?["data"]?[1]?["last_name"]?.ToString();

        Console.WriteLine($"Get User Name: {page} {userName} {userLastName}");
    }
    else
    {
        Console.WriteLine($"Error: {getUserResponse.ErrorMessage}");
    }
}

//Create
static void CreateUser(RestClient client)
{
    var createUserRequest = new RestRequest("users", Method.Post);
    createUserRequest.AddHeader("Content-Type", "application/json");
    createUserRequest.AddJsonBody(new { name = "John Doe", job = "Software Developer" });
    var createUserResponse = client.Execute(createUserRequest);
    Console.WriteLine("created" + createUserResponse.Content);
}

//Update
static void UpdateUser(RestClient client)
{
    var updateUserRequest = new RestRequest("users/2", Method.Put);
    updateUserRequest.AddHeader("Content-Type", "application/json");
    updateUserRequest.AddJsonBody(new { name = "John Doe", job = "Software Developer" });
    var updateUserResponse = client.Execute(updateUserRequest);
    Console.WriteLine("updated" + updateUserResponse.Content);
}

//Delete
static void DeleteUser(RestClient client)
{
    var deleteUserRequest = new RestRequest("users/2", Method.Delete);
    var deleteUserResponse = client.Execute(deleteUserRequest);
    Console.WriteLine("deleted " + deleteUserResponse.ResponseStatus);
}

