using System;
using System.Collections.Generic;
using System.Linq;

namespace Byndyusoft.ML.Tools.Metrics.Helpers
{
    /// <summary>
    ///     Douglas Peucker Reduction algorithm. <see href="https://gist.github.com/ahancock1/0d99b43c4c01ef9b4fe4a5e7ad1e9029" />
    /// </summary>
    /// <remarks>
    ///     Ramer Douglas Peucker algorithm is a line simplification
    ///     algorithm for reducing the number of points used to define its
    ///     shape.
    /// </remarks>
    public class DouglasPeuckerInterpolation
    {
        /// <summary>
        ///     Minimum number of points required to run the algorithm.
        /// </summary>
        private const int MinPoints = 3;

        /// <summary>
        ///     Gets the perpendicular distance of a point to the line between start
        ///     and end.
        /// </summary>
        /// <param name="start">The start point of the line.</param>
        /// <param name="end">The end point of the line.</param>
        /// <param name="point">
        ///     The point to calculate the perpendicular distance of.
        /// </param>
        /// <returns>The perpendicular distance.</returns>
        private static double GetDistance(Point start, Point end, Point point)
        {
            // Let A - start, B - end, P - point, AB - vector AB, AP - vector AP
            // We have to find point D on line segment AB closest to P
            // First we have to find point C on line AB such as PC should be perpendicular AB
            // Dot product AB * AP = |AB|*|AP|*cos(BAP) = |AB|*|AC| or -|AB|*|AC| if angle BAP is obtuse

            var vectorAB = new VectorStruct(start, end);
            var vectorAP = new VectorStruct(start, point);

            var squareOfvABLength = vectorAB.GetSquareOfLength(); // |AB|^2
            var dotProduct = vectorAB.CalculateDotProduct(vectorAP); // |AB|*|AC| or -|AB|*|AC| if angle BAP is obtuse
            var pointCCoefficient =
                dotProduct / squareOfvABLength; // ck = |AC|/|AB| or -|AC|/|AB| if angle BAP is obtuse

            // We can calculate if D is point C, A or B 
            var pointD = pointCCoefficient switch
            {
                < 0 => start, // Angle BAP is obtuse => C is before A => D = A
                > 1 => end, // C is after B, D = B
                _ => new Point(start.X + vectorAB.X * pointCCoefficient,
                    start.Y + vectorAB.Y * pointCCoefficient) // C is between A and B => D = C, ck * AB = AC
            };

            var vPC = new VectorStruct(point, pointD);
            return vPC.GetLength();
        }

        /// <summary>
        ///     Creates a new <see cref="Segment" /> with the start and end indices.
        ///     Calculates the max perpendicular distance for each specified point
        ///     against the line between start and end.
        /// </summary>
        /// <param name="start">The start index of the line.</param>
        /// <param name="end">The end index of the line.</param>
        /// <param name="points">The points.</param>
        /// <returns>The Segment</returns>
        /// <remarks>
        ///     If the segment doesn't contain enough values to be split again the
        ///     segment distance property is left as 0. This ensures that the segment
        ///     wont be selected again from the <see cref="GetReducedSegments" /> part of the
        ///     algorithm.
        /// </remarks>
        private static Segment CreateSegment(int start, int end, Point[] points)
        {
            var count = end - start + 1;

            if (count >= MinPoints)
            {
                var first = points[start];
                var last = points[end];

                var pointWithMaxDistance = points
                    .Skip(start + 1)
                    .Take(count - 2)
                    .Select((point, index) => new
                    {
                        Index = start + 1 + index,
                        Distance = GetDistance(first, last, point)
                    })
                    .OrderByDescending(p => p.Distance).First();

                return new Segment
                {
                    Start = start,
                    End = end,
                    PointIndexWithMaxDistance = pointWithMaxDistance.Index,
                    MaxDistanceFromPoints = pointWithMaxDistance.Distance
                };
            }

            return new Segment
            {
                Start = start,
                End = end,
                PointIndexWithMaxDistance = -1
            };
        }

        /// <summary>
        ///     Splits the specified segment about the perpendicular index and return
        ///     the segment before and after with calculated values.
        /// </summary>
        /// <param name="segment">The segment to split.</param>
        /// <param name="points">The points.</param>
        /// <returns>The two segments.</returns>
        private static IEnumerable<Segment> SplitSegment(
            Segment segment,
            Point[] points)
        {
            yield return CreateSegment(segment.Start, segment.PointIndexWithMaxDistance, points);
            yield return CreateSegment(segment.PointIndexWithMaxDistance, segment.End, points);
        }

        /// <summary>
        ///     Check to see if the point has valid values and returns true if not.
        /// </summary>
        /// <param name="point">The point to check.</param>
        /// <returns>False if the points values are valid.</returns>
        private static bool IsGap(Point point)
        {
            return double.IsNaN(point.X) || double.IsNaN(point.Y);
        }

        /// <summary>
        ///     Interpolates the specified points by reducing until the specified
        ///     tolerance is met or the specified max number of points is met.
        /// </summary>
        /// <param name="points">The points to reduce.</param>
        /// <param name="maxPointCount">The max number of points to return.</param>
        /// <param name="tolerance">
        ///     The min distance tolerance of points to return.
        /// </param>
        /// <returns>The interpolated reduced points.</returns>
        public static IEnumerable<Point> Interpolate(
            Point[] points,
            int maxPointCount,
            double tolerance = 0d)
        {
            if (maxPointCount < MinPoints || points.Length < maxPointCount)
                return points;

            var segments = GetSegments(points).ToArray();
            segments = GetReducedSegments(segments, points, maxPointCount, tolerance);

            return segments
                .OrderBy(segment => segment.Start)
                .SelectMany((segment, index) => GetPoints(segment, segments.Length, index, points));
        }

