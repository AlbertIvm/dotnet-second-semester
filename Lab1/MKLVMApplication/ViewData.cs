using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;

using MKLWrapper;

namespace MKLBenchmarkApp
{
    internal class ViewData
    {
        public VMBenchmark Benchmark { get; set; }
        public bool HasBenchmarkDataChanged { get; set; }
        public bool ChangesNotSaved { get; set; } = false;

        public ViewData()
        {
            Benchmark = new VMBenchmark();
        }

        public void AddVMTime(VMf function, VMGrid grid)
        {
            Benchmark.AddVMTime(function, grid);
        }

        public void AddVMAccuracy(VMf function, VMGrid grid)
        {
            Benchmark.AddVMAccuracy(function, grid);
        }

        public bool Save(string filename)
        {
            bool saved = false;
            FileStream? fileStream =  null;

            try
            {
                fileStream = File.Open(filename, FileMode.Create);
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fileStream, Benchmark);
                ChangesNotSaved = false;
                saved = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                        messageBoxText: $"Failed to save benchmark data; error: {ex.Message}.",
                        caption: "MKL Benchmark App",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error,
                        MessageBoxResult.OK);
            }
            finally
            {
                fileStream?.Close();
            }

            return saved;
        }

        public static bool Load(string filename, ref VMBenchmark vmb)
        {
            bool loaded = false;
            FileStream? fileStream = null;

            try
            {
                fileStream = File.Open(filename, FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();

                vmb = formatter.Deserialize(fileStream) as VMBenchmark;
                loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                        messageBoxText: $"Failed to load benchmark data; error: {ex.Message}.",
                        caption: "MKL Benchmark App",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error,
                        MessageBoxResult.OK);
            }
            finally
            {
                fileStream?.Close();
            }

            return loaded;
        }
    }
}
