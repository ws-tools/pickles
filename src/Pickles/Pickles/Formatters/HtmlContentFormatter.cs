﻿#region License

/*
    Copyright [2011] [Jeffrey Cameron]

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace Pickles.Formatters
{
    public class HtmlContentFormatter
    {
        private readonly HtmlFeatureFormatter htmlFeatureFormatter;
        private readonly HtmlMarkdownFormatter htmlMarkdownFormatter;

        public HtmlContentFormatter(HtmlFeatureFormatter htmlFeatureFormatter, HtmlMarkdownFormatter htmlMarkdownFormatter)
        {
            this.htmlFeatureFormatter = htmlFeatureFormatter;
            this.htmlMarkdownFormatter = htmlMarkdownFormatter;
        }

        public XElement Format(FeatureNode featureNode)
        {
            var xmlns = XNamespace.Get("http://www.w3.org/1999/xhtml");

            if (featureNode.Type == FeatureNodeType.Feature)
            {
                return this.htmlFeatureFormatter.Format(featureNode.Feature);
            }
            else if (featureNode.Type == FeatureNodeType.Markdown)
            {
                return this.htmlMarkdownFormatter.Format(File.ReadAllText(featureNode.Location.FullName));
            }

            throw new InvalidOperationException("Cannot format a FeatureNode with a Type of " + featureNode.Type + " as content");
        }
    }
}
