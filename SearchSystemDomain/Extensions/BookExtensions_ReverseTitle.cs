using System.Text;

namespace SearchSystemDomain.Extensions;

public static class BookExtensions_ReverseTitle
{
    /// <summary>
    /// Reverses a book title using Array.Reverse.
    /// Pros: Fast, uses built-in .NET methods, handles Unicode.
    /// Cons: Slightly more verbose than LINQ.
    /// </summary>
    public static string ReverseTitle_ViaArrayReverse(this string title)
    {
        if (string.IsNullOrEmpty(title)) return title;
        var chars = title.ToCharArray();
        Array.Reverse(chars);
        return new string(chars);
    }

    /// <summary>
    /// Reverses a book title using a manual loop.
    /// Pros: Explicit control, slightly faster for very large strings.
    /// Cons: More code, less readable, no practical advantage for short strings.
    /// </summary>
    public static string ReverseTitle_ViaManualLoop(this string title)
    {
        if (string.IsNullOrEmpty(title)) return title;
        var result = new char[title.Length];
        for (int i = 0, j = title.Length - 1; i < title.Length; i++, j--)
            result[i] = title[j];
        return new string(result);
    }

    /// <summary>
    /// Reverses a book title using StringBuilder.
    /// Pros: Efficient for repeated string manipulations, handles Unicode.
    /// Cons: Overkill for simple reversal, slightly less readable.
    /// </summary>
    public static string ReverseTitle_ViaStringBuilder(this string title)
    {
        if (string.IsNullOrEmpty(title)) return title;
        var sb = new StringBuilder(title.Length);
        for (int i = title.Length - 1; i >= 0; i--)
            sb.Append(title[i]);
        return sb.ToString();
    }
}