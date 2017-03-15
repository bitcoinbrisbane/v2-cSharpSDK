/** PassKit C# SDK for API CORE v2
*
* @author  Apoorva Katta
* @email apoorvakatta@gmail.com
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

    public class PassBatch
    {
        public List<Pass> passes = null;
    }

    public class Search
    {
        public int? size = null;
        public int? from = null;
        public PassFilter passFilter = null;
    }

    public class SearchResult
    {
        public int? nextFrom = null;
        public int? totalCount = null;
        public Pass[] data = null;
    }

    public class PassFilter
    {
        public string id = null;
        public string templateName = null;
        public string campaignName = null;

        public bool? isVoided = null;
        public bool? isRedeemed = null;
        public bool? isInvalid = null;

        public Operation userDefinedId = null;
        public Operation expiryDate = null;
        public Operation updatedAt = null;
        public Operation createdAt = null;
        public Operation firstUnregisteredAt = null;
        public Operation lastUnregisteredAt = null;
        public Operation firstRegisteredAt = null;
        public Operation lastRegisteredAt = null;
        public Operation lastRedeemAt = null;
        public Operation recoveryEmail = null;
        public Operation passbookDevices = null;
        public Operation androidPayDevices = null;

        public Hashtable dynamicData = null;
    }

    public class Operation
    {
        public object gt = null;
        public object gte = null;
        public object lt = null;
        public object lte = null;
        public object eq = null;
        public bool? exists = null;
    }

    public class PassKit
    {
        private string apiKey = null;
        private string apiSecret = null;

        private string apiPassUrl = "https://api-pass.passkit.net";
        private string apiSearchUrl = "https://search.passkit.net";
        private string apiPassVersion = "v2";
        private string apiSearchVersion = "v1";

        public PassKit(string apiKey, string apiSecret)
        {
            this.apiKey = apiKey;
            this.apiSecret = apiSecret;
        }

        public string doQuery(string apiMode, string path, string requestType)
        {
            string url = null;
            if (apiMode == "PASS") { url = this.apiPassUrl + "/" + this.apiPassVersion + "/" + path; }
            else if (apiMode == "SEARCH") { url = this.apiSearchUrl + "/" + this.apiSearchVersion + "/" + path; }
            else { return null; }

            HttpResponse<string> response = null;
            try
            {
                for (var i = 1; i < 4; i++)
                {
                    if (requestType == "GET")
                    {
                        response = Unirest.get(url)
                                .header("Authorization", "PKAuth " + createJWT(apiKey, apiSecret))
                                .asString();
                    }
                    else if (requestType == "PUT")
                    {
                        response = Unirest.put(url)
                                .header("Authorization", "PKAuth " + createJWT(apiKey, apiSecret))
                                .asString();
                    }

                    if (JsonConvert.DeserializeObject(response.Body).ToString().Substring(15, 13) != "Invalid token")
                    { i = 4; }
                    //else
                    //{ Console.WriteLine("Invalid Token: Try " + i + " out of 3"); }
                }

                if ((400 <= response.Code) && (response.Code < 500))
                {
                    throw new Exception("[ " + response.Code + ": " + response.Body + " ]");
                }

                if ((500 <= response.Code) && (response.Code < 600))
                {
                    throw new Exception("[ " + response.Code + ": " + response.Body + " ]");
                }
                return response.Body;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string doQuery(string apiMode, string path, string jsonString, string requestType)
        {
            string url = null;
            if (apiMode == "PASS") { url = this.apiPassUrl + "/" + this.apiPassVersion + "/" + path; }
            else if (apiMode == "SEARCH") { url = this.apiSearchUrl + "/" + this.apiSearchVersion + "/" + path; }
            else { return null; }

            HttpResponse<string> response = null;
            try
            {
                for (var i = 1; i < 4; i++)
                {

                    if (requestType == "POST")
                    {
                        response = Unirest.post(url)
                                .header("Authorization", "PKAuth " + createJWT(apiKey, apiSecret))
                                .header("Content-Type", "application/json")
                                .body(jsonString)
                                .asString();
                    }
                    else if (requestType == "PUT")
                    {
                        response = Unirest.put(url)
                                .header("Authorization", "PKAuth " + createJWT(apiKey, apiSecret))
                                .header("Content-Type", "application/json")
                                .body(jsonString)
                                .asString();
                    }

                    if (JsonConvert.DeserializeObject(response.Body).ToString().Substring(15, 13) != "Invalid token")
                    { i = 4; }
                    //else
                    //{ Console.WriteLine("Invalid Token: Try " + i + " out of 3"); }
                }

                if ((400 <= response.Code) && (response.Code < 500))
                {
                    throw new Exception("[ " + response.Code + ": " + response.Body + " ]");
                }

                if ((500 <= response.Code) && (response.Code < 600))
                {
                    throw new Exception("[ " + response.Code + ": " + response.Body + " ]");
                }
                return response.Body;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private string createJWT(string apiKey, string apiSecret)
        {
            var payload = new Dictionary<string, object>()
            {
                {"exp",(int)((DateTime.Now.AddSeconds(30)).ToUniversalTime().Subtract(new DateTime(1970, 1, 1))).TotalSeconds},
                {"key",apiKey}
            };
            return JWT.Encode(payload, Encoding.UTF8.GetBytes(apiSecret), JwsAlgorithm.HS256);
        }

        public string createPass(Pass inputPass)
        {
            try
            {
                string inputJsonString = PassToJsonString(inputPass);
                dynamic output = doQuery("PASS", "passes", inputJsonString, "POST");
                return JsonConvert.DeserializeObject(output)["id"].ToObject<string>();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Pass retrievePass(string passId)
        {
            try
            {
                string output = doQuery("PASS", "passes/" + passId, "GET");
                return JsonStringToPass(output);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Pass retrievePass(string userDefinedId, string campaignName)
        {
            try
            {
                string output = doQuery("PASS", "passes?userDefinedId=" + userDefinedId + "&campaignName" + campaignName, "GET");
                return JsonStringToPass(output);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string updatePass(string passId, Pass inputPass)
        {
            try
            {
                string inputJsonString = PassToJsonString(inputPass);
                dynamic output = doQuery("PASS", "passes/" + passId, inputJsonString, "PUT");
                return JsonConvert.DeserializeObject(output)["id"].ToObject<string>();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string updatePass(string userDefinedId, string campaignName, Pass inputPass)
        {
            try
            {
                string inputJsonString = PassToJsonString(inputPass);
                dynamic output = doQuery("PASS", "passes?userDefinedId=" + userDefinedId + "&campaignName" + campaignName, inputJsonString, "PUT");
                return JsonConvert.DeserializeObject(output)["id"].ToObject<string>();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string redeemPass(string passId)
        {
            try
            {
                dynamic output = doQuery("PASS", "passes/" + passId + "/redeem", "PUT");
                return JsonConvert.DeserializeObject(output)["id"].ToObject<string>();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string[] createPassBatch(Pass[] inputPasses)
        {
            try
            {
                string inputJsonString = PassesToJsonString(inputPasses);
                dynamic output = doQuery("PASS", "passesBatch", inputJsonString, "POST");
                return JsonConvert.DeserializeObject(output)["id"].ToObject<string[]>();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Pass[] retrievePassBatch(string[] inputPassNames)
        {
            try
            {
                dynamic output = doQuery("PASS", "passesBatch?id=" + string.Join(",", inputPassNames), "GET");
                return JsonStringToPassArray(output);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string[] updatePassBatch(Pass[] inputPasses)
        {
            try
            {
                string inputJsonString = PassArrayToJsonString(inputPasses);
                dynamic output = doQuery("PASS", "passesBatch", inputJsonString, "PUT");
                return JsonConvert.DeserializeObject(output)["id"].ToObject<string[]>();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public SearchResult searchPass(Search inputSearch)
        {
            try
            {
                string inputJsonString = SearchToJsonString(inputSearch);
                dynamic output = doQuery("SEARCH", "passes", inputJsonString, "POST");
                return JsonStringToSearchResult(output);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string PassToJsonString(Pass inputPass)
        {
            return JsonConvert.SerializeObject(inputPass, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        public string PassArrayToJsonString(Pass[] inputPasses)
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

        public string SearchToJsonString(Search inputSearch)
        {
            return JsonConvert.SerializeObject(inputSearch, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        public string SearchResultToJsonString(SearchResult inputSearchResult)
        {
            return JsonConvert.SerializeObject(inputSearchResult, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        public Pass JsonStringToPass(string inputJsonString)
        {
            return JsonConvert.DeserializeObject<Pass>(inputJsonString);
        }

        public Pass[] JsonStringToPassArray(string inputJsonString)
        {
            return JsonConvert.DeserializeObject<Pass[]>(inputJsonString);
        }

        public SearchResult JsonStringToSearchResult(string inputJsonString)
        {
            return JsonConvert.DeserializeObject<SearchResult>(inputJsonString);
        }
    }
}