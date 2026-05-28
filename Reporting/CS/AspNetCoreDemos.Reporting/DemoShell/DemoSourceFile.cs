using System;
using System.IO;
using System.Linq;

namespace AspNetCoreDemos.DemoShell {
    public class DemoSourceFile {
        readonly string _contentRootPath;
        readonly string _filePath;
        readonly IParser _parser;

        public DemoSourceFile(string contentRootPath, string filePath, IParser parser = null) {
            _contentRootPath = contentRootPath;
            _filePath = filePath;
            _parser = parser;
        }

        public string Name => Path.GetFileName(_filePath);

        public string GetRealPath() {
            var result = Path.Combine(_contentRootPath, _filePath);
#pragma warning disable DX0025 // doesn't use user input
            if(!File.Exists(result))
                result = Path.Combine(_contentRootPath, "Sources", _filePath + ".txt");
#pragma warning restore DX0025
            return result;
        }

        public string Read() {
            var realPath = GetRealPath();

            if(!File.Exists(realPath))
                return "File not found";

            var text = File.ReadAllText(realPath);
            text = text.Replace("\r", "");

            if(_parser != null)
                text = _parser.Parse(text);

            return text;
        }

    }
}
