using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestExNunit
{
    [TestFixture]
    internal class ReqResAPITests
    {
        private RestClient client;
        private string baseUrl = "https://reqres.in/api/";
        
        [SetUp]
        public void SetUp()
        {
            client = new RestClient(baseUrl);
        }

        [Test, Order(1)]
        public void GetSingleUser()
        {
            var getUserRequest = new RestRequest("users/2", Method.Get);
            var getUserResponse = client.Execute(getUserRequest);
            Assert.That(getUserResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));

            var userdata = JsonConvert.DeserializeObject<UserDataResponse>(getUserResponse.Content);
            UserData? user = userdata?.Data;

            Assert.NotNull(user);
            Assert.That(user.Id, Is.EqualTo(2));
            Assert.IsNotEmpty(user.Email);
        }

        [Test,Order(2)]
        public void GetUsers()
        {
            var getUsersRequest = new RestRequest("users", Method.Get);
            getUsersRequest.AddQueryParameter("page", "1"); //Adding query parameter
            var getUsersResponse = client.Execute(getUsersRequest);
            Assert.That(getUsersResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
            var user = JsonConvert.DeserializeObject<UserData>(getUsersResponse.Content);
            Assert.NotNull(user);
            Console.WriteLine("get :" + getUsersResponse.Content);
        }

        [Test, Order(3)]
        public void CreateUser()
        {
            var createUserRequest = new RestRequest("users", Method.Post);
            createUserRequest.AddHeader("Content-Type", "application/json");
            createUserRequest.AddJsonBody(new { name = "John Doe", job = "Software Developer" });
            var createUserResponse = client.Execute(createUserRequest);
            Assert.That(createUserResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));
            var user = JsonConvert.DeserializeObject<UserData>(createUserResponse.Content);
            Assert.NotNull(user);
            Console.WriteLine("created" + createUserResponse.Content);
        }

        [Test, Order(4)]
        public void UpdateUser()
        {
            var updateUserRequest = new RestRequest("users/2", Method.Put);
            updateUserRequest.AddHeader("Content-Type", "application/json");
            updateUserRequest.AddJsonBody(new { name = "John Doe", job = "Software Developer" });
            var updateUserResponse = client.Execute(updateUserRequest);
            Assert.That(updateUserResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
            var user = JsonConvert.DeserializeObject<UserData>(updateUserResponse.Content);
            Assert.NotNull(user);
            Console.WriteLine("updated" + updateUserResponse.Content);
        }

        [Test, Order(5)]
        public void DeleteUser()
        {
            var deleteUserRequest = new RestRequest("users/2", Method.Delete);
            var deleteUserResponse = client.Execute(deleteUserRequest);
            Assert.That(deleteUserResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NoContent));
            Console.WriteLine("deleted " + deleteUserResponse.ResponseStatus);
        }

        [Test, Order(6)]
        public void GetSingleUsernotFound()
        {
            var getUserRequest = new RestRequest("users/23", Method.Get);
            var getUserResponse = client.Execute(getUserRequest);
            Assert.That(getUserResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NotFound));
        }
    }
}
