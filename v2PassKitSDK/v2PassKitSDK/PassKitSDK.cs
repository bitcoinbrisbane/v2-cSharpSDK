/*
 * PassKit C# Wrapper for PassKit API v2:
 * https://dev.passkit.net/v2
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Jose;
using unirest_net.http;

namespace v2PassKitSDK
{
    #region Class definition for API objects (used for marshal & unmarshal)

    public class PassImages
    {
        public string background = null;
        public string footer = null;
        public string icon = null;
        public string logo = null;
        public string strip = null;
        public string thumbnail = null;
    }

    public class DynamicImages
    {
        public PassImages passbook = null;
    }

    public class DynamicBackfields
    {
        public string label = null;
        public string value = null;
        public string androidPay = null;
        public string appleWallet = null;
    }

    public class PassLocations
    {
        public double? alt = null;
        public double? lat = null;
        public double? lon = null;
        public string relevantText = null;
    }

    public class PassBeacons
    {
        public int? major = null;
        public int? minor = null;
        public string uuid = null;
        public string relevantText = null;
    }

    public class PassPassbook
    {
        public string relevantDate = null;
        public string groupId = null;
        public string bgColor = null;
        public string labelColor = null;
        public string fgColor = null;
        public PassLocations[] locations = null;
        public PassBeacons[] beacons = null;

    }

    public class Pass
    {
        public string id = null;
        public string templateName = null;
        public string campaignName = null;
        public string userDefinedId = null;
        public string recoveryEmail = null;
        public string expiryDate = null;
        public string updatedAt = null;
        public string createdAt = null;
        public string firstUnregisteredAt = null;
        public string lastUnregisteredAt = null;
        public string firstRegisteredAt = null;
        public string lastRegisteredAt = null;
        public string lastRedeemAt = null;
        public bool? isVoided = null;
        public bool? isRedeemed = null;
        public bool? isInvalid = null;
        public int? passbookDevices = null;
        public int? androidPayDevices = null;
        public Hashtable dynamicData = null;
        public DynamicImages dynamicImages = null;
        public Hashtable dynamicBackfields = null;
        public PassPassbook passbook = null;
    }

    public class PassCreateUpdateResponse 
    {
		public string id { get; set; }
    }

	public class PassCreateUpdateBatchResponse
	{
		public string[] id { get; set; }
	}

	public class PassBatch
    {
        public List<Pass> passes = null;
    }

    #endregion

    public class PassKit
    {
        private string apiKey = null;
        private string apiSecret = null;

        private string apiUrl = "https://api-pass.passkit.net";
        private string apiVersion = "v2";

        public PassKit(string apiKey, string apiSecret)
        {
            this.apiKey = apiKey;
            this.apiSecret = apiSecret;
        }

        #region Public Class Methods (all supported API calls)

        /*
         * Creates a new pass with PassKit.
         * Method specification: https://dev.passkit.net/v2#create-a-pass
         */
        public string createPass(Pass input)
        {
            try
            {
                string payload = PassToJsonString(input);
                string response = doQuery("POST", "passes", payload);

                PassCreateUpdateResponse pass = JsonConvert.DeserializeObject<PassCreateUpdateResponse>(response);

                return pass.id;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /*
         * Retrieves a pass by the PassKit pass-id.
         * Method specification: https://dev.passkit.net/v2#retrieve-a-pass
         */
        public Pass retrievePassById(string pid)
        {
            try
            {
                string response = doQuery("GET", "passes/" + pid);

                return JsonConvert.DeserializeObject<Pass>(response);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /*
         * Retrieves a pass by the user defined Id and campaign name.
         * Method specification: https://dev.passkit.net/v2#retrieve-a-pass-with-user-defined-id
         */
        public Pass retrievePassByUserDefinedId(string userDefinedId, string campaignName)
        {
            try
            {
                string response = doQuery("GET", "passes?userDefinedId=" + userDefinedId + "&campaignName" + campaignName);

                return JsonConvert.DeserializeObject<Pass>(response);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /*
         * Updates a pass by the PassKit pass-id.
         * Method specification: https://dev.passkit.net/v2#update-a-pass
         */
        public string updatePass(string pid, Pass input)
        {
            try
            {
                string payload = PassToJsonString(input);
                string response = doQuery("PUT", "passes/" + pid, payload);

                PassCreateUpdateResponse pass = JsonConvert.DeserializeObject<PassCreateUpdateResponse>(response);

                return pass.id;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /*
         * Updates a pass by the user defined Id and campaign name.
         * Method specification: https://dev.passkit.net/v2#updating-a-pass-with-a-user-defined-id
         */
        public string updatePass(string userDefinedId, string campaignName, Pass input)
        {
            try
            {
                string payload = PassToJsonString(input);
                string response = doQuery("PUT", "passes?userDefinedId=" + userDefinedId + "&campaignName" + campaignName, payload);

                PassCreateUpdateResponse pass = JsonConvert.DeserializeObject<PassCreateUpdateResponse>(response);

                return pass.id;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /*
         * Creates passes in batch (max of 25 at the time)
         * Method specification: https://dev.passkit.net/v2#batch-create-passes
         */
        public string[] createPassBatch(Pass[] inputPasses)
        {
            try
            {
                string payload = PassesToJsonString(inputPasses);
                string response = doQuery("POST", "passesBatch", payload);

                PassCreateUpdateBatchResponse passes = JsonConvert.DeserializeObject<PassCreateUpdateBatchResponse>(response);

                return passes.id;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /*
		 * Updates passes in batch (max of 25 at the time)
		 * Method specification: https://dev.passkit.net/v2#batch-update-passes
		 */
        public string[] updatePassBatch(Pass[] inputPasses)
        {
            try
            {
                string payload = PassArrayToJsonString(inputPasses);
                string response = doQuery("PUT", "passesBatch", payload);

                PassCreateUpdateBatchResponse passes = JsonConvert.DeserializeObject<PassCreateUpdateBatchResponse>(response);

                return passes.id;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /*
         * Retrieves passes in batch (max of 25 at the time)
         * Method specification: https://dev.passkit.net/v2#batch-get-passes
         */
        public Pass[] retrievePassBatch(string[] pid)
        {
            try
            {
                string response = doQuery("GET", "passesBatch?id=" + string.Join(",", pid));

                return JsonConvert.DeserializeObject<Pass[]>(response);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        #endregion

        #region Private helper methods

        /*
         * Executes the queries to the PassKit API
         */
        private string doQuery(string httpMethod, string endpoint, string payload = "")
        {
            string url = this.apiUrl + "/" + this.apiVersion + "/" + endpoint;

            HttpResponse<string> response = null;

            try
            {
                string authHeader = "PKAuth " + generateJWT();

                // Put a 5 time retry in place in case of API throttling.
                for (var i = 0; i < 5; i++)
                {
                    switch (httpMethod)
                    {
                        case "GET":
                            response = Unirest.get(url)
                                    .header("Authorization", authHeader)
                                    .asString();
                            break;
                        case "PUT":
                            if (payload != "")
                            {
                                response = Unirest.put(url)
                                    .header("Authorization", authHeader)
                                    .header("Content-Type", "application/json")
                                    .body(payload)
                                    .asString();
                            }
                            else
                            {
                                response = Unirest.put(url)
                                    .header("Authorization", authHeader)
                                    .asString();
                            }
                            break;
                        case "POST":
                            if (payload != "")
                            {
                                response = Unirest.post(url)
                                    .header("Authorization", authHeader)
                                    .header("Content-Type", "application/json")
                                    .body(payload)
                                    .asString();
                            }
                            else
                            {
                                response = Unirest.post(url)
                                    .header("Authorization", authHeader)
                                    .asString();
                            }
                            break;
                    }

                    // on success break loop and return body.
                    if (response.Code == 200)
                    {
                        break;
                    }

                    if (response.Code != 503 && response.Code == 500 && response.Code != 429)
                    {
                        throw new Exception("PassKit returned unexpected error. Code: " + response.Code + ". " +
                                            "URL: " + url + ". Request Payload: " + payload + ". " +
                                            "Request Response: " + response.Body + ".");
                    }
                    // Sleep 200ms
                    System.Threading.Thread.Sleep(200);
                }

                // It either reaches this on success, or after 5 retries. In case it's not success, then return
                // the latest error.
                if (response.Code != 200)
                {
                    throw new Exception("PassKit returned unexpected error after 5 retries. Code: " + response.Code + ". " +
                                        "URL: " + url + ". Request Payload: " + payload + ". " +
                                        "Request Response: " + response.Body + ".");
                }

                return response.Body;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private string generateJWT()
        {
            Int32 exp = (Int32)(DateTime.Now.AddSeconds(300).ToUniversalTime().Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

            var payload = new Dictionary<string, object>() {
                {"exp",exp },
                {"key",this.apiKey}
            };

            return JWT.Encode(payload, Encoding.UTF8.GetBytes(this.apiSecret), JwsAlgorithm.HS256);
        }


        private string PassToJsonString(Pass inputPass)
        {
            return JsonConvert.SerializeObject(inputPass, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        private string PassArrayToJsonString(Pass[] inputPasses)
        {
            return JsonConvert.SerializeObject(inputPasses, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        private string PassesToJsonString(Pass[] inputPasses)
        {
            try
            {
                PassBatch tempPB = new PassBatch();
                tempPB.passes = new List<Pass>();
                for (int i = 0; i < inputPasses.Length; i++)
                {
                    tempPB.passes.Add(inputPasses[i]);
                }
                string inputJsonString = JsonConvert.SerializeObject(
                    tempPB,
                    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }
                );
                return inputJsonString;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
                             
        #endregion
    }
}