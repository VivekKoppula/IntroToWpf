using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Transactions;
using System.Windows;
using System.Windows.Input;
using System.Xml;
using ListBoxScrollViewer.Infra;
using ListBoxScrollViewer.Models;

namespace ListBoxScrollViewer.ViewModels
{
    public class UserControl3ViewModel : BaseViewModel, IPageViewModel
    {
        public event EventHandler<EventArgs<string>>? ViewChanged;

        private string systemSoftwareVersion = string.Empty;
        public ObservableCollection<string> MyStrings { get; }
        public ObservableCollection<SoftwareRelease> SoftwareReleases { get; }
        public string PageId { get; set; }
        public string Title { get; set; } = "View 3";

        private XmlDocument releaseNotesXml;

        private bool _showingFilteredNotes = false;
        public bool ShowingFilteredNotes
        {
            get { return _showingFilteredNotes; }
            set
            {
                _showingFilteredNotes = value;
                OnPropertyChanged(nameof(ShowingFilteredNotes));
            }
        }

        private SoftwareRelease _selectedRelease = null!;
        public SoftwareRelease SelectedRelease
        {
            get => _selectedRelease;
            set
            {
                if (_selectedRelease != value)
                {
                    foreach (SoftwareRelease release in SoftwareReleases)
                        release.IsSelected = (release == value);

                    _selectedRelease = value;
                    OnPropertyChanged(nameof(SelectedRelease));
                }
            }
        }

        public UserControl3ViewModel(string pageIndex = "3")
        {
            releaseNotesXml = new XmlDocument();

            SoftwareReleases = new ObservableCollection<SoftwareRelease>();

            RetrieveEmbeddedXml();

            ReadReleaseNotesFile();

            PageId = pageIndex;

            MyStrings = new ObservableCollection<string>();

            var stringSample = "This is a very very very very very very very very very very very long long long long long long long long long long long long long long long long long long sample text to test a listbox scroller...";
            stringSample = stringSample + stringSample + stringSample;
            for (int i = 0; i < 15; i++)
            {
                MyStrings.Add(stringSample);
            }
        }
        private void RetrieveEmbeddedXml()
        {
            var currentAssembly = this.GetType().Assembly;

            var manifestResourceNames = currentAssembly.GetManifestResourceNames();

            string? filePath = manifestResourceNames.FirstOrDefault(x => x.Contains("CurrentReleaseNotes"));

            RetrieveXmlFromStream(currentAssembly.GetManifestResourceStream(filePath!)!);
        }

        public void RetrieveXmlFromStream(Stream fileStream)
        {
            using (fileStream)
            {
                try
                {
                    var xmlDocument = new XmlDocument();
                    xmlDocument.Load(fileStream);
                    releaseNotesXml = xmlDocument;
                }
                catch (Exception e)
                {
                    var message = e.Message;
                    MessageBox.Show(message, "Error!!", MessageBoxButton.OK, MessageBoxImage.Error);
                    releaseNotesXml = null!;
                }
            }
        }

