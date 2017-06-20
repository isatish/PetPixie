using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Common.Logging;
using KinveyXamarin;
using Merial.PetPixie.Core.Services.Contracts;
using SQLite.Net.Interop;

namespace Merial.PetPixie.Core.Services
{
    public class KinveyService : IKinveyService
    {
        private readonly ILog _logger;
        private Client _client;

        public KinveyService(ILog logger)
        {
            _logger = logger;
        }

        public void CreateClient(string filePath, ISQLitePlatform platform)
        {
            _client = new Client.Builder(Constants.Kinvey.KeyApp, Constants.Kinvey.KeySecret)
                       .SetProjectId(Constants.Google.SenderId)
                       .setLogger(msg =>
                       {
                           _logger?.Trace($"[KINVEY] : {msg}");
                           Debug.WriteLine("KINVEY: " + msg);
                       })
                       .setFilePath(filePath)
                       .setOfflinePlatform(platform)
                       .build();

            _client.SetClientAppVersion(1,0,0); // Send the version on the App
#if DEBUG
            //dsmvxTask.Run(async () =>
           //dsmvx {
               //dsmvx   await VerifySetUp();
            //dsmvx});
#endif
        }

        //dsmvx public async Task VerifySetUp()
        //{
        //    try
        //    {
        //        //PingResponse response = await _client.PingAsync();
        //    }
        //    catch (Exception)
        //    {
        //        //an error has occured
        //    }
        //}

        public Client GetClient()
        {
            return _client;
        }
    }
}
