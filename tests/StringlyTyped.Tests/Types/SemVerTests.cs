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

        [Test]
        public void SemVer_NormalSemVer_ReturnsResult()
        {
            SemVer sv = new Stringly<SemVer>("1.2.3");

            Assert.That(sv.Major, Is.EqualTo(1));
            Assert.That(sv.Minor, Is.EqualTo(2));
            Assert.That(sv.Patch, Is.EqualTo(3));
        }

        [Test]
        public void SemVer_NegativeMajor_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                SemVer sv = new Stringly<SemVer>("-1.2.3");
            });
        }

        [Test]
        public void SemVer_NegativeMinor_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                SemVer sv = new Stringly<SemVer>("1.-2.3");
            });
        }

        [Test]
        public void SemVer_NegativePatch_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                SemVer sv = new Stringly<SemVer>("1.2.-3");
            });
        }

        [Test]
        public void SemVer_MajorLeadingZeroes_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                SemVer sv = new Stringly<SemVer>("01.2.3");
            });
        }

        [Test]
        public void SemVer_MinorLeadingZeroes_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                SemVer sv = new Stringly<SemVer>("1.02.3");
            });
        }

        [Test]
        public void SemVer_PatchLeadingZeroes_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                SemVer sv = new Stringly<SemVer>("01.2.3");
            });
        }

        [Test]
        public void SemVer_MajorIsZero_MarkedUnstable()
        {
            SemVer sv = new Stringly<SemVer>("0.1.2");

            Assert.That(sv.IsUnstable, Is.True);
            Assert.That(sv.IsStable, Is.False);
        }

        [Test]
        public void SemVer_MajorIsNonZero_MarkedStable()
        {
            SemVer sv = new Stringly<SemVer>("1.1.2");

            Assert.That(sv.IsUnstable, Is.False);
            Assert.That(sv.IsStable, Is.True);
        }

        [Test]
        public void SemVer_SinglePreReleaseVersion_SetsPreRelease()
        {
            SemVer sv = new Stringly<SemVer>("1.2.3-Alpha");

            Assert.That(sv.IsPreRelease, Is.True);
            Assert.That(sv.IsStable, Is.False);
            Assert.That(sv.IsUnstable, Is.True);
            Assert.That(sv.PreReleaseIdentifiers.Contains("Alpha"));
        }

        [Test]
        public void SemVer_MultiplePreReleaseVersions_SetsPreRelease()
        {
            SemVer sv = new Stringly<SemVer>("1.2.3-Alpha.Staging2.123");

            Assert.That(sv.IsPreRelease, Is.True);
            Assert.That(sv.IsStable, Is.False);
            Assert.That(sv.IsUnstable, Is.True);
            Assert.That(sv.PreReleaseIdentifiers.Contains("Alpha"));
            Assert.That(sv.PreReleaseIdentifiers.Contains("Staging2"));
            Assert.That(sv.PreReleaseIdentifiers.Contains("123"));
        }

        [Test]
        public void SemVer_NonAlphaPreReleaseIdentifier_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                SemVer sv = new Stringly<SemVer>("1.2.3-Staging!");
            });
        }

        [Test]
        public void SemVer_EmptyPreReleaseIdentifier_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                SemVer sv = new Stringly<SemVer>("1.2.3-");
            });
        }

        [Test]
        public void SemVer_NumericPreReleaseIdentifierLeadingZeroes_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                SemVer sv = new Stringly<SemVer>("1.2.3-01");
            });
        }

        [Test]
        public void SemVer_SingleBuildIdentifier_SetsBuild()
        {
            SemVer sv = new Stringly<SemVer>("1.2.3+build");

            Assert.That(sv.BuildIdentifiers.Contains("build"));
        }

        [Test]
        public void SemVer_MultipleBuildIdentifiers_SetsBuild()
        {
            SemVer sv = new Stringly<SemVer>("1.2.3+build.staging2.0123");

            Assert.That(sv.BuildIdentifiers.Contains("build"));
            Assert.That(sv.BuildIdentifiers.Contains("staging2"));
            Assert.That(sv.BuildIdentifiers.Contains("0123"));
        }

        [Test]
        public void SemVer_NonAlphaBuildIdentifier_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                SemVer sv = new Stringly<SemVer>("1.2.3+Staging!");
            });
        }

        [Test]
        public void SemVer_EmptyBuildIdentifier_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                SemVer sv = new Stringly<SemVer>("1.2.3+");
            });
        }

        [Test]
        public void SemVer_PreReleaseAndBuildIdentifiers_SetsAllIdentifiers()
        {
            SemVer sv = new Stringly<SemVer>("1.2.3-Staging+build0123");

            Assert.That(sv.PreReleaseIdentifiers.Contains("Staging"));
            Assert.That(sv.BuildIdentifiers.Contains("build0123"));
        }

        // TODO (RC): Enable Implementers to override the Exception Message - make it helpful to provide the right format!

        // TODO (RC): Precedence - http://semver.org/#spec-item-11

        // Pre-Release < Normal Release
        // Build Numbers Have No Meaning
        // TODO (RC): a.SemVer > b.SemVer - "New Version"
        // TODO (RC): b.IsBreaking(a) - (b.Major > a.Major)

        // TODO (RC): SemVer + Patch = Bump Patch
        // TODO (RC): SemVer + Minor = Bump Minor, Reset Patch
        // TODO (RC): SemVer + Major = Bump Minor and Patch, Reset Minor and Patch
    }
}