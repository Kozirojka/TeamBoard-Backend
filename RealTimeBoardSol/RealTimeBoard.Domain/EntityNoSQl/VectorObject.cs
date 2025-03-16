    using System.Drawing;
    using System.Text.Json.Nodes;

    namespace RealTimeBoard.Domain.EntityNoSQl;

    public class VectorObject
    {
        public Guid Id { get; set; }
        public FigureType Type { get; set; }
        public Point Position { get; set; }
        public string Color { get; set; }
        public JsonNode? Data { get; set; }
    }