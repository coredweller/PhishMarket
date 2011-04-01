﻿
namespace TheCore.Infrastructure
{
    public interface IImageSizes
    {
        int Width { get; }
        int MaxHeight { get; }
        bool OnlyResizeIfWider { get; }
    }
}
