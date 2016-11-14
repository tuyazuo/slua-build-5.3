
    using UnityEngine;
    using System.Text;
    using System.Collections.Generic;
    using System.IO;
    using TinyTeam.Debuger;

public class TTTextDictionary
{
    byte[] mBuffer;
    int mOffset = 0;
    char separatorChar = '=';
    Dictionary<string, string> dictionary;

    private TTTextDictionary()
    {
    }

    public TTTextDictionary(TextAsset asset)
    {
        mBuffer = asset.bytes;
        dictionary = ReadDictionary();
    }

    public TTTextDictionary(byte[] bytes)
    {
        mBuffer = bytes;
        dictionary = ReadDictionary();
    }

    public Dictionary<string, string> Dict
    {
        get { return dictionary; }
    }

    public override string ToString()
    {
        if (dictionary == null) return string.Empty;

        string content = string.Empty;
        Dictionary<string, string>.Enumerator emu = dictionary.GetEnumerator();
        while (emu.MoveNext())
        {
            content += (emu.Current.Key + " = " + emu.Current.Value + "\n");
        }
        return content;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public string Get(string key)
    {
        string val = string.Empty;
        if (dictionary != null)
        {
            if (!dictionary.TryGetValue(key, out val))
            {
                //TTDebuger.LogError("读取不到字典，不存在key = " + key);
            }
        }
        return val == null ? string.Empty : val;
    }

    bool canRead
    {
        get { return (mBuffer != null && mOffset < mBuffer.Length); }
    }

    static string ReadLine(byte[] buffer, int start, int count)
    {
#if UNITY_FLASH
// Encoding.UTF8 is not supported in Flash :(
		StringBuilder sb = new StringBuilder();

		int max = start + count;

		for (int i = start; i < max; ++i)
		{
			byte byte0 = buffer[i];

			if ((byte0 & 128) == 0)
			{
				// If an UCS fits 7 bits, its coded as 0xxxxxxx. This makes ASCII character represented by themselves
				sb.Append((char)byte0);
			}
			else if ((byte0 & 224) == 192)
			{
				// If an UCS fits 11 bits, it is coded as 110xxxxx 10xxxxxx
				if (++i == count) break;
				byte byte1 = buffer[i];
				int ch = (byte0 & 31) << 6;
				ch |= (byte1 & 63);
				sb.Append((char)ch);
			}
			else if ((byte0 & 240) == 224)
			{
				// If an UCS fits 16 bits, it is coded as 1110xxxx 10xxxxxx 10xxxxxx
				if (++i == count) break;
				byte byte1 = buffer[i];
				if (++i == count) break;
				byte byte2 = buffer[i];

				if (byte0 == 0xEF && byte1 == 0xBB && byte2 == 0xBF)
				{
					// Byte Order Mark -- generally the first 3 bytes in a Windows-saved UTF-8 file. Skip it.
				}
				else
				{
					int ch = (byte0 & 15) << 12;
					ch |= (byte1 & 63) << 6;
					ch |= (byte2 & 63);
					sb.Append((char)ch);
				}
			}
			else if ((byte0 & 248) == 240)
			{
				// If an UCS fits 21 bits, it is coded as 11110xxx 10xxxxxx 10xxxxxx 10xxxxxx 
				if (++i == count) break;
				byte byte1 = buffer[i];
				if (++i == count) break;
				byte byte2 = buffer[i];
				if (++i == count) break;
				byte byte3 = buffer[i];

				int ch = (byte0 & 7) << 18;
				ch |= (byte1 & 63) << 12;
				ch |= (byte2 & 63) << 6;
				ch |= (byte3 & 63);
				sb.Append((char)ch);
			}
		}
		return sb.ToString();
#else
        return Encoding.UTF8.GetString(buffer, start, count);
#endif
    }

    string ReadLine(bool skipEmptyLines)
    {
        int max = mBuffer.Length;

        // Skip empty characters
        if (skipEmptyLines)
        {
            while (mOffset < max && mBuffer[mOffset] < 32) ++mOffset;
        }

        int end = mOffset;

        if (end < max)
        {
            for (;;)
            {
                if (end < max)
                {
                    int ch = mBuffer[end++];
                    if (ch != '\n' && ch != '\r') continue;
                }
                else ++end;

                string line = ReadLine(mBuffer, mOffset, end - mOffset - 1);
                mOffset = end;
                return line;
            }
        }
        mOffset = max;
        return null;
    }

    string ReadLine()
    {
        return ReadLine(true);
    }

    Dictionary<string, string> ReadDictionary()
    {
        Dictionary<string, string> dict = new Dictionary<string, string>();
        char[] separator = new char[] {separatorChar};

        while (canRead)
        {
            string line = ReadLine();
            if (line == null) break;
            if (line.StartsWith("//")) continue;

#if UNITY_FLASH
			string[] split = line.Split(separator, System.StringSplitOptions.RemoveEmptyEntries);
#else
            string[] split = line.Split(separator, 2, System.StringSplitOptions.RemoveEmptyEntries);
#endif

            if (split.Length == 2)
            {
                string key = split[0].Trim();
                string val = split[1].Trim().Replace("\\n", "\n");
                dict[key] = val;
            }
        }
        return dict;
    }
}