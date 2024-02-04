namespace ListBoxScrollViewer.Models
{
    public enum DetailStyle
    {
        Subheader,
        Paragraph,
        Footer,
        Item,
        Separator,
    }

    public class SoftwareReleaseNotes
    {
        public SoftwareReleaseNotes(string text, DetailStyle detailStyle)
        {
            Text = text;
            Style = detailStyle;
        }
        public string Text { get; }
        public DetailStyle Style { get; }
    }
}
