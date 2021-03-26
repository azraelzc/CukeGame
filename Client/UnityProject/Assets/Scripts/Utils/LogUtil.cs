using System;
using System.Diagnostics;

namespace ZCGame.Utils {
    public static class LogUtil {
        #region LOG_LEVEL_MESSAGE

        [Conditional("LOG_LEVEL_MESSAGE"), Conditional("LOG_LEVEL_WARNING"), Conditional("LOG_LEVEL_EXCEPTION"), Conditional("LOG_LEVEL_ERROR")]
        public static void Log(object message) {
            UnityEngine.Debug.Log(message);
        }

        [Conditional("LOG_LEVEL_MESSAGE"), Conditional("LOG_LEVEL_WARNING"), Conditional("LOG_LEVEL_EXCEPTION"), Conditional("LOG_LEVEL_ERROR")]
        public static void Log(object message, UnityEngine.Object context) {
            UnityEngine.Debug.Log(message, context);
        }

        [Conditional("LOG_LEVEL_MESSAGE"), Conditional("LOG_LEVEL_WARNING"), Conditional("LOG_LEVEL_EXCEPTION"), Conditional("LOG_LEVEL_ERROR")]
        public static void LogFormat(UnityEngine.Object context, string format, params object[] args) {
            UnityEngine.Debug.LogFormat(context, format, args);
        }

        [Conditional("LOG_LEVEL_MESSAGE"), Conditional("LOG_LEVEL_WARNING"), Conditional("LOG_LEVEL_EXCEPTION"), Conditional("LOG_LEVEL_ERROR")]
        public static void LogFormat(string format, params object[] args) {
            UnityEngine.Debug.LogFormat(format, args);
        }

        #endregion

        #region LOG_LEVEL_WARNING

        [Conditional("LOG_LEVEL_WARNING"), Conditional("LOG_LEVEL_EXCEPTION"), Conditional("LOG_LEVEL_ERROR")]
        public static void LogWarning(object message) {
            UnityEngine.Debug.LogWarning(message);
        }

        [Conditional("LOG_LEVEL_WARNING"), Conditional("LOG_LEVEL_EXCEPTION"), Conditional("LOG_LEVEL_ERROR")]
        public static void LogWarning(object message, UnityEngine.Object context) {
            UnityEngine.Debug.LogWarning(message, context);
        }

        [Conditional("LOG_LEVEL_WARNING"), Conditional("LOG_LEVEL_EXCEPTION"), Conditional("LOG_LEVEL_ERROR")]
        public static void LogWarningFormat(string format, params object[] args) {
            UnityEngine.Debug.LogWarningFormat(format, args);
        }

        [Conditional("LOG_LEVEL_WARNING"), Conditional("LOG_LEVEL_EXCEPTION"), Conditional("LOG_LEVEL_ERROR")]
        public static void LogWarningFormat(UnityEngine.Object context, string format, params object[] args) {
            UnityEngine.Debug.LogWarningFormat(context, format, args);
        }

        #endregion

        #region LOG_LEVEL_EXCEPTION

        [Conditional("LOG_LEVEL_EXCEPTION"), Conditional("LOG_LEVEL_ERROR")]
        public static void LogException(Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }

        [Conditional("LOG_LEVEL_EXCEPTION"), Conditional("LOG_LEVEL_ERROR")]
        public static void LogException(Exception exception, UnityEngine.Object context) {
            UnityEngine.Debug.LogException(exception, context);
        }

        #endregion

        #region LOG_LEVEL_ERROR

        [Conditional("LOG_LEVEL_ERROR")]
        public static void LogError(object message) {
            UnityEngine.Debug.LogError(message);
        }

        [Conditional("LOG_LEVEL_ERROR")]
        public static void LogError(object message, UnityEngine.Object context) {
            UnityEngine.Debug.LogError(message, context);
        }

        [Conditional("LOG_LEVEL_ERROR")]
        public static void LogErrorFormat(string format, params object[] args) {
            UnityEngine.Debug.LogErrorFormat(format, args);
        }

        [Conditional("LOG_LEVEL_ERROR")]
        public static void LogErrorFormat(UnityEngine.Object context, string format, params object[] args) {
            UnityEngine.Debug.LogErrorFormat(context, format, args);
        }

        #endregion

        #region LOG_LEVEL_ASSERTION

        public static void LogAssertion(object message, UnityEngine.Object context) {
            UnityEngine.Debug.LogAssertion(message, context);
        }

        public static void LogAssertion(object message) {
            UnityEngine.Debug.LogAssertion(message);
        }

        public static void LogAssertionFormat(UnityEngine.Object context, string format, params object[] args) {
            UnityEngine.Debug.LogAssertionFormat(context, format, args);
        }

        public static void LogAssertionFormat(string format, params object[] args) {
            UnityEngine.Debug.LogAssertionFormat(format, args);
        }

        #endregion
    }
}
