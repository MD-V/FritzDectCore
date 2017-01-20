using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Xml;

namespace FritzDectCore.Helper
{
    public class FritzApiHelper
    {

        private static string _FritzUrl = "http://fritz.box";
        private static string _HomeAutoSwitchUrl = "/webservices/homeautoswitch.lua";
        private static string _LoginUrl = "/login_sid.lua";

        private static string _ResponseKeyWord = "response=";

        public FritzApiHelper()
        {
            
        }

        private async Task<string> Login(string username, string password)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync(_FritzUrl + _LoginUrl);

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(response);

            var challenge = xml.GetElementsByTagName("Challenge").Item(0).InnerText;


            var challengeWithPassword = $"{challenge}-{password}";

            MD5 md5Hash = MD5.Create();
            
            string hash = Md5Helper.GetMd5Hash(md5Hash, challengeWithPassword);


            var responseChallenge = $"{challenge}-{hash}";

            var loginResponse = await client.GetStringAsync(_FritzUrl + _LoginUrl + $"?username={username}&" + _ResponseKeyWord + responseChallenge);


            XmlDocument responseXml = new XmlDocument();
            responseXml.LoadXml(loginResponse);

            var sid = responseXml.GetElementsByTagName("SID").Item(0).InnerText;


            return sid;
        }


        public async Task<bool> TurnOn(string ain, string username, string password)
        {
            var sid = Login(username, password).Result;
            HttpClient client = new HttpClient();
            var switchOnResponse = await client.GetStringAsync(_FritzUrl + _HomeAutoSwitchUrl + $"?ain={ain}&switchcmd=setswitchon&sid={sid}");

            return true;
        }

        public async Task<bool> TurnOff(string ain, string username, string password)
        {
            var sid = Login(username, password).Result;
            HttpClient client = new HttpClient();
            var switchOnResponse = await client.GetStringAsync(_FritzUrl + _HomeAutoSwitchUrl + $"?ain={ain}&switchcmd=setswitchoff&sid={sid}");

            return true;
        }


        public async Task<string[]> SwitchList(string username, string password)
        {
            var sid = Login(username, password).Result;
            HttpClient client = new HttpClient();
            var switchOnResponse = await client.GetStringAsync(_FritzUrl + _HomeAutoSwitchUrl + $"?switchcmd=getswitchlist&sid={sid}");

            return switchOnResponse.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
        }


    }
}
