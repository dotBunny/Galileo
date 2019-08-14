using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Galileo.Core.Submissions;

namespace Galileo.Core.Checks
{
    static class CheckFactory
    {
        [Flags]
        public enum CheckType
        {
            None,
            Content,
            ContentHash,
            FileName,
            MetaCreator,
            MetaDateCreated,
            MetaDateLastPrinted,
            MetaDateModified,
            MetaLastModifiedBy
        }

        public static ConcurrentBag<ICheck> CreateChecks(Submission submission, CheckType types)
        {
            ConcurrentBag<ICheck> checks = new ConcurrentBag<ICheck>();

            foreach (CheckType type in types.GetUniqueFlags<CheckType>())
            {
                switch (type)
                {
                    case CheckType.Content:
                        checks.Add(new ContentCheck(submission));
                        break;
                    case CheckType.ContentHash:
                        checks.Add(new ContentHashCheck(submission));
                        break;
                    case CheckType.FileName:
                        checks.Add(new FileNameCheck(submission));
                        break;
                    case CheckType.MetaCreator:
                        checks.Add(new MetaCreatorCheck(submission));
                        break;
                    case CheckType.MetaDateCreated:
                        checks.Add(new MetaDateCreatedCheck(submission));
                        break;
                    case CheckType.MetaDateLastPrinted:
                        checks.Add(new MetaDateLastPrintedCheck(submission));
                        break;
                    case CheckType.MetaDateModified:
                        checks.Add(new MetaDateModifiedCheck(submission));
                        break;
                    case CheckType.MetaLastModifiedBy:
                        checks.Add(new MetaLastModifiedByCheck(submission));
                        break;
                }
            }

            return checks;
        }
    }
}
