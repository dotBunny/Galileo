using System.Collections.Generic;
using System.Text;
using Galileo.Core.Report;
using Galileo.Core.Submissions;

namespace Galileo.Core.Checks
{
    //TODO : Maybe our evaluated content needs to also have a lowercase version?? Or our comparisons need to not care
    class BaseCheck
    {
        internal Submission _submission;
        public Flag Flag { get; internal set; }
        internal string _localReference; 

        internal BaseCheck(Submission original)
        {
            // Do not set flag value as the base is called after the child.
            _submission = original;
        }

        public virtual string GetName() => "Base Check";
        public virtual string GetDescription() => "The default check, it really does nothing.";
        public virtual new CheckFactory.CheckType GetType() => CheckFactory.CheckType.None;
        public virtual string GetID()
        {
            return GetName().Replace(" ", "-").ToLower() + "-check";
        }

        public virtual string GetKBLink()
        {
            return Website.KB + "check/" + GetName().Replace(" ", "-").ToLower() + "/";
        }

        public virtual void Check(Submission other)
        {
        }
        public virtual string GetLocalReference()
        {
            return _localReference;
        }

        public virtual bool Flagged()
        {
            return Flag.Check;
        }
        public virtual float GetWeight()
        {
            return Flag.Weight;
        }
        public virtual Flag GetFlag()
        {
            return Flag;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("- " + GetName() + " (" + Flag.Weight.ToPercentage() +" Weighting)" + Platform.EndOfLine());

            List<string> submissions = new List<string>();
            foreach(KeyValuePair<Submission, FlagItem> item in Flag.Similar)
            {
                if ( item.Value.Reference != string.Empty)
                {
                    // -- Robin
                    // WAS:
                    //// submissions.Add(item.Key.RelativePath + " [**" + item.Value.GetPercentage() + "**: " + item.Value.Reference + "]");    
                    submissions.Add(item.Key.FileName + " [**" + item.Value.GetPercentage() + "**: " + item.Value.Reference + "]");    
                }
                else
                {
                    // -- Robin
                    // WAS:
                    //// submissions.Add(item.Key.RelativePath + " [**" + item.Value.GetPercentage() + "**]");
                    submissions.Add(item.Key.FileName + " [**" + item.Value.GetPercentage() + "**]");
                }

            }
            builder.Append(Markdown.UnorderedList(submissions.ToArray(), "  "));

            return builder.ToString();
        }
    }
}
