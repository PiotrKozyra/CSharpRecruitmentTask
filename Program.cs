using System;
using System.IO;
using System.Text;
using System.Collections;

namespace C_RecruitmentTask
{
  class Program
  {
    static void Main(string[] args)
    {
        Console.Write("Enter array dimensions(rows): ");
        int rows = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter array dimensions(columns): ");
        int columns = Convert.ToInt32(Console.ReadLine());

        ulong[,] array = Generate2dArray(rows, columns);

        string fileName = "array.txt";
        
        WriteToFile(fileName, ConvertToString(array));

        array = LoadFile(fileName);

        SortedList listOfAverages = CalculateAverages(array);

        array = SortArrayByColumn(array, listOfAverages);

        fileName = "array2.txt";
        WriteToFile(fileName, ConvertToString(array));
       
    }

    public static ulong[,] Generate2dArray(int rows, int columns)
    {
        ulong[,] array = new ulong[rows, columns];
        Random rnd = new Random();
        
        for (int i=0; i < rows ; i++){
            for (int j=0; j < columns ; j++)
            {
                array[i, j] = (ulong)rnd.Next(1, Int32.MaxValue);
            }
        }
        return array;
    }

    public static void WriteToFile(string fileName, string array) {
        File.WriteAllText(fileName, array);
    }

    public static string ConvertToString(ulong[,] array) {
        StringBuilder sb = new StringBuilder();
        sb.Clear();
        for (var i = 0; i < array.GetLength(0); i++)
        {
            for (var j = 0; j < array.GetLength(1); j++)
            {
                sb.Append(String.Format("{0}\t", array[i, j]) );
            }
            sb.Append(String.Format("\n") );
        }
        return sb.ToString();
    }

    public static ulong[,] LoadFile(string fileName) {
        string[] fileLines = File.ReadAllLines(fileName);
        return ConvertStringToArray(fileLines);
    }

    public static ulong[,] ConvertStringToArray(string[] fileLines) {
        ulong[,] array = new ulong[fileLines.Length, fileLines[0].Split('\t').Length-1];
        for (int i = 0; i < array.GetLength(0); i++)
        {
            string line = fileLines[i];
            for (int j = 0; j < array.GetLength(1); j++)
            {   
                string[] split = line.Split('\t');
                array[i, j] = Convert.ToUInt64(split[j]);
            }
        }
        return array;
    }

    public static SortedList CalculateAverages(ulong[,] array) {
        SortedList sortedList = new SortedList();

        ulong sum = 0;
        for (int i = 0; i < array.GetLength(1); i++)
        {
            sum = 0;
            for (int j = 0; j < array.GetLength(0); j++)
            {
                sum = sum + array[j,i];
            }
            ulong avarage = sum/(ulong)array.GetLength(0);
            sortedList.Add(avarage, i);   
        }
        return sortedList;
    }

    public static ulong[,] SortArrayByColumn(ulong[,] orgArray, SortedList sortedList) {
        ulong[,] arrayCopy = (ulong[,])orgArray.Clone();
        IList valueList = sortedList.GetValueList();
        
        for (int i=0; i < orgArray.GetLength(0); i++){
            for (int j=0; j < orgArray.GetLength(1); j++)
            {
                orgArray[i,j] = arrayCopy[i, (int)valueList[j]];
            }
        }
        return orgArray;
    }
  }
}
