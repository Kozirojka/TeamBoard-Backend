using System.Drawing;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RealTimeBoard.Api;

public class PointConverter : JsonConverter<Point>
{
    public override Point Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
        {
            var root = doc.RootElement;
            int x = root.GetProperty("X").GetInt32();
            int y = root.GetProperty("Y").GetInt32();
            return new Point(x, y);
        }
    }

    public override void Write(Utf8JsonWriter writer, Point value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteNumber("X", value.X);
        writer.WriteNumber("Y", value.Y);
        writer.WriteEndObject();
    }
}