        /// <summary>
        ///     Gets the reduced points from the <see cref="Segment" />. Invalid values
        ///     are included in the result as well as last point of the last segment.
        /// </summary>
        /// <param name="segment">The segment to get the indices from.</param>
        /// <param name="count">The total number of segments in the algorithm.</param>
        /// <param name="index">The index of the current segment.</param>
        /// <param name="points">The points.</param>
        /// <returns>The valid points from the segment.</returns>
        private static IEnumerable<Point> GetPoints(
            Segment segment,
            int count,
            int index,
            Point[] points)
        {
            yield return points[segment.Start];

            var next = segment.End + 1;
            var nextPointIsGap = next < points.Length && IsGap(points[next]);
            var segmentIsLast = index == count - 1;

            if (segmentIsLast || nextPointIsGap)
            {
                yield return points[segment.End];

                if (nextPointIsGap)
                    yield return points[next];
            }
        }

        /// <summary>
        ///     Gets the initial <see cref="Segment" /> for the algorithm. If points
        ///     contains invalid values then multiple segments are returned for each
        ///     side of the invalid value.
        /// </summary>
        /// <param name="points">The points.</param>
        /// <returns>The segments.</returns>
        private static IEnumerable<Segment> GetSegments(Point[] points)
        {
            var previous = 0;

            foreach (var pointWithIndex in points.Select((point, index) => new
                         {
                             Point = point,
                             Index = index
                         })
                         .Where(p => IsGap(p.Point)))
            {
                yield return CreateSegment(previous, pointWithIndex.Index - 1, points);

                previous = pointWithIndex.Index + 1;
            }

            yield return CreateSegment(previous, points.Length - 1, points);
        }

        /// <summary>
        ///     Reduces the segments until the specified max or tolerance has been met
        ///     or the points can no longer be reduced.
        /// </summary>
        /// <param name="segments">The segments to reduce.</param>
        /// <param name="points">The points.</param>
        /// <param name="maxPointCount">The max number of points to return.</param>
        /// <param name="tolerance">The min distance tolerance for the points.</param>
        private static Segment[] GetReducedSegments(
            Segment[] segments,
            Point[] points,
            int maxPointCount,
            double tolerance)
        {
            var gapCount = points.Count(IsGap);
            var segmentQueue = new SegmentQueue();

            foreach (var segment in segments)
                segmentQueue.Enqueue(segment, segment.MaxDistanceFromPoints);

            // Check to see if max numbers has been reached.
            while (segmentQueue.Count + 1 + gapCount < maxPointCount)
            {
                // Check if tolerance has been met yet or can no longer reduce.
                if (segmentQueue.Peek().MaxDistanceFromPoints <= tolerance)
                    break;

                // Get the largest perpendicular distance segment.
                var current = segmentQueue.Dequeue();

                var spittedSegments = SplitSegment(current, points);

                foreach (var spittedSegment in spittedSegments)
                    segmentQueue.Enqueue(spittedSegment);
            }

            return segmentQueue.UnorderedItems.Select(i => i.Element).ToArray();
        }

        /// <summary>
        ///     Class representing a Douglas Peucker
        ///     segment. Contains the start and end index of the line,
        ///     the biggest distance of a point from the line and the
        ///     points index.
        /// </summary>
        private class Segment
        {
            /// <summary>
            ///     The start index of the line.
            /// </summary>
            public int Start { get; set; }

            /// <summary>
            ///     The end index of the line.
            /// </summary>
            public int End { get; set; }

            /// <summary>
            ///     The biggest perpendicular distance index of a point.
            /// </summary>
            public int PointIndexWithMaxDistance { get; set; }

            /// <summary>
            ///     The max perpendicular distance of a point along the line.
            /// </summary>
            public double MaxDistanceFromPoints { get; set; }
        }

        /// <summary>
        ///     Queue with priority for segments. Dequeuing always returns segment with maximum distance
        /// </summary>
        private class SegmentQueue : PriorityQueue<Segment, double>
        {
            /// <summary>
            ///     Enqueue segment with correct priority. Do not use standard Enqueue method.
            /// </summary>
            public void Enqueue(Segment segment)
            {
                Enqueue(segment, -segment.MaxDistanceFromPoints);
            }
        }

        /// <summary>
        ///     Vector
        /// </summary>
        private readonly struct VectorStruct
        {
            /// <summary>
            ///     X direction
            /// </summary>
            public double X { get; }

            /// <summary>
            ///     Y direction
            /// </summary>
            public double Y { get; }

            public VectorStruct(Point from, Point to)
            {
                X = to.X - from.X;
                Y = to.Y - from.Y;
            }

            /// <summary>
            ///     Calculate square of vector length
            /// </summary>
            public double GetSquareOfLength()
            {
                return X * X + Y * Y;
            }

            /// <summary>
            ///     Calculate vector length
            /// </summary>
            public double GetLength()
            {
                return Math.Sqrt(GetSquareOfLength());
            }

            /// <summary>
            ///     Calculate dot product
            /// </summary>
            public double CalculateDotProduct(VectorStruct secondVector)
            {
                return X * secondVector.X + Y * secondVector.Y;
            }
        }
    }

    /// <summary>
    ///     Point with double coordinates
    /// </summary>
    // TODO Вынести из этого файла
    public class Point
    {
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        ///     X coordinate
        /// </summary>
        public double X { get; }

        /// <summary>
        ///     Y coordinate
        /// </summary>
        public double Y { get; }
    }
}