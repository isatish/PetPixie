using Merial.PetPixie.Core.Models;
using System;
using System.Globalization;
using System.Linq;
using Merial.PetPixie.Core.Helpers;
using MvvmCross.Platform.Converters;

namespace Merial.PetPixie.Core.Converters
{
    public class LikeUsersValueConverter : MvxValueConverter<FeedModel, string>
    {
        protected override string Convert(FeedModel value, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            string likeUser0 = string.Empty;
            string likeUser1 = string.Empty;
            var currentUser = Settings.CurrentUserProfile;
            
            if (value.UserHasLiked)
            {
                likeUser0 = "You";
                likeUser1 = value.Likes.FirstOrDefault(u => u.From.Id != currentUser?.Id)?.From?.DisplayUsername;
            }
            else
            {
                if (value.NbLikes > 0)
                {
                    likeUser0 = value.Likes.Where(u => u.From.Id != currentUser?.Id).ToList()[0].From.DisplayUsername;
                }

                if (value.NbLikes > 1)

                    try
                    {
                       var list= value.Likes.Where(u => u.From.Id != currentUser?.Id).ToList();
                        if (list.Count() > 1)
                        {
                            likeUser1 = list[1].From.DisplayUsername;
                        }
                        else
                        {
                            return $"{value.NbLikes} buddies like this";
                        }
                    }
                    catch (Exception e)
                    {
                        var exp = e;
                    }
            }
            
#if DEBUG
            if (String.IsNullOrWhiteSpace(likeUser0))
            {
                likeUser0 = "No Name";
            }
            if (String.IsNullOrWhiteSpace(likeUser1))
            {
                likeUser1 = "No Name";
            }
#endif



            switch (value.NbLikes)
            {
                case 0:
                    return "Be the first to like this";
                case 1:
                    if (value.UserHasLiked)
                        return $"{likeUser0} like this";
                    else
                        return $"{likeUser0} likes this";
                case 2:
                    if (string.IsNullOrWhiteSpace(likeUser1))
                        return $"{likeUser0} and 1 buddy like this";
                    else
                        return $"{likeUser0} and {likeUser1} like this";
                default:
                    return $"{likeUser0} and {value.NbLikes - 1} buddies like this";
            }
        }
    }
}

