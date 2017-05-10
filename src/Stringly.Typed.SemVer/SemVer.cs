using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using StringlyTyped;

namespace Stringly.Typed.SemVer
{
    public class SemVer : StringlyPattern<SemVer>
    {
        public int Major { get; private set; }

        protected override Regex Regex => new Regex(@"\d+");
        protected override SemVer ParseFromRegex(Match match)
        {
            return null;
        }
    }
}
