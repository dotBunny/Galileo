using System;
using System.Text;

namespace Galileo.Core
{
    internal class Markdown
    {
        public static string Strip(string source)
        {
            //string baseReplace = source.Replace("**__", "")
            //             .Replace("__**", "")
            //             .Replace("```", "")
            //             .Replace("------- | :----", "")
            //             .Replace("###### ", "")
            //             .Replace("##### ", "")
            //             .Replace("#### ", "")
            //             .Replace("### ", "")
            //             .Replace("## ", "");

            //if(baseReplace.EndsWith("_"))
            //{
            //    baseReplace = baseReplace.Substring(0, baseReplace.Length - 1);
            //}
            //if(baseReplace.StartsWith("_"))
            //{
            //    baseReplace = baseReplace.Substring(1);
            //}

            //return baseReplace;
            return source;
        }

        public static string Bold(string text, bool emphasis = false)
        {
            if ( emphasis )
            {
                return "**__" + text + "__**";
            }
            return "**" + text + "**";
        }

        public static string Blockquote(string text)
        {
            // TODO: Find all new lines and add > infront
            return "> " + text;
        }

        public static string Emphasis(string text)
        {
            return "_" + text + "_";
        }
      
        public static string H1(string header)
        {
            return "# " + header + Platform.EndOfLine();
        }

        public static string H2(string header)
        {
            return "## " + header + Platform.EndOfLine();;
        }

        public static string H3(string header)
        {
            return "### " + header + Platform.EndOfLine();;
        }

        public static string H4(string header)
        {
            return "#### " + header + Platform.EndOfLine(); ;
        }

        public static string H5(string header)
        {
            return "##### " + header + Platform.EndOfLine(); ;
        }

        public static string H6(string header)
        {
            return "###### " + header + Platform.EndOfLine(); ;
        }

        public static string HR()
        {
            return "-----";
        }

        public static string Linefeed()
        {
            return "  " + Platform.EndOfLine();
        }

        public static string Link(string text, string uri)
        {
            return "[" + text + "](" + uri + ")";
        }

        public static string OrderedList(string[] items, string indent = "")
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < items.Length; i++ )
            {
                builder.Append(indent + (i+1).ToString() + ". " + items[i] + Platform.EndOfLine());
            }
            return builder.ToString();
        }

        public static string StrikeOut(string text)
        {
            return "~~" + text + "~~";
        }

        public static string UnorderedList(string[] items, string indent = "")
        {
            StringBuilder builder = new StringBuilder();
            foreach(string s in items)
            {
                builder.Append(indent + "- " + s + Platform.EndOfLine());
            }
            return builder.ToString();
        }

        public static string Highlight(string text, string type = "")
        {
            if ( type != "" )
            {
                return "```" + type + Platform.EndOfLine() + text + Platform.EndOfLine() + "```";   
            }
            return "```" + Platform.EndOfLine() + text + Platform.EndOfLine() + "```";
        }
    }
}
