using System;
using Galileo.Core.Report;
using Galileo.Core.Submissions;

namespace Galileo.Core.Checks
{
    interface ICheck
    {
        CheckFactory.CheckType GetType();
        string ToString();
        string GetName();
        string GetKBLink();
        string GetDescription();
        string GetID();
        void Check(Submission other);
        bool Flagged();
        Flag GetFlag();
        float GetWeight();
        string GetLocalReference();
    }
}
