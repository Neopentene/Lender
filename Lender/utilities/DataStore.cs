using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lender.Utils
{
    internal class DataStore
    {
        public class Events
        {
            public delegate void OnRead();
            public delegate void OnReading();

            public delegate void OnWrite();
            public delegate void OnWriting();
        }

        public static XDocument? document;

        public static string? XmlFileName;

        public static Events.OnReading? OnReading { get; set; }
        public static Events.OnRead? OnRead { get; set; }

        public static Events.OnWriting? OnWriting { get; set; }
        public static Events.OnWrite? OnWrite { get; set; }

        public static class Reader
        {
            public static T Read<T>(string FileName, IReadable<T> Readable)
            {
                if (File.Exists(FileName)) document = XDocument.Load(FileName);
                else { document = new XDocument(); document.AddFirst(new XElement("Document")); document.Save(FileName); }

                XmlFileName = FileName;

                OnReading?.Invoke();

                T result = Readable.Read(in document);

                OnRead?.Invoke();
                return result;
            }
        }

        public static class Writer
        {
            public static async Task Write(IWritable Writable)
            {
                if (XmlFileName == null) throw new DataStoreError("No reference to file found.");
                if (document == null) throw new DataStoreError("Read a document first.");
                OnWriting?.Invoke();

                Writable.Write(document);
                await Task.Run(() => document.Save(XmlFileName));

                OnWrite?.Invoke();
            }
        }

        public interface IWritable
        {
            public void Write(in XDocument document);
        }

        public interface IReadable<T>
        {
            public T Read(in XDocument document);
        }
    }

    internal class DataStoreError : Exception
    {
        public DataStoreError(string message) : base(message) { }
    }
}
