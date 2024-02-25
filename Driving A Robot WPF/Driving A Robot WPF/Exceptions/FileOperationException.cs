namespace Driving_A_Robot_WPF.Exceptions
{
    public class FileOperationException : Exception
    {
        public FileOperationException(string message) : base(message) { }

        public class FileNotFoundException : FileOperationException
        {
            public FileNotFoundException(string filePath) : base($"File not found: {filePath}") { }
        }

        public class FileReadingException : FileOperationException
        {
            public FileReadingException(string message) : base(message) { }
        }

    }
}
