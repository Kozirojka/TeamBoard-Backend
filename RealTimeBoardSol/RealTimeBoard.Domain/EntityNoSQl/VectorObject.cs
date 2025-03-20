    using System.Drawing;
    using System.Text.Json.Nodes;
    using System.Text.Json.Serialization;

    namespace RealTimeBoard.Domain.EntityNoSQl;

    /// it will be like parent for other object that should have one or move specific field
    public class VectorObject
    {
        public Guid Id { get; set; }
        public Guid BoardId { get; set; }
        
        public FigureType Type { get; set; }
        
        [JsonConverter(typeof(PointConverter))]
        public Point Position { get; set; }
        public string Color { get; set; }
        
        /// in this filed we will send some property of
        /// object like height and weight, or we can send
        /// sequence of nodes for curve line
        public JsonNode? Data { get; set; }
    }