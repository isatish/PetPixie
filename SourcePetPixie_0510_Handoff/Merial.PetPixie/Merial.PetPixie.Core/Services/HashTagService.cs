using Merial.PetPixie.Core.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using KHashTag = Merial.PetPixie.Core.Models.Kinvey.KHashTag;

namespace Merial.PetPixie.Core.Services
{
    public class HashTagService : KinveyServiceBase, IHashTagService
    {
        public HashTagService(IKinveyService kinveyService) : base(kinveyService)
        {
        }

        public async Task<List<KHashTag>> GetAllAsync()
        {
            List<KHashTag> allHashTags = new List<KHashTag>();
            try
            {
                allHashTags = await base.GetAppDataAsync<KHashTag>("HashTag");
            }
            catch (Exception e)
            {
                _logger.Error(e);
                Debug.WriteLine(e.Message);
            }
            return allHashTags;
        }

        public async Task<List<KHashTag>> GetTagByTextAsync(string text)
        {
			if (text.StartsWith("#", StringComparison.CurrentCulture) && text.Length > 0)
				text = text.Substring(1);

            var regex = string.Format(@"{{""text"":{{""$regex"":""^.*?(?i){0}""}}}}", text);

            var tags = await GetAppDataAsync<KHashTag>("HashTag",regex);
            return tags;
        }
    }
}