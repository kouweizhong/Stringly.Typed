using System;
using NUnit.Framework;
using Stringly.Typed.SemVer;

namespace StringlyTyped.Tests.Types
{
    public class SemVerTests
    {
        // http://semver.org/

        [Test]
        public void SemVer_EmptyString_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => { var sv = new Stringly<SemVer>(string.Empty); });
        }

        // TODO (RC): Major Non-Negative
        // TODO (RC): Minor Non-Negative
        // TODO (RC): Patch Non-Negative
        // TODO (RC): Major Non-Leading Zeroes
        // TODO (RC): Minor Non-Leading Zeroes
        // TODO (RC): Patch Non-Leading Zeroes
        // TODO (RC): Major v.0 == Unstable / Stable
        // TODO (RC): Major > v.0 = !Unstable / Stable

        // TODO (RC): SemVer + Patch = Bump Patch
        // TODO (RC): SemVer + Minor = Bump Minor, Reset Patch
        // TODO (RC): SemVer + Major = Bump Minor and Patch, Reset Minor and Patch

        // TODO (RC): Pre-Release Version - "1.2.3-Alpha"
        //  - Dot-Separated
        //  - Non-Empty Identifiers
        //  - Alphanumeric
        //  - No Leading Zeroes in Numeric Identifiers
        //  - Pre-Release < Normal Release
        //  - "1.2.3-Staging" == IsPreRelease / !IsNormal
        //  - "1.2.3-Staging" == Unstable / !Stable

        // TODO (RC): Build Metadata - "1.2.3+321337
        //  - Dot-Separated
        //  - Non-Empty Identifiers
        //  - Alphanumeric
        //  - Ignored Determining Precedence
        //  - Two Identical Versions w/ Different Build Metadata Are Equal

        // TODO (RC): Precedence - http://semver.org/#spec-item-11

        // TODO (RC): a.SemVer > b.SemVer - "New Version"
        // TODO (RC): b.IsBreaking(a) - (b.Major > a.Major)


        private void DoSemVerStuff(SemVer semVer)
        {

        }
    }
}