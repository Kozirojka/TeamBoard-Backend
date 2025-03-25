    using System.Drawing;
    using System.Text.Json.Nodes;
    using System.Text.Json.Serialization;

    namespace RealTimeBoard.Domain.EntityNoSQl;

    /// it will be like parent for other object that should have one or move specific field
 public class VectorObject
 {
     public Guid Id { get; set; }
     public string Type { get; set; }
     public string? Name { get; set; }
     public string? Description { get; set; }
     public List<Point>? Points { get; set; }
     public double? Radius { get; set; }   
     public double? Width { get; set; }    
     public double? Height { get; set; }   
     public string? Color { get; set; }
     public double? StrokeWidth { get; set; }
     public string? StrokeColor { get; set; }
     public string? FillColor { get; set; }
     public double? Opacity { get; set; }
     public Dictionary<string, object>? Metadata { get; set; }
 }