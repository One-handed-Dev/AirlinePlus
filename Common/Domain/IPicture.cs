namespace Common.Domain
{
    public interface IPicture<T>
    {
        public T Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
    }
}
