﻿namespace ShopTARge23.Core.Domain
{
    public class FileToDatabase
    {
        public Guid Id { get; set; }
        public String? ImageTitle { get; set; }
        public byte[]? ImageData { get; set; }
        public Guid? RealEstateId { get; set; }
    }
}
