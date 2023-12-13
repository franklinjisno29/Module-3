using AssignmentNunit.Utilities;
using Newtonsoft.Json;
using RestSharp;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentNunit
{
    [TestFixture]
    internal class JsonPlcaeTests : CoreCodes
    {
        [Test, Order(1)]
        [TestCase(2)]
        public void GetSingleUser(int uid)
        {
            test = extent.CreateTest("Get Single User");
            Log.Information("GetSingleUser Test Started");
            var getUserRequest = new RestRequest("posts/1", Method.Get);
            var getUserResponse = client.Execute(getUserRequest);
            try
            {
                Assert.That(getUserResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information("API Response:" + getUserResponse.Content);
                var user = JsonConvert.DeserializeObject<UserData>(getUserResponse.Content);
                Assert.NotNull(user);
                Log.Information("User Returned");
                Assert.That(user.Id, Is.EqualTo(1));
                Log.Information("UserId Matches with the fetch");
                Assert.IsNotEmpty(user.Title);
                Log.Information("Title is not empty");
                Log.Information("GetSingleUser test passed all Asserts");
                test.Pass("GetSingleUser test passed all Assert");
            }
            catch (AssertionException)
            {
                test.Fail("GetSingleUser test Failed");
            }
        }

        [Test, Order(2)]
        public void GetAllUsers()
        {
            test = extent.CreateTest("Get All Users");
            Log.Information("GetAllUsers Test Started");
            var getUsersRequest = new RestRequest("posts", Method.Get);
            var getUsersResponse = client.Execute(getUsersRequest);
            try
            {
                Assert.That(getUsersResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information("API Response:" + getUsersResponse.Content);
                List<UserData> users = JsonConvert.DeserializeObject<List<UserData>>(getUsersResponse.Content);
                Assert.NotNull(users);
                Log.Information("Users Returned");
                Log.Information("GetAll Users test passed all Asserts");
                test.Pass("Get All Users test passed all Assert");
            }
            catch (AssertionException)
            {
                test.Fail("Get Users test Failed");
            }
        }

        [Test, Order(3)]
        public void CreateUser()
        {
            test = extent.CreateTest("Create User");
            Log.Information("Create User Test Started");
            var createUserRequest = new RestRequest("posts", Method.Post);
            createUserRequest.AddHeader("Content-Type", "application/json");
            createUserRequest.AddJsonBody(new { userId = "10", title = "Holiday", body = "sunday" });
            var createUserResponse = client.Execute(createUserRequest);
            try
            {
                Assert.That(createUserResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));
                Log.Information("API Response:" + createUserResponse.Content);
                var user = JsonConvert.DeserializeObject<UserData>(createUserResponse.Content);
                Assert.NotNull(user);
                Log.Information("User Created & Returned");
                Log.Information("Create User test passed all Asserts");
                test.Pass("Create User test passed all Assert");
            }
            catch (AssertionException)
            {
                test.Fail("Create User test Failed");
            }
        }

        [Test, Order(4)]
        [TestCase(2)]
        public void UpdateUser(int uid)
        {
            test = extent.CreateTest("Update User");
            Log.Information("Update User Test Started");
            var updateUserRequest = new RestRequest("posts/1", Method.Put);
            updateUserRequest.AddHeader("Content-Type", "application/json");
            updateUserRequest.AddJsonBody(new { userId = "10", title = "Holiday", body = "sunday" });
            var updateUserResponse = client.Execute(updateUserRequest);
            try
            {
                Assert.That(updateUserResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information("API Response:" + updateUserResponse.Content);
                var user = JsonConvert.DeserializeObject<UserData>(updateUserResponse.Content);
                Assert.NotNull(user);
                Log.Information("User Updated & Returned");
                Log.Information("Update User test passed all Asserts");
                test.Pass("Update User test passed all Assert");
            }
            catch (AssertionException)
            {
                test.Fail("Update User test Failed");
            }
        }

        [Test, Order(5)]
        [TestCase(2)]
        public void DeleteUser(int uid)
        {
            test = extent.CreateTest("Delete User");
            Log.Information("Delete User Test Started");
            var deleteUserRequest = new RestRequest("posts/1", Method.Delete);
            var deleteUserResponse = client.Execute(deleteUserRequest);
            try
            {
                Assert.That(deleteUserResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information("User Deleted");
                Log.Information("Delete User test passed all Asserts");
                test.Pass("Delete User test passed all Assert");
            }
            catch (AssertionException)
            {
                test.Fail("Delete User test Failed");
            }
        }

        [Test, Order(6)]
        [TestCase(23)]
        public void GetSingleUsernotFound(int uid)
        {
            test = extent.CreateTest("Get Single User Not Found");
            Log.Information("GetSingleUser Not FOund Test Started");
            var getUserRequest = new RestRequest("posts/23", Method.Get);
            var getUserResponse = client.Execute(getUserRequest);
            try
            {
                Assert.That(getUserResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information("User Not Found");
                Log.Information("User Not Found test passed all Asserts");
                test.Pass("User Not Found test passed all Assert");
            }
            catch (AssertionException)
            {
                test.Fail("User Not Found test Failed");
            }
        }
    }
}
