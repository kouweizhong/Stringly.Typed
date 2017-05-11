using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using StringlyTyped;

namespace Stringly.Typed.SemVer
{
    public class SemVer : StringlyPattern<SemVer>
    {
        public uint Major { get; private set; }
        public uint Minor { get; private set; }
        public uint Patch { get; private set; }
        public bool IsUnstable { get; private set; }
        public bool IsStable { get; private set; }
        public bool IsPreRelease { get; private set; }
        public IList<string> PreReleaseIdentifiers { get; private set; }
        public IList<string> BuildIdentifiers { get; private set; }

        protected override Regex Regex => new Regex(@"^(?<Major>[1-9]+|0)\.(?<Minor>[1-9]+|0)\.(?<Patch>[1-9]+|0)(?:\-(?<PreRelease>(?:(?:[a-zA-Z]|[1-9]+)+\.?)+))?(?:\+(?<Build>(?:(?:[a-zA-Z0-9]+)+\.?)+))?$");
        protected override SemVer ParseFromRegex(Match match)
        {
            var major = uint.Parse(match.Groups["Major"].Value);
            var minor = uint.Parse(match.Groups["Minor"].Value);
            var patch = uint.Parse(match.Groups["Patch"].Value);

            var prValue = match.Groups["PreRelease"].Success
                ? match.Groups["PreRelease"].Value
                : string.Empty;
            var pri = prValue.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

            var bValue = match.Groups["Build"].Success
                ? match.Groups["Build"].Value
                : string.Empty;
            var bi = bValue.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);


            return new SemVer(major, minor, patch, pri, bi);
        }

        public SemVer()
        {
            // Default Paramless for Stringly.Typed
        }

        public SemVer(uint major, uint minor, uint patch, IList<string> prereleaseIdentifiers = null, IList<string> buildIdentifiers = null)
        {
            Major = major;
            Minor = minor;
            Patch = patch;

            IsStable = Major > 0;
            IsUnstable = !IsStable;

            if (prereleaseIdentifiers != null && prereleaseIdentifiers.Any())
            {
                PreReleaseIdentifiers = prereleaseIdentifiers.ToList();
                IsPreRelease = true;
                IsStable = false;
                IsUnstable = true;
            }
            else
            {
                PreReleaseIdentifiers = new List<string>();
            }

            BuildIdentifiers = (prereleaseIdentifiers != null && prereleaseIdentifiers.Any())
                ? buildIdentifiers.ToList()
                : new List<string>();
        }
    }
}
