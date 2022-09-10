using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;

using MKLWrapper;

namespace MKLBenchmarkApp
{
    public class ViewData
    {
        public VMBenchmark Benchmark { get; set; }
        public bool ChangesNotSaved { get; set; } = false;

        public ViewData()
        {
            Benchmark = new VMBenchmark();
        }

        public void AddVMTime(VMf function, VMGrid grid)
        {
            Benchmark.AddVMTime(function, grid);
            ChangesNotSaved = true;
        }

        public void AddVMAccuracy(VMf function, VMGrid grid)
        {
            Benchmark.AddVMAccuracy(function, grid);
            ChangesNotSaved = true;
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
                
                // This duplication is necessary because we can save our data to multiple files
                saved = true;
                ChangesNotSaved = false;
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

        public bool Load(string filename)
        {
            bool loaded = false;
            FileStream? fileStream = null;

            try
            {
                fileStream = File.Open(filename, FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();

                Benchmark = formatter.Deserialize(fileStream) as VMBenchmark;
                loaded = true;
                ChangesNotSaved = false;
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
