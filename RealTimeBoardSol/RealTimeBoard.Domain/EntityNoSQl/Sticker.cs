namespace RealTimeBoard.Domain.EntityNoSQl;

public class Sticker : VectorObject
{
    public string Text { get; set; } = string.Empty;
    public int FontSize { get; set; } = 12;
    public string FontFamily { get; set; } = "Arial";
}