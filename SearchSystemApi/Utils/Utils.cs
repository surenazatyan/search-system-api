using SearchSystemApi.RequestResponseDTOs;

namespace SearchSystemApi.Utils;

public static class Utils
{
    public static double CalculateScore(string searchInput, string resultCandidate)
    {
        //return Levenshtein(searchInput.ToLower(), resultCandidate.ToLower()); // no good results
        return JaroWinklerSimilarity(searchInput.ToLower(), resultCandidate.ToLower());
    }

    // Jaro-Winkler similarity (returns 0-1 score)
    public static double JaroWinklerSimilarity(string s1, string s2)
    {
        if (string.IsNullOrEmpty(s1) && string.IsNullOrEmpty(s2)) return 1.0;
        if (string.IsNullOrEmpty(s1) || string.IsNullOrEmpty(s2)) return 0.0;

        double jaro = JaroSimilarity(s1, s2);
        int prefixLength = GetCommonPrefixLength(s1, s2);
        double winklerBonus = 0.1 * prefixLength * (1 - jaro);
        return jaro + winklerBonus;
    }

    // Jaro similarity (helper for Jaro-Winkler)
    private static double JaroSimilarity(string s1, string s2)
    {
        int len1 = s1.Length;
        int len2 = s2.Length;
        if (len1 == 0 && len2 == 0) return 1.0;

        int maxDist = Math.Max(len1, len2) / 2 - 1;
        bool[] matches1 = new bool[len1];
        bool[] matches2 = new bool[len2];
        int matches = 0;
        int transpositions = 0;

        // Find matches
        for (int i = 0; i < len1; i++)
        {
            int start = Math.Max(0, i - maxDist);
            int end = Math.Min(len2 - 1, i + maxDist);
            for (int j = start; j <= end; j++)
            {
                if (!matches2[j] && s1[i] == s2[j])
                {
                    matches1[i] = true;
                    matches2[j] = true;
                    matches++;
                    break;
                }
            }
        }

        if (matches == 0) return 0.0;

        // Count transpositions
        int k = 0;
        for (int i = 0; i < len1; i++)
        {
            if (matches1[i])
            {
                while (!matches2[k]) k++;
                if (s1[i] != s2[k]) transpositions++;
                k++;
            }
        }

        return ((double)matches / len1 + (double)matches / len2 + (double)(matches - transpositions / 2) / matches) / 3.0;
    }

    // Helper to get common prefix length (up to 4 chars for Winkler)
    private static int GetCommonPrefixLength(string s1, string s2)
    {
        int minLen = Math.Min(s1.Length, s2.Length);
        int prefixLen = 0;
        for (int i = 0; i < minLen && i < 4; i++)
        {
            if (s1[i] == s2[i]) prefixLen++;
            else break;
        }
        return prefixLen;
    }

    public static int Levenshtein(string a, string b)
    {
        if (string.IsNullOrEmpty(a)) return string.IsNullOrEmpty(b) ? 0 : b.Length;
        if (string.IsNullOrEmpty(b)) return a.Length;

        int[,] costs = new int[a.Length + 1, b.Length + 1];
        for (int i = 0; i <= a.Length; i++)
            costs[i, 0] = i;
        for (int j = 0; j <= b.Length; j++)
            costs[0, j] = j;

        for (int i = 1; i <= a.Length; i++)
        {
            for (int j = 1; j <= b.Length; j++)
            {
                int cost = a[i - 1] == b[j - 1] ? 0 : 1;
                costs[i, j] = Math.Min(
                    Math.Min(costs[i - 1, j] + 1, costs[i, j - 1] + 1),
                    costs[i - 1, j - 1] + cost);
            }
        }
        return costs[a.Length, b.Length];
    }



    public static double CalculateDistance(GeoLocation from, GeoLocation to)
    {
        // Haversine formula to calculate distance
        const double EarthRadius = 6371; // Earth radius in kilometers
        var dLat = ToRadians(to.Lat - from.Lat);
        var dLng = ToRadians(to.Lng - from.Lng);
        var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(ToRadians(from.Lat)) * Math.Cos(ToRadians(to.Lat)) *
                Math.Sin(dLng / 2) * Math.Sin(dLng / 2);
        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        var distance = EarthRadius * c;
        return distance;
    }

    public static double ToRadians(double angle)
    {
        return angle * Math.PI / 180;
    }
}
