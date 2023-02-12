namespace TARge21Shop.Core.Domain
{
    public class FileToApi
    {
        public Guid Id { get; set; }
        public string ExistingFilePath { get; set; }
        public byte[] ImageData { get; set; }
        public Guid? RealEstateId { get; set; }
    }
}
