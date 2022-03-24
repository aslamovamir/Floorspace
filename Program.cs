using System;
using System.IO;


//Name: Amir Aslamov
//Program Description: This program is written to read two files and do calculations on the floors for clients



namespace AxelSpringerHQBuilding
{
    class Program
    {
        static void Main(string[] args)
        {
            //let's open the file "Lesson 17 - Floor Data.txt" and store the data in int format in an array
            //we know there are 75 rows in the file, correspondent to 75 floors, so the size of the array is 75
            const int SIZE = 75;
            int[] floors = new int[SIZE];

            //we create a stream object to fetch the data from another file
            StreamReader input_file;
            //we now open the file and read the data
            input_file = File.OpenText("Lesson 17 - Floor Data.txt");

            //now we store the fetched data into the array
            int index = 0;

            while(index < SIZE && !input_file.EndOfStream)
            {
                floors[index] = int.Parse(input_file.ReadLine());
                index++;
            }

            //now we fetch data from the 20th floor file
            StreamReader input_file2;
            input_file2 = File.OpenText("Lesson 17 - 20 Residents.txt");

            //we know create a jagged array to map each tenant to thier square foot need on the 20th floor
            //we know there are 10 tenants on the 20th floor, so the base size of the array is 10
            int[][] map20 = new int[10][];

            //let's now calculate the total square foot amount the tenants would need in total on 20th floor
            int total_required = 0;
            //we will also get how much storage is available on the 20th floor from the first array
            int total_available = floors[19];

            //now we loop through the rows of the input file and map each string, tenant, to a square foot
            //we know that tenants names are at odd indeces, while the square foot they need are at even indeces
            index = 0;
            int index_indicator = 0;
            string fetch;

            while (index < 20 && !input_file2.EndOfStream)
            {
                fetch = input_file2.ReadLine();
                if (fetch == " " || fetch == "\n")
                {
                    continue;
                }
                
                if (index_indicator%2 == 0)
                {
                    //we know that this is square foot, while the tenant is the next element in the file
                    //let's create a new array of size one for that tenant and store the square foor needed for tis tenant
                    map20[index] = new int[1] { int.Parse(fetch) };
                    total_required += map20[index][0];
                    index++;
                }
                index_indicator++;
            }

            //now we can calculate how much square foot is left by substracting the square foot required from square foot available
            int result = total_available - total_required;

            input_file2.Close();

            //now to verify our results we can create a new file and write our results in there
            StreamWriter output_file2;
            output_file2 = File.CreateText("Results.txt");

            //first let's write the jagged array
            output_file2.WriteLine("JAGGED ARRAY:");
            for (int i = 0; i < 10; i++)
            {
                output_file2.WriteLine(map20[i][0]);
            }

            //now we write total square footage available on the 20th floor
            output_file2.WriteLine("\nTOTAL AVAILABLE SQUARE FOOTAGE:");
            output_file2.WriteLine(total_available.ToString());

            //now we write how much total required square foot we got
            output_file2.WriteLine("\nREQUIRED SQUARE FOOT IN TOTAL:");
            output_file2.WriteLine(total_required.ToString());

            //now we write the total square footage left to sell
            output_file2.WriteLine("\nLEFT SQUARE FOOT IN TOTAL:");
            output_file2.WriteLine(result.ToString());

            output_file2.Close();
        }
    }
}
