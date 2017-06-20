using System.Threading.Tasks;

namespace Exakis.Common.Logging
{
    using System;
    using System.IO;
    using System.Text;

    using PCLStorage;

    using global::Common.Logging;

    public class StaticFileLogger : global::Common.Logging.Simple.DebugOutLogger
    {
        #region Fields

        private readonly string staticFilePath;
        #endregion

        #region Constructors and Destructors

        public StaticFileLogger()
            : base(null, LogLevel.All, true, true, false, null)
        {
            this.staticFilePath =
                Path.Combine(
                Path.Combine(
                Path.Combine(
							    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    "merial"),
                    "PetPixie"),
                    "log.txt");
        }

        #endregion

        #region Methods
        
        protected override void WriteInternal(
            LogLevel level,
            object message,
            Exception exception)
        {
            try
            {
                var stringBuilder = new StringBuilder();
                this.FormatOutput(stringBuilder, level, message, exception);
                stringBuilder.AppendLine();
                var log = stringBuilder.ToString();
                System.Diagnostics.Debug.Write(log);
                File.AppendAllText(this.staticFilePath, log);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        #endregion
    }
}