using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Net;
using API_JSON_Parser;

namespace API_JSON_Parser.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CreateRequest()
        {
            JSON parser = new JSON();
            string location = JSON.CreateRequest("Macon");
            Assert.IsInstanceOfType(location, typeof(string));
        }

        [TestMethod]
        public void MakeRequest()
        {
            JSON parser = new JSON();
            string location = JSON.CreateRequest("Macon");
            Response locationsResponse = JSON.MakeRequest(location);
            Assert.IsInstanceOfType(locationsResponse, typeof(Response));

        }

        [TestMethod]
        public void parseresponse()
        {

            // Tested without the last readkey line
            JSON parser = new JSON();
            string location = JSON.CreateRequest("Macon");
            Response locationsResponse = JSON.MakeRequest(location);
            JSON.ProcessResponse(locationsResponse);
            Assert.IsNotNull(parser);
        }
    }
}
