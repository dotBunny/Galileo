using System;
using System.Collections.Generic;
using System.Text;

namespace Galileo.Core.Report
{
    class Layout
    {
        HunterSession _session;
        Dictionary<Guid, Submissions.Submission> Submissions = new Dictionary<Guid, Submissions.Submission>();

        HTML.Components.HeaderComponent _headerComponent;
        HTML.Components.Navbar _navbarComponent;
        HTML.Components.BackgroundComponent _backgroundComponent;

        HTML.Sections.OverviewSection _overviewSection;
        HTML.Sections.SubmissionsSection _submissionsSection;
        HTML.Sections.SettingsSection _settingsSection;

        HTML.Components.CopyrightComponent _copyrightComponent;
        HTML.Components.CustomJavascriptComponent _customJSComponent;
        HTML.Components.CustomStylesheetComponent _customCSSComponent;
        HTML.Components.FooterComponent _footerComponent;

        internal Layout(HunterSession hunter, string title = "Galileo Report")
        {
            _session = hunter;

            // Reference the submissions differently
            foreach(Submissions.Submission s in _session.Submissions)
            {
                Submissions.Add(s.GUID, s);
            }

            // Create our head
            _headerComponent = new HTML.Components.HeaderComponent(_session.Config.ProcessReportEmbedResources, title);
            _navbarComponent = new HTML.Components.Navbar(_session.Profile.PackageVersion);
            _backgroundComponent = new HTML.Components.BackgroundComponent();

            _overviewSection = new HTML.Sections.OverviewSection(_session, Submissions);
            _submissionsSection = new HTML.Sections.SubmissionsSection(_session, Submissions);
            _settingsSection = new HTML.Sections.SettingsSection(_session.Config);

            // Create our tail section
            _copyrightComponent = new HTML.Components.CopyrightComponent();
            _customJSComponent = new HTML.Components.CustomJavascriptComponent(_session.Config.ProcessReportEmbedResources);
            _customCSSComponent = new HTML.Components.CustomStylesheetComponent();
            _footerComponent = new HTML.Components.FooterComponent();
        }


        public override string ToString()
        {
            StringBuilder layout = new StringBuilder();

            // Create our head
            layout.Append(_headerComponent.GetHTML());
            layout.Append(_navbarComponent.GetHTML());
            layout.Append(_backgroundComponent.GetHTML());

            layout.Append(_overviewSection.GetHTML());
            layout.Append(_submissionsSection.GetHTML());
            layout.Append(_settingsSection.GetHTML());

            // Create our tail section
            layout.Append(_copyrightComponent.GetHTML());
            layout.Append(_customJSComponent.GetHTML());
            layout.Append(_customCSSComponent.GetHTML());
            layout.Append(_footerComponent.GetHTML());

            return layout.ToString();
        }
    }
}
