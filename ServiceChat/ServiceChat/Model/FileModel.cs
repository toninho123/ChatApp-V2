﻿using Microsoft.AspNetCore.Http;

namespace ServiceChat.Model
{
    public class FileModel
    {
        public string FileName { get; set; }
        public IFormFile FormFile { get; set; }
    }
}