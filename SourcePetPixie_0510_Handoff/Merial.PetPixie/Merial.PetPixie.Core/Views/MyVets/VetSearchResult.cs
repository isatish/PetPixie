using System;
using Merial.PetPixie.Core.Models.Kinvey;

namespace Merial.PetPixie.Core
{
    public class SearchResult
    {
        //
        // Constructors
        //
        public SearchResult() { }

        //
        // Methods
        //
        public bool IsMatch(string text) { return true; }

        //
        // Nested Types
        //
        public enum SearchType
        {
            Text,
            Maps,
            Custom
        }

    }

    public class VetSearchResult : SearchResult
    {
        public KVet Vet { get; }
        public string Title { get; }
        public SearchType Type { get; }
        public string Icon { get; }

        public VetSearchResult(KVet vet,  string iconName)
        {
            this.Vet = vet;
            Title = vet.Name;
            Type = SearchType.Custom;
       //     Icon = resources.GetDrawable(Resource.Drawable.selectedplace, theme);
        }
    }
}
