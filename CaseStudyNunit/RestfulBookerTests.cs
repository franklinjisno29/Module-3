using CaseStudyNunit.Utilities;
using Newtonsoft.Json;
using RestSharp;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyNunit
{
    [TestFixture]
    internal class RestfulBookerTests : CoreCodes
    {
        [Test, Order(1)]
        public void GetToken()
        {
            test = extent.CreateTest("Get Token");
            Log.Information("Get Token Test Started");
            var getTokenRequest = new RestRequest("auth", Method.Post);
            getTokenRequest.AddHeader("Content-Type", "application/json");
            getTokenRequest.AddJsonBody(new { username = "admin", password = "password123"});
            var getTokenResponse = client.Execute(getTokenRequest);
            try
            {
                Assert.That(getTokenResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information("API Response:" + getTokenResponse.Content);
                Log.Information("Token Received");
                Log.Information("Get Token test passed all Asserts");
                test.Pass("Get Token test passed all Assert");
            }
            catch (AssertionException)
            {
                test.Fail("Get Token test Failed");
            }
        }

        [Test, Order(2)]
        public void GetBookings()
        {
            test = extent.CreateTest("Get Bookings");
            Log.Information("Get Bookoings Test Started");
            var getbookingsRequest = new RestRequest("booking", Method.Get);
            var getbookingsResponse = client.Execute(getbookingsRequest);
            try
            {
                Assert.That(getbookingsResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information("API Response:" + getbookingsResponse.Content);
                List<BookingId> bookings = JsonConvert.DeserializeObject<List<BookingId>>(getbookingsResponse.Content);
                Assert.NotNull(bookings);
                Log.Information("Bookings Returned");
                Log.Information("GetBookings test passed all Asserts");
                test.Pass("Get Bookings test passed all Assert");
            }
            catch (AssertionException)
            {
                test.Fail("Get Bookings test Failed");
            }
        }

        [Test, Order(3)]
        public void GetOneBooking()
        {
            test = extent.CreateTest("Get One Booking");
            Log.Information("Get One Booking Test Started");
            var getOneBookingRequest = new RestRequest("booking/13", Method.Get);
            getOneBookingRequest.AddHeader("Accept", "application/json");
            var getOneBookingResponse = client.Execute(getOneBookingRequest);
            try
            {
                Assert.That(getOneBookingResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information("API Response:" + getOneBookingResponse.Content);
                var booking = JsonConvert.DeserializeObject<BookingData>(getOneBookingResponse.Content);
                Assert.NotNull(booking);
                Log.Information("Booking Returned");
                Assert.IsNotEmpty(booking.FirstName);
                Log.Information("Name is not empty");
                Log.Information("Get One Booking test passed all Asserts");
                test.Pass("Get One Booking test passed all Assert");
            }
            catch (AssertionException)
            {
                test.Fail("Get One Booking test Failed");
            }
        }

        [Test, Order(4)]
        public void CreateBooking()
        {
            test = extent.CreateTest("Create Booking");
            Log.Information("Create Booking Test Started");
            var createUserRequest = new RestRequest("booking", Method.Post);
            createUserRequest.AddHeader("Content-Type", "application/json");
            createUserRequest.AddHeader("Accept", "application/json");

            createUserRequest.AddJsonBody(new
            {   firstname = "Jim",
                lastname = "Brown",
                totalprice = 111,
                depositpaid = true,
                bookingdates=new 
                {   checkin = "2018-01-01", 
                    checkout = "2019-01-01" },
                additionalneeds = "Breakfast"});
            var createUserResponse = client.Execute(createUserRequest);
            try
            {
                Assert.That(createUserResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information("API Response:" + createUserResponse.Content);
                var booking = JsonConvert.DeserializeObject<BookingData>(createUserResponse.Content);
                Assert.NotNull(booking);
                Log.Information("Booking Created & Returned");
                Log.Information("Create Booking test passed all Asserts");
                test.Pass("Create Booking test passed all Assert");
            }
            catch (AssertionException)
            {
                test.Fail("Create Booking test Failed");
            }
        }

        [Test, Order(4)]
        [TestCase(2)]
        public void UpdateBooking(int uid)
        {
            test = extent.CreateTest("Update Booking");
            Log.Information("Update Booking Test Started");
            var getTokenRequest = new RestRequest("auth", Method.Post);
            getTokenRequest.AddHeader("Content-Type", "application/json");
            getTokenRequest.AddJsonBody(new 
            { username = "admin", 
                password = "password123" });
            var getTokenResponse = client.Execute(getTokenRequest);
            var token = JsonConvert.DeserializeObject<BookingData>(getTokenResponse.Content);
            var updateUserRequest = new RestRequest("booking/13", Method.Put);
            updateUserRequest.AddHeader("Content-Type", "application/json");
            updateUserRequest.AddHeader("Accept", "application/json");
            updateUserRequest.AddHeader("Cookie", "token="+token.Token);

            updateUserRequest.AddJsonBody(new
            {
                firstname = "Jim",
                lastname = "Brown",
                totalprice = 111,
                depositpaid = true,
                bookingdates = new
                {
                    checkin = "2018-01-01",
                    checkout = "2019-01-01"
                },
                additionalneeds = "Breakfast"
            });
            var updateUserResponse = client.Execute(updateUserRequest);
            try
            {
                Assert.That(updateUserResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information("API Response:" + updateUserResponse.Content);
                var user = JsonConvert.DeserializeObject<BookingData>(updateUserResponse.Content);
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
        public void DeleteBooking(int uid)
        {
            test = extent.CreateTest("Delete Booking");
            Log.Information("Delete Booking Test Started");
            var getTokenRequest = new RestRequest("auth", Method.Post);
            getTokenRequest.AddHeader("Content-Type", "application/json");
            getTokenRequest.AddJsonBody(new
            {
                username = "admin",
                password = "password123"
            });
            var getTokenResponse = client.Execute(getTokenRequest);
            var token = JsonConvert.DeserializeObject<BookingData>(getTokenResponse.Content);
            var deleteUserRequest = new RestRequest("booking/1", Method.Delete);
            deleteUserRequest.AddHeader("Content-Type", "application/json");
            deleteUserRequest.AddHeader("Cookie", "token=" + token.Token);
            var deleteUserResponse = client.Execute(deleteUserRequest);
            try
            {
                Assert.That(deleteUserResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information("Booking Deleted");
                Log.Information("Delete Booking test passed all Asserts");
                test.Pass("Delete Booking test passed all Assert");
            }
            catch (AssertionException)
            {
                test.Fail("Delete Booking test Failed");
            }
        }
    }
}
