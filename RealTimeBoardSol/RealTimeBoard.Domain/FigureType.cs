namespace RealTimeBoard.Domain;

public enum FigureType
{
    Rectangle,   // Прямокутник
    Square,      // Квадрат
    Circle,      // Коло
    Ellipse,     // Еліпс
    Line,        // Пряма лінія
    Polyline,    // Полілінія (послідовність точок)
    Polygon,     // Багатокутник (трикутник, п'ятикутник тощо)
    Curve,       // Крива лінія (без'є або сплайн)
    Arc,         // Дуга
    Text,        // Текстовий блок
    Image        // Вставлене зображення (наприклад, PNG, SVG)
}