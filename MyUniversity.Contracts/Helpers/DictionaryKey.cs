using System;

namespace MyUniversity.Api.Helpers
{
    public class DictionaryKey : IEquatable<DictionaryKey>
    {
        public string Navigation { get; set; }
        public string Base { get; set; }

        public bool Equals(DictionaryKey other)
        {
            if (other == null) return false;

            return other.Base == Base && other.Navigation == Navigation;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as DictionaryKey);
        }

        public override int GetHashCode()
        {
            return Navigation.GetHashCode() * Base.GetHashCode();
        }

        public DictionaryKey(string baseName, string navigationName)
        {
            this.Base = baseName;
            this.Navigation = navigationName;
        }
    }
}