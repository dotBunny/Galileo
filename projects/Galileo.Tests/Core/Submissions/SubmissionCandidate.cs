using System;
using System.Collections.Generic;
using System.Text;
using Galileo.Core;
using System.IO;
using Xunit;
using Galileo.Core.Submissions;

namespace Galileo.Tests.Core.Submissions
{
    public class SubmissionCandidate
    {
        [Fact]
        public void FlatFolderSearch()
        {
          SubmissionCandidates candidates = new SubmissionCandidates();
          candidates.Add(@"C:\dev\_work\dotBunny\Galileo\Tests\Text Documents\DEV_WordPerfect_ONLY");
          candidates.Resolve(Path.GetTempPath());
          
          // Test?.wpd
          var candidate1 = candidates.FirstResolved;
          Assert.NotNull(candidate1);
          Assert.True((Path.GetFileNameWithoutExtension(candidate1.ReadPath).StartsWith("Test")));
          Assert.Equal(".wpd", Path.GetExtension(candidate1.ReadPath));
          Assert.Equal(Galileo.Core.FileTypes.Types.CorelWordPerfect, candidate1.FileType);
          
          // Test?.wpd
          var candidate2 = candidates.NextResolved;
          Assert.NotNull(candidate2);
          Assert.True((Path.GetFileNameWithoutExtension(candidate2.ReadPath).StartsWith("Test")));
          Assert.Equal(".wpd", Path.GetExtension(candidate2.ReadPath));
          Assert.Equal(Galileo.Core.FileTypes.Types.CorelWordPerfect, candidate2.FileType);

          Assert.NotEqual(candidate1, candidate2);

          // No more.
          Assert.Null(candidates.NextResolved);
        }

        [Fact]
        public void HiearchalFolderSearch()
        {
          SubmissionCandidates candidates = new SubmissionCandidates();
          candidates.Add(@"C:\dev\_work\dotBunny\Galileo\Tests\Text Documents\");


          candidates.Resolve(Path.GetTempPath());
          
          var candidate = candidates.FirstResolved;

          while(candidate != null)
          {
              string name = candidate.ReadPath;
              candidate = candidates.NextResolved;
          }



        }
    }
}
