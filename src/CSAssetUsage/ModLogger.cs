/*
    The MIT License(MIT)

    Copyright(c) 2015 Dimitri Slappendel

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.

    https://github.com/Archomeda/csl-common-shared-library
*/

using ColossalFramework.Plugins;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CSAssetUsage
{
    public static class ModLogger
    {
        private static string _prefix;

        public static void Initialize()
        {
            _prefix = string.Format("[{0}]", typeof(ModLogger).Assembly.GetName().Name);
        }

        /// <summary>
        /// Gets or sets whether the debug logging is enabled or not.
        /// </summary>
        public static bool DebugLogging { get; set; }

        /// <summary>
        /// Logs to the Unity Engine.
        /// </summary>
        /// <param name="logFunc">The Unity Engine log method to use.</param>
        /// <param name="message">The log message.</param>
        private static void LogUE(Action<object> logFunc, string message)
        {
            logFunc(string.Format("{0} {1}", _prefix, message));
        }

        /// <summary>
        /// Logs to the default output panel of the game.
        /// </summary>
        /// <param name="messageType">The message type to use.</param>
        /// <param name="message">The log message.</param>
        private static void LogOP(PluginManager.MessageType messageType, string message)
        {
            DebugOutputPanel.AddMessage(messageType, string.Format("{0} {1}", _prefix, message));
        }

        private static void LogFile(string message)
        {
            string logFileName = "CSAssetUsage.log";
            File.AppendAllText(logFileName, message);
            File.AppendAllText(logFileName, Environment.NewLine);
        }

        /// <summary>
        /// Logs a debug message.
        /// </summary>
        /// <param name="message">The log message.</param>
        public static void Debug(string message)
        {
            if (DebugLogging)
            {
                message = string.Format("[DEBUG] {0} - {1}", DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff"), message);
                LogUE(UnityEngine.Debug.Log, message);
                LogOP(PluginManager.MessageType.Message, message);
                LogFile(message);
            }
        }

        /// <summary>
        /// Logs a debug message through the string formatter.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        public static void Debug(string format, params object[] args)
        {
            Debug(string.Format(format, args));
        }

        /// <summary>
        /// Logs an info message.
        /// </summary>
        /// <param name="message">The log message.</param>
        public static void Info(string message)
        {
            LogUE(UnityEngine.Debug.Log, message);
            LogOP(PluginManager.MessageType.Message, message);
            LogFile(message);
        }

        /// <summary>
        /// Logs an info message through the string formatter.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        public static void Info(string format, params object[] args)
        {
            Info(string.Format(format, args));
        }

        /// <summary>
        /// Logs a warning message.
        /// </summary>
        /// <param name="message">The log message.</param>
        public static void Warning(string message)
        {
            LogUE(UnityEngine.Debug.LogWarning, message);
            LogOP(PluginManager.MessageType.Warning, message);
            LogFile(message);
        }

        /// <summary>
        /// Logs a warning message through the string formatter.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        public static void Warning(string format, params object[] args)
        {
            Warning(string.Format(format, args));
        }

        /// <summary>
        /// Logs an error message.
        /// </summary>
        /// <param name="message">The log message.</param>
        public static void Error(string message)
        {
            LogUE(UnityEngine.Debug.LogError, message);
            LogOP(PluginManager.MessageType.Error, message);
            LogFile(message);
        }

        /// <summary>
        /// Logs an error message through the string formatter.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        public static void Error(string format, params object[] args)
        {
            Error(string.Format(format, args));
        }

        public static void DumpObject(object myObject)
        {
            StringBuilder objectDetails = new StringBuilder();
            objectDetails.AppendLine();
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(myObject))
            {
                string name = descriptor.Name;
                object value = descriptor.GetValue(myObject);
                objectDetails.AppendLine(string.Format("{0}: {1}", name, value));
            }
            Debug(objectDetails.ToString());
        }

    }
}
