using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomDictionary
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomDictionary<int, string> dict = new CustomDictionary<int, string>();
            CustomDictionary<string, string> strDict = new CustomDictionary<string, string>();
            int choice = 1;
            do
            {
                Console.Clear();
                Console.WriteLine("1.Clear the dict\n2.Add an entry\n3.Remove an entry\n4.Fetch the value\n5.Simulate large number of key additions\n6.Try Simulating with string dictionary\n7.Exit\nPlease enter a choice");
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        dict = new CustomDictionary<int, string>();
                        Console.WriteLine("Dictionary cleared");
                        Console.ReadKey();
                        break;
                    case 2:
                        try
                        {
                            Console.WriteLine("Enter the key to add\n");
                            int key = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter the value of the key :");
                            string value = Console.ReadLine();
                            dict.Add(key, value);
                            Console.WriteLine("Added successfully");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        Console.ReadKey();
                        break;
                    case 3:
                        try
                        {
                            Console.WriteLine("Enter the key to remove :");
                            int key = Convert.ToInt32(Console.ReadLine());
                            dict.Remove(key);
                            Console.WriteLine("Removed successfully");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        Console.ReadKey();
                        break;
                    case 4:
                        try
                        {
                            Console.WriteLine("Enter the key to fetch value for :");
                            int key = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine(dict[key]);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        Console.ReadKey();
                        break;
                    case 5:
                        try
                        {
                            using (StreamWriter writer = new StreamWriter(@"D:\outputText.txt"))
                            {
                                int min, max, numberOfElements;
                                Console.WriteLine("Enter the start range of keys :");
                                min = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("Enter the end range of keys :");
                                max = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("Enter the number of keys to add :");
                                numberOfElements = Convert.ToInt32(Console.ReadLine());
                                Random rand = new Random();
                                for (int i = 0; i < numberOfElements; i++)
                                {
                                    int num = rand.Next(min, max);
                                    try
                                    {
                                        dict.Add(num, (num * 2).ToString());
                                        writer.WriteLine("Added " + num + " Array Capacity : " + dict.CustomDictionaryArrayCapacity + " Number of keys : " + dict.KeysCount + " Number of Non empty positions : " + (dict.CustomDictionaryArrayCapacity - dict.NumberOfEmptyArrayPositions) + " Fill Ratio : " + dict.ArrayFillRatio);
                                        //Console.WriteLine("Added " + num + " Array Capacity : " + dict.CustomDictionaryArrayCapacity + " Number of keys : " + dict.KeysCount + " Number of Non empty positions : " + (dict.CustomDictionaryArrayCapacity - dict.NumberOfEmptyArrayPositions) + " Fill Ratio : " + dict.ArrayFillRatio);
                                    }
                                    catch (Exception ex)
                                    {
                                        //Console.WriteLine("Could not add the number " + num);
                                        writer.WriteLine("Could not add the number " + num);
                                    }
                                }
                            }

                            Console.ReadKey();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Outer exception : " + ex.Message);
                        }
                        break;
                    case 6:
                        List<string> names = new List<string>();
                        using (StreamReader reader = new StreamReader(@"D:\namesList.txt"))
                        {
                            string line = "";
                            while ((line = reader.ReadLine()) != null)
                            {
                                names.Add(line.Trim());
                            }
                        }

                        try
                        {
                            using (StreamWriter writer = new StreamWriter(@"D:\strDictOutputText.txt"))
                            {
                                for (int i = 0; i < names.Count; i++)
                                {
                                    try
                                    {
                                        strDict.Add(names[i], names[i] + "_value");
                                        writer.WriteLine("Added " + names[i] + " Array Capacity : " + strDict.CustomDictionaryArrayCapacity + " Number of keys : " + strDict.KeysCount + " Number of Non empty positions : " + (strDict.CustomDictionaryArrayCapacity - strDict.NumberOfEmptyArrayPositions) + " Fill Ratio : " + strDict.ArrayFillRatio);
                                        //Console.WriteLine("Added " + num + " Array Capacity : " + dict.CustomDictionaryArrayCapacity + " Number of keys : " + dict.KeysCount + " Number of Non empty positions : " + (dict.CustomDictionaryArrayCapacity - dict.NumberOfEmptyArrayPositions) + " Fill Ratio : " + dict.ArrayFillRatio);
                                    }
                                    catch (Exception ex)
                                    {
                                        //Console.WriteLine("Could not add the number " + num);
                                        writer.WriteLine("Could not add the name " + names[i]);
                                    }
                                }
                            }

                            Console.ReadKey();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Outer exception : " + ex.Message);
                        }
                        break;
                    case 7:
                        break;
                    default:
                        Console.WriteLine("Please enter a valid choice");
                        Console.ReadKey();
                        break;
                }
            } while (choice != 6);
        }
    }
}
