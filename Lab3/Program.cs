// Lab 3
// Student name: Brian Pham
// Student number: 24626509

using System;
using System.Collections.Generic;

Console.WriteLine("CPEN223 Lab 3");

//Testing: Write some test cases to test well all methods you are to implement    
//         This is to demonstrates what test cases you have considered
//TODO 
Console.WriteLine("Default Test Case:");
Console.WriteLine("SensorAnalyzer.IsUsableReading(21.5, 0.0, 50.0)");
bool dtc = SensorAnalyzer.IsUsableReading(21.5, 0.0, 50.0);
Console.WriteLine($"Expected: True, Actual: {dtc}");
Console.WriteLine("");

Console.WriteLine("Empty Collection Test Cases:");
List<double> a = new();
Console.WriteLine("New List a: {}");
Console.WriteLine("SensorAnalyzer.CleanReadings(a, 0, 100)");
List<double> etc1 = SensorAnalyzer.CleanReadings(a, 0, 100); // empty list
Console.WriteLine($"Expected: 0, Actual: {etc1.Count}");
Console.WriteLine("SensorAnalyzer.MovingAverage(a, 3)");
List<double> etc2 = SensorAnalyzer.MovingAverage(a, 3); //empty list
Console.WriteLine($"Expected: 0, Actual: {etc2.Count}");
Console.WriteLine("");

Console.WriteLine("Range Boundaries Test Case:");
List<double> b = new() { 0.0, 50.0 };
Console.WriteLine("New List b: { 0.0, 50.0 }");
Console.WriteLine("SensorAnalyzer.CleanReadings(b, 0.0, 50.0)");
List<double> rbtc = SensorAnalyzer.CleanReadings(b, 0.0, 50.0); // keeps both values
Console.WriteLine($"Expected: 2, Actual: {rbtc.Count}");
Console.WriteLine("");

Console.WriteLine("Approximate Equality Test Case:");
List<double> c = new() { 0.1 + 0.2 };
Console.WriteLine("New List c: { 0.1 + 0.2 }");
Console.WriteLine("SensorAnalyzer.ContainsApproximately(c, 0.3, 1e-12)");
bool aetc = SensorAnalyzer.ContainsApproximately(c, 0.3, 1e-12); // true
Console.WriteLine($"Expected: true, Actual: {aetc}");
Console.WriteLine("");

Console.WriteLine("NaN Test Case:");
List<double> d = new() {1.0, double.NaN, 2.0};
Console.WriteLine("New List d: {1.0, double.NaN, 2.0}");
Console.WriteLine("SensorAnalyzer.CleanReadings(d, 0.0, 10.0)");
List<double> ntc = SensorAnalyzer.CleanReadings(d, 0.0, 10.0); // returns 1.0, 2.0
Console.WriteLine($"Expected: 2, Actual: {ntc.Count}");
Console.WriteLine("");

Console.WriteLine("Moving Average Test Case:");
List<double> e = new() { 2.0, 4.0, 6.0 };
Console.WriteLine("New List e: { 2.0, 4.0, 6.0 }");
Console.WriteLine("SensorAnalyzer.MovingAverage(e, 1)");
List<double> matc1 = SensorAnalyzer.MovingAverage(e, 1); // 2.0, 4.0, 6.0
Console.WriteLine($"Expected: 3, Actual: {matc1.Count}");
Console.WriteLine("SensorAnalyzer.MovingAverage(e, 3)");
List<double> matc2 = SensorAnalyzer.MovingAverage(e, 3); // 4.0
Console.WriteLine($"Expected: 1, Actual: {matc2.Count}");
Console.WriteLine("SensorAnalyzer.MovingAverage(e, 4)");
List<double> matc3 =SensorAnalyzer.MovingAverage(e, 4); // empty list
Console.WriteLine($"Expected: 0, Actual: {matc3.Count}");
Console.WriteLine("");
//end Testing code

//Do not change the program skeleton
public static class SensorAnalyzer
{
    public static bool IsUsableReading(
        double reading, double minimum, double maximum)
    {
		// throw exception
        if (!double.IsFinite(minimum) || !double.IsFinite(maximum) || (minimum > maximum))
		{
			throw new ArgumentException("ERROR: range boundaries incorrect");
		}
		
		// return false even if reading is NaN or infinity
		return double.IsFinite(reading) && (reading >= minimum) && (reading <= maximum);
    }

    public static List<double> CleanReadings(
        IReadOnlyList<double> readings, double minimum, double maximum)
    {
		// no readings, throw exception
        if (readings == null)
		{
			throw new ArgumentException("ERROR: no readings");
		}
		
		// throw exception
		if (!double.IsFinite(minimum) || !double.IsFinite(maximum) || (minimum > maximum))
		{
			throw new ArgumentException("ERROR: range boundaries incorrect");
		}
		
		// new empty list
		List<double> cleaned = new List<double>();
		
		// add clean readings to empty list
		for (int i = 0; i < readings.Count; i++) 
		{
			if (double.IsFinite(readings[i]) && (readings[i] >= minimum) && (readings[i] <= maximum))
			{
				cleaned.Add(readings[i]);
			}
		}
		
		// return list
		return cleaned;
    }

    public static bool ContainsApproximately(
        IReadOnlyList<double> readings, double target, double tolerance)
    {
		// no readings, throw exception
        if (readings == null)
		{
			throw new ArgumentException("ERROR: no readings");
		}
		
		// throw exception
		if (!double.IsFinite(target) || !double.IsFinite(tolerance) || (tolerance < 0))
		{
			throw new ArgumentException("ERROR: target/tolerance incorrect");
		}
		
		// return true if readings is within tolerance
		for (int i = 0; i < readings.Count; i++) 
		{
			if (double.IsFinite(readings[i]) && (Math.Abs(readings[i] - target) <= tolerance))
			{
				return true;
			}
		}
		
		// otherwise, return false
		return false;
    }

    public static List<double> MovingAverage(
        IReadOnlyList<double> readings, int windowSize)
    {
		// no readings, throw exception
        if (readings == null)
		{
			throw new ArgumentException("ERROR: no readings");
		}
		
		// window size lesser than or equal to 0, throw exception
		if (windowSize <= 0)
		{
			throw new ArgumentException("ERROR: window size lesser than or equal to 0");
		}
		
		// if reading is not finite, throw exception
		for (int i = 0; i < readings.Count; i++) 
		{
			if (!double.IsFinite(readings[i]))
			{
				throw new ArgumentException("ERROR: readings cannot be NaN or infinity");
			}
		}
		
		// new empty list
		List<double> list = new List<double>();
		
		// if window size is greater than number of readings, return empty list
		if (windowSize > readings.Count)
		{
			return list;
		}
		
		// initialize window total to 0
		double windowTotal = 0;
		
		// sum elements in first window
		for (int j = 0; j < windowSize; j++)
		{
			windowTotal += readings[j];
		}
		
		// append mean of first window to empty list
		list.Add(windowTotal/windowSize);
		
		// iterate next window starting at previous window
		for (int k = windowSize; k < readings.Count; k++)
		{
			// add next reading and remove first reading from last window 
			windowTotal += readings[k] - readings[k - windowSize];
			// append mean of next window to list
			list.Add(windowTotal/windowSize);
		}
		
		// return list
		return list;
    }
}
