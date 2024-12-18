using System.Collections.Concurrent;
using System.Collections.Generic;
using JavaMan.Interfaces;

namespace JavaMan.SdkProviders
{
    public class EclipseTemurinSdkProvider : ISdkProvider
    {
        private readonly IDictionary<string, string> _urls = new ConcurrentDictionary<string, string>();
        public EclipseTemurinSdkProvider()
        {
            _urls = new ConcurrentDictionary<string, string>();
        }
        private IDictionary<string, string> BuildUrls()
        {
            var urls = new ConcurrentDictionary<string, string>();
            urls.TryAdd("21.0.5-tem", "https://github.com/adoptium/temurin21-binaries/releases/download/jdk-21.0.5%2B11/OpenJDK21U-jdk_x64_windows_hotspot_21.0.5_11.zip");
            return urls;
        }
        public string GetUrl(string version)
        {
            return _urls[version];
        }
    }
}