using Restup.Webserver.Attributes;
using Restup.Webserver.Models.Contracts;
using Restup.Webserver.Models.Schemas;
using System;

namespace FritzDectCoreIoT
{

    [RestController(InstanceCreationType.Singleton)]
    public sealed class FritzDectController
    {
       
        [UriFormat("/")]
        public IGetResponse GetWithSimpleParameters()
        {
            return new GetResponse(GetResponse.ResponseStatus.OK, "It works!");
        }


        [UriFormat("/TurnOn?ain={ain}&username={username}&password={password}")]
        public IGetResponse TurnOn(string ain, string username, string password)
        {
            var helper = new FritzApiHelper();

            var result = helper.TurnOn(ain, username, password).AsTask().Result;


            return new GetResponse(GetResponse.ResponseStatus.OK);
        }

        [UriFormat("/TurnOff?ain={ain}&username={username}&password={password}")]
        public IGetResponse TurnOff(string ain, string username, string password)
        {
            var helper = new FritzApiHelper();

            var result = helper.TurnOff(ain, username, password).AsTask().Result;


            return new GetResponse(GetResponse.ResponseStatus.OK);
        }
    }
}
