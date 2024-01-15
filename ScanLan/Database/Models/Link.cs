namespace ScanLan.Models
{
    public class Link
    {
        public LinkPart Begin { get; set; } = new LinkPart();

        public LinkPart End { get; set; } = new LinkPart();
    }
}