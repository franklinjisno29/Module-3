using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentNunit
{
    internal class JsonPlaceAPITests
    {
        private RestClient client;
        private string baseUrl = "https://jsonplaceholder.typicode.com";

        [SetUp]
        public void SetUp()
        {
            client = new RestClient(baseUrl);
        }

        [Test, Order(1)]
        public void GetSingleUser()
        {
            var getUserRequest = new RestRequest("posts/1", Method.Get);
            var getUserResponse = client.Execute(getUserRequest);
            Assert.That(getUserResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));

            var user = JsonConvert.DeserializeObject<UserData>(getUserResponse.Content);

            Assert.NotNull(user);
            Assert.That(user.Id, Is.EqualTo(1));
            Assert.IsNotEmpty(user.Title);
            Console.WriteLine("Get title:" + user.Title);
        }

        [Test, Order(2)]
        public void GetAllUsers()
        {
            var getUsersRequest = new RestRequest("posts", Method.Get);
            var getUsersResponse = client.Execute(getUsersRequest);
            Assert.That(getUsersResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
            List<UserData> users = JsonConvert.DeserializeObject<List<UserData>>(getUsersResponse.Content);
            //var userdata = JsonConvert.DeserializeObject<UserDataResponse>(getUsersResponse.Content);
            //UserData? user = userdata?.Data;
            Assert.NotNull(users);
            Console.WriteLine("get :" + getUsersResponse.Content);
        }

        [Test, Order(3)]
        public void CreateUser()
        {
            var createUserRequest = new RestRequest("posts", Method.Post);
            createUserRequest.AddHeader("Content-Type", "application/json");
            createUserRequest.AddJsonBody(new { userId = "10", title = "Holiday", body = "sunday" });
            var createUserResponse = client.Execute(createUserRequest);
            Assert.That(createUserResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));
            var user = JsonConvert.DeserializeObject<UserData>(createUserResponse.Content);
            Assert.NotNull(user);
            Console.WriteLine("created" + createUserResponse.Content);
        }

        [Test, Order(4)]
        public void UpdateUser()
        {
            var updateUserRequest = new RestRequest("posts/1", Method.Put);
            updateUserRequest.AddHeader("Content-Type", "application/json");
            updateUserRequest.AddJsonBody(new { userId = "10", title = "Holiday", body = "sunday" });
            var updateUserResponse = client.Execute(updateUserRequest);
            Assert.That(updateUserResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
            var user = JsonConvert.DeserializeObject<UserData>(updateUserResponse.Content);
            Assert.NotNull(user);
            Console.WriteLine("updated" + updateUserResponse.Content);
        }

        [Test, Order(5)]
        public void DeleteUser()
        {
            var deleteUserRequest = new RestRequest("posts/1", Method.Delete);
            var deleteUserResponse = client.Execute(deleteUserRequest);
            Assert.That(deleteUserResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
            Console.WriteLine("deleted " + deleteUserResponse.ResponseStatus);
        }

        [Test, Order(6)]
        public void GetSingleUsernotFound()
        {
            var getUserRequest = new RestRequest("posts/23", Method.Get);
            var getUserResponse = client.Execute(getUserRequest);
            Assert.That(getUserResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NotFound));
        }
    }
}