        public bool ReadReleaseNotesFile(string targetedVersion = null!)
        {
            ShowingFilteredNotes = !string.IsNullOrEmpty(targetedVersion);

            try
            {
                XmlNodeList nodes = releaseNotesXml.GetElementsByTagName("release")!;

                if (nodes == null || nodes.Count <= 0)
                {
                    // _logger.Log(nameof(ReleaseNotesViewModel), "XML is not a valid release notes file", Level.Error);
                    return false;
                }

                SoftwareReleases.Clear();
                SelectedRelease = null!;

                foreach (XmlNode releaseNode in nodes)
                {
                    if (releaseNode.Name != "release" || releaseNode is null)
                        throw new ArgumentException($"Invalid node Name {releaseNode?.Name}");

                    string fullVersion = releaseNode.Attributes?.GetNamedItem("version")?.InnerText!;
                    string displayVersion = releaseNode.Attributes?.GetNamedItem("displayVersion")?.InnerText!;

                    if (string.IsNullOrEmpty(systemSoftwareVersion))
                        systemSoftwareVersion = "0.0.23170.1213";

                    if (fullVersion == "current")
                        fullVersion = (ShowingFilteredNotes ? targetedVersion : systemSoftwareVersion);

                    // Skip past this version if we're only showing updates and it's not newer than the current version
                    if (ShowingFilteredNotes && new Version(fullVersion) <= new Version(systemSoftwareVersion))
                        continue;

                    if (string.IsNullOrEmpty(fullVersion) || string.IsNullOrEmpty(displayVersion))
                    {
                        // _logger.Log(nameof(ReleaseNotesViewModel), "This release note is missing version information. Full: {fullVersion} Display: {displayVersion}", Level.Trace);
                        continue;
                    }

                    string date = GetDisplayDate(fullVersion);

                    // string buildDateString = string.Format(TranslationManager.Instance.Translate("RN_BUILD_DATE").ToString(), date); // Build Date: {0}
                    string buildDateString = "Build Date: {0}";

                    SoftwareRelease version = new SoftwareRelease(fullVersion, displayVersion, buildDateString);

                    foreach (XmlNode node in releaseNode.ChildNodes)
                    {
                        // Parse each top level node belonging to this release
                        string text = "";
                        DetailStyle style = DetailStyle.Subheader;

                        switch (node.Name)
                        {
                            case "features":
                                //text = TranslationManager.Instance.Translate("RN_NEW_FEATURES").ToString();
                                text = "New Features";
                                break;
                            case "enhancements":
                                text = "UX Enhancements"; // TranslationManager.Instance.Translate("RN_ENHANCEMENTS").ToString();
                                break;
                            case "bugfixes":
                                text = "Bug Fixes"; // TranslationManager.Instance.Translate("RN_BUG_FIXES").ToString();
                                break;
                            case "paragraph":
                                text = ParseTranslatedNode(node, fullVersion);
                                style = DetailStyle.Paragraph;
                                break;
                            default:
                                throw new ArgumentException($"Invalid node Name {node.Name}");
                        }

                        // If this is a proper node, add it to the version and parse all of its children if it has any
                        version.Notes.Add(new SoftwareReleaseNotes(text, style));

                        foreach (XmlNode childNode in node.ChildNodes)
                        {
                            if (childNode.Name != "item")
                                throw new ArgumentException($"Invalid node Name {childNode.Name}");

                            // Only attempt to grab the text for the current language
                            string childText = ParseTranslatedNode(childNode, fullVersion);

                            version.Notes.Add(new SoftwareReleaseNotes(childText, DetailStyle.Item));
                        }
                    }

                    if (version.Notes.Count > 0)
                        SoftwareReleases.Add(version);
                    else
                    {
                        // _logger.Log(nameof(ReleaseNotesViewModel), $"Release note {fullVersion} is missing contents.", Level.Error);
                    }
                    var fullVersionString = string.Format("Full Version Number: {0}", fullVersion);

                    // Add Release Notes footer information
                    version.Notes.Add(new SoftwareReleaseNotes(string.Empty, DetailStyle.Separator));
                    version.Notes.Add(new SoftwareReleaseNotes(fullVersionString, DetailStyle.Footer));
                    version.Notes.Add(new SoftwareReleaseNotes(buildDateString, DetailStyle.Footer));
                }

                if (SoftwareReleases.Count <= 0)
                {
                    // _logger.Log(nameof(ReleaseNotesViewModel), $"No software versions found in release notes.", Level.Error);
                    return false;
                }

                if (SelectedRelease == null)
                {
                    SelectedRelease = SoftwareReleases.First();
                    OpenCloseReleaseNotesBar(SelectedRelease);
                }

                SoftwareReleases.Last().IsLast = true;

                return true;
            }
            catch (Exception e)
            {
                // _logger.Log(nameof(ReleaseNotesViewModel), $"Exception reading XML {releaseNotesXml.NameTable}.", Level.Error, e);
                return false;
            }
        }

        public void OpenCloseReleaseNotesBar(SoftwareRelease softwareReleaseToOpenClose)
        {
            // Manage opened release.
            //if (softwareReleaseToOpenClose.Equals(OpenedRelease))
            //    OpenedRelease = null;
            //else
            //    OpenedRelease = softwareReleaseToOpenClose;

            //// Get current list of read releases.
            //SqlSettings.Instance.ReadSettingValue("RELEASE_NOTES_READ_VERSION_LIST", out string commaSeperatedCurrentVersionString);
            //HashSet<string> readSoftwareVersionSet = ConvertCommaSeperatedStringToHashSet(commaSeperatedCurrentVersionString);

            //if (OpenedRelease != null)
            //{
            //    // We opened a new release, so add it to the list of read releases.
            //    // This is modifying a HashSet, so adding a string that is already there will do nothing.
            //    readSoftwareVersionSet.Add(OpenedRelease.FullVersionNumber);
            //}

            //// Make sure all of the releases have their IsUnread property set according to settings.
            //foreach (SoftwareRelease softwareRelease in SoftwareReleases)
            //{
            //    softwareRelease.IsUnread = !readSoftwareVersionSet.Contains(softwareRelease.FullVersionNumber);
            //}

            //// Write the list of read releases back to settings.
            //commaSeperatedCurrentVersionString = ConvertHashsetToCommaSeperatedString(readSoftwareVersionSet);
            //SqlSettings.Instance.WriteSettingValue("RELEASE_NOTES_READ_VERSION_LIST", commaSeperatedCurrentVersionString, 0);
        }

        private string ParseTranslatedNode(XmlNode node, string fullVersion)
        {
            string text = node.Attributes?.GetNamedItem("en-US")?.InnerText!;
            string englishText = node.Attributes?.GetNamedItem("en-US")?.InnerText!;

            if (string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(englishText))
            {
                // Log an error if there is English text but no foreign language text
                // _logger.Log(nameof(ReleaseNotesViewModel), $"Release notes item in {fullVersion} with English text '{text}' is missing translation data for {systemLanguage}. Defaulting to English", Level.Warn);
            }
            else if (string.IsNullOrEmpty(text) && string.IsNullOrEmpty(englishText))
            {
                // If there's no text for either language, throw an exception
                throw new ArgumentException($"Text not found on node {node.Name}");
            }

            return !string.IsNullOrEmpty(text) ? text : englishText;
        }

        private string GetDisplayDate(string fullVersion)
        {
            return "6/19/2023"; // Some sample date. 
        }
    }
}
