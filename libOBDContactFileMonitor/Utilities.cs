using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.workflowconcepts.applications.filemonitor
{
    public static class Utilities
    {
        private static Random random = new Random();
        public static bool ValidatePortNumber(int PortNumber)
        {
            try
            {
                if (PortNumber > 0 && PortNumber <= 65535)
                {
                    return true;
                }
                else
                {
                    Log.Instance.Warn($"{nameof(PortNumber)}:{PortNumber} is outside of valid range");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"Exception: {ex.Message} {Environment.NewLine} Stacktrace: {ex.StackTrace}");
                return false;
            }
        }

        public static bool ValidatePortNumber(string PortNumber, out int Port)
        {
            try
            {
                if (string.IsNullOrEmpty(PortNumber))
                {
                    Log.Instance.Warn($"{nameof(PortNumber)} is null or empty");
                    Port = 0;
                    return false;
                }

                int iPort = 0;

                if (int.TryParse(PortNumber, out iPort))
                {
                    if (ValidatePortNumber(iPort))
                    {
                        Port = iPort;
                        return true;
                    }
                    else
                    {
                        Log.Instance.Warn($"{nameof(PortNumber)}:{PortNumber} is outside of valid range");
                        Port = 0;
                        return false;
                    }
                }
                else
                {
                    Log.Instance.Warn($"{nameof(PortNumber)}:{PortNumber} is not an integer");
                    Port = 0;
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"Exception: {ex.Message} {Environment.NewLine} Stacktrace: {ex.StackTrace}");
                Port = 0;
                return false;
            }
        }

        public static bool GetResourceIDFromBase64Token(string Token, out string ResourceID)
        {
            try
            {
                if (string.IsNullOrEmpty(Token))
                {
                    Log.Instance.Warn($"{nameof(Token)} is null/empty");
                    ResourceID = string.Empty;
                    return false;
                }

                string sToken = Encoding.UTF8.GetString(Convert.FromBase64String(Token));

                if (string.IsNullOrEmpty(sToken))
                {
                    Log.Instance.Warn($"{nameof(sToken)} is empty or null; assume SYSTEM");
                    ResourceID = string.Empty;
                    return true;
                }

                if (sToken.IndexOf(":") >= 0)
                {
                    ResourceID = sToken.Substring(0, sToken.IndexOf(":"));
                }
                else
                {
                    ResourceID = sToken;
                }

                return true;
            }
            catch (FormatException fex)
            {
                Log.Instance.Warn("Format Exception: " + fex.Message + Environment.NewLine + "Stacktrace: " + fex.StackTrace);

                if (string.IsNullOrEmpty(Token))
                {
                    ResourceID = string.Empty;
                }
                else
                {
                    ResourceID = Token;
                }

                return true;
            }
            catch (Exception ex)
            {
                Log.Instance.Error("Exception: " + ex.Message + Environment.NewLine + "Stacktrace: " + ex.StackTrace);
                ResourceID = string.Empty;
                return false;
            }
        }

        public static string CreateMD5Hash(string Message)
        {
            try
            {
                if (String.IsNullOrEmpty(Message))
                {
                    Log.Instance.Warn("Message is either null or empty.");
                    return null;
                }

                System.Security.Cryptography.HashAlgorithm algorithm = System.Security.Cryptography.MD5.Create();  //or use SHA256.Create();
                byte[] bHash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(Message));

                StringBuilder sb = new StringBuilder();

                foreach (byte b in bHash)
                {
                    sb.Append(b.ToString("X2"));
                }

                return sb.ToString();
            }
            catch
            {
                return null;
            }
        }

        public static string CleansePhoneNumber(string ANI, System.Globalization.CultureInfo Culture)
        {
            if (string.IsNullOrEmpty(ANI))
            {
                return string.Empty;
            }

            if (Culture == null)
            {
                return string.Empty;
            }

            switch (Culture.Name)
            {
                case "en-US":

                    string s = System.Text.RegularExpressions.Regex.Replace(ANI, "[^0-9]", "");

                    int iLength = 10;

                    if (s.Length <= iLength)
                    {
                        return s;
                    }
                    else
                    {
                        return s.Substring(s.Length - iLength, iLength);
                    }

                default:

                    return ANI;
            }
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789~!@#$%^&*()=-{}[]<>?.,";

            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static bool TranscodeText(string OriginalText, string OriginalEncoding, System.Text.Encoding TargetEncoding, out string TranscodedText)
        {
            try
            {
                TranscodedText = string.Empty;

                if(string.IsNullOrEmpty(OriginalText))
                {
                    Log.Instance.Warn($"{nameof(OriginalText)} is null/empty");
                    return false;
                }

                if (string.IsNullOrEmpty(OriginalEncoding))
                {
                    Log.Instance.Warn($"{nameof(OriginalEncoding)} is null/empty");
                    return false;
                }

                if(TargetEncoding == null)
                {
                    Log.Instance.Warn($"{nameof(TargetEncoding)} is null");
                    return false;
                }

                byte[] NodeBodyBytes = System.Text.Encoding.GetEncoding(OriginalEncoding).GetBytes(OriginalText);

                TranscodedText = TargetEncoding.GetString(NodeBodyBytes);

                NodeBodyBytes = null;

                return true;
            }
            catch(Exception ex)
            {
                Log.Instance.Error($"Exception:{ex.Message} {Environment.NewLine} StackTrace:{ex.StackTrace}");
                TranscodedText = string.Empty;
                return false;
            }
        }
    }
}
